
export default async function postWebViewMessage(messageType, object = {}) {
    return new Promise((resolve, reject) => {
        
        const webViewAPI = window.chrome.webview;
        
        const handler = (e) => {
            const response = e.data;

            console.log("WebView response:", response);

            if (response.type !== messageType) {
                return;
            }
            
            webViewAPI.removeEventListener("message", handler);

            if (response.success) {
                resolve(response.data);
            } else {
                reject(response.error);
            }
        };

        webViewAPI.addEventListener("message", handler);

        webViewAPI.postMessage({
            type: messageType,
            ...object
        });
    });
}