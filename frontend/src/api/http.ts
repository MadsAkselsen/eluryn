const API_BASE_URL = import.meta.env.VITE_API_BASE_URL as string;

export async function apiGet<T>(path: string): Promise<T> {
  const res = await fetch(`${API_BASE_URL}${path}`, {
    method: "GET",
    headers: { "Accept": "application/json" },
    // if you later use cookie auth, flip this on:
    // credentials: "include",
  });

  if (!res.ok) {
    const text = await res.text().catch(() => "");
    throw new Error(`GET ${path} failed: ${res.status} ${res.statusText} ${text}`);
  }

  return res.json() as Promise<T>;
}
