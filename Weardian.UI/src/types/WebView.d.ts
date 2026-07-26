interface WebViewApi {
  postMessage(message: unknown): void;

  addEventListener(
    type: "message",
    listener: (event: MessageEvent) => void,
  ): void;

  removeEventListener(
    type: "message",
    listener: (event: MessageEvent) => void,
  ): void;
}

interface Window {
  chrome: {
    webview: WebViewApi;
  };
}
