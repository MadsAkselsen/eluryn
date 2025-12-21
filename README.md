# eluryn

## Branch protection via Git hooks

GitHub’s branch protection rules are unavailable for this private repository, so local Git hooks enforce the policy instead.

### Setup

Run the setup script once after cloning to install the hooks:

```bash
npm run setup
```

This configures `core.hooksPath` to point at `.githooks`, enabling the `pre-push` hook.

### Behavior

* Pushing directly to `main` or `dev` is blocked with a clear message.
* All other branches are unaffected, so continue to use feature branches and pull requests for changes intended for `main` or `dev`.

### Disabling (if needed)

To temporarily disable the hooks (for example, when rebasing from a trusted mirror), run:

```bash
npm run hooks:disable
```

Re-enable afterwards with `npm run hooks:enable`.
