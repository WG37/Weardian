import postWebViewMessage from "./WebViewMessage";
import type { EncryptResponse } from "../types/encryption/EncryptResponse";
import type { DecryptResponse } from "../types/decryption/DecryptResponse";
import type { RetrievePayloadResponse } from "../types/retrieve/RetrievePayloadResponse";
import type { DeleteKeyResult } from "../types/delete/DeleteKeyResult";

export function encryptInput(keyName: string, password: string, isSynced: boolean): Promise<EncryptResponse> {
    return postWebViewMessage<EncryptResponse>("encryption", {
        keyName,
        password,
        isSynced
    });
}

export function retrieveKey(keyId: string): Promise<RetrievePayloadResponse> {
    return postWebViewMessage<RetrievePayloadResponse>("retrieveKey", {
        keyId
    });
}

export function retrieveAllKeys(): Promise<RetrievePayloadResponse[]> {
    return postWebViewMessage<RetrievePayloadResponse[]>("retrieveAllKeys");
}

export function decryptInput(selectedKey: string): Promise<DecryptResponse> {
    return postWebViewMessage<DecryptResponse>("decryption", {
        selectedKey
    });
}

export function deleteKeyById(selectedKey: string): Promise<DeleteKeyResult> {
    return postWebViewMessage<DeleteKeyResult>("deleteKey", {
        selectedKey
    });
}