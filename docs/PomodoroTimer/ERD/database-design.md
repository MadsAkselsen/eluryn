## Database design notes & intent

### `focus_log_entries`

This table acts as an **append-only event log** and is the source of truth for all focus activity.

* A row is inserted **when an interval ends**
* Rows are rarely updated (only to set end-related fields)
* Designed for historical accuracy, auditing, and debugging
* Not used directly for leaderboard queries once rollups are in place

As the dataset grows, this table can be **time-partitioned** (e.g. monthly partitions by `started_at_utc`) to:

* Keep indexes small and efficient
* Improve time-based queries (last week, last month)
* Allow easy retention management by dropping old partitions

---

### Rollup tables (`user_focus_daily / weekly / monthly / all_time`)

These tables are **read models**, optimized for leaderboards and rank queries.

* One row per user per time period
* Updated incrementally using **UPSERT** whenever a focus interval ends
* Serve as the primary data source for:

  * Daily / weekly / monthly / all-time leaderboards
  * User rank and “neighbor” queries (e.g. `#56042 – #56048`)
* Prevent expensive aggregations over millions of raw log rows

Rollups allow leaderboard queries to scale linearly with **users × periods**, instead of **users × focus intervals**.

---

## Indexing strategy (Postgres)

### `focus_log_entries`

Optimized for recent lookups and focus-only scans:

```sql
CREATE INDEX idx_focus_entries_user_time
  ON focus_log_entries (user_id, started_at_utc);

CREATE INDEX idx_focus_entries_started_user_focus
  ON focus_log_entries (started_at_utc, user_id)
  WHERE interval_type = 'focus';
```

These indexes support:

* User activity history
* Efficient focus-only filtering
* Time-based partition pruning

---

### Rollup leaderboards

Indexes are structured to support both:

* **Top N leaderboards**
* **Rank + neighbors** queries

Tie-breaking is deterministic via `(focus_seconds DESC, user_id ASC)`.

```sql
CREATE INDEX idx_lb_daily
  ON user_focus_daily (date_utc, focus_seconds DESC, user_id ASC);

CREATE INDEX idx_lb_weekly
  ON user_focus_weekly (week_start_utc, focus_seconds DESC, user_id ASC);

CREATE INDEX idx_lb_monthly
  ON user_focus_monthly (month_start_utc, focus_seconds DESC, user_id ASC);

CREATE INDEX idx_lb_all_time
  ON user_focus_all_time (focus_seconds DESC, user_id ASC);
```

These indexes allow:

* Fast ranking without full table sorts
* Efficient retrieval of users immediately above/below a given user
* Stable ordering even when scores are tied
