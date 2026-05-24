import postWebViewMessage from "./WebViewMessage";

export async function encryptInput(keyName, password, isSynced) {
    await postWebViewMessage("encryption", {
        keyName,
        password,
        createSynced: isSynced
    });
}

export async function decryptInput(selectedKeyId) {
    await postWebViewMessage("decryption", {
        keyId: selectedKeyId
    });
}

export async function retrieveKey(keyId) {
    await postWebViewMessage("retrieveKey", {
        keyId: keyId
    });
}

export async function retrieveKeys() {
    await postWebViewMessage("retrieveAllKeys");
}

export async function deleteKeyById(selectedKeyId) {
    await postWebViewMessage("delete", {
        keyId: selectedKeyId
    });
}