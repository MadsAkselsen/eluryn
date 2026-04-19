const API_BASE_URL = import.meta.env.VITE_API_BASE_URL as string;

type ApiRequestOptions = {
  method?: "GET" | "PUT";
  body?: unknown;
};

async function apiRequest<T>(path: string, options: ApiRequestOptions = {}): Promise<T> {
  const method = options.method ?? "GET";
  const res = await fetch(`${API_BASE_URL}${path}`, {
    method,
    headers: {
      Accept: "application/json",
      ...(options.body === undefined ? {} : { "Content-Type": "application/json" }),
    },
    body: options.body === undefined ? undefined : JSON.stringify(options.body),
    // if you later use cookie auth, flip this on:
    // credentials: "include",
  });

  if (!res.ok) {
    const text = await res.text().catch(() => "");
    throw new Error(`${method} ${path} failed: ${res.status} ${res.statusText} ${text}`);
  }

  return res.json() as Promise<T>;
}

export function apiGet<T>(path: string): Promise<T> {
  return apiRequest<T>(path);
}

export function apiPut<T>(path: string, body: unknown): Promise<T> {
  return apiRequest<T>(path, { method: "PUT", body });
}
