import postWebViewMessage from "./WebViewMessage";

export async function encryptInput(keyName, password, isSynced) {
    return await postWebViewMessage("encryption", {
        keyName,
        password,
        createSynced: isSynced
    });
}

export async function decryptInput(selectedKeyId) {
    return await postWebViewMessage("decryption", {
        keyId: selectedKeyId
    });
}

export async function retrieveKey(keyId) {
    return await postWebViewMessage("retrieveKey", {
        keyId: keyId
    });
}

export async function retrieveKeys() {
    return await postWebViewMessage("retrieveAllKeys");
}

export async function deleteKeyById(selectedKeyId) {
    return await postWebViewMessage("deleteKey", {
        keyId: selectedKeyId
    });
}