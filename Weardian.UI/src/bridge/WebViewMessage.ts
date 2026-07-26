interface WebViewResponse<T = unknown> {
  type: string;
  success: boolean;
  data?: T;
  error?: string;
}

export default async function postWebViewMessage<T>(
  messageType: string,
  object: Record<string, unknown> = {},
): Promise<T> {
  return new Promise<T>((resolve, reject) => {
    const webViewApi = window.chrome.webview;

    const handler = (e: MessageEvent) => {
      const response = e.data as WebViewResponse<T>;

      if (response.type !== messageType) {
        return;
      }

      webViewApi.removeEventListener("message", handler);

      if (response.success) {
        resolve(response.data!);
      } else {
        reject(response.error);
      }
    };

    webViewApi.addEventListener("message", handler);

    webViewApi.postMessage({
      type: messageType,
      ...object,
    });
  });
}
