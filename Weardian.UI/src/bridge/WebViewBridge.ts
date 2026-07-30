import postWebViewMessage from "./WebViewMessage";
import type { EncryptResponse } from "../types/encryption/EncryptResponse";
import type { DecryptResponse } from "../types/decryption/DecryptResponse";
import type { RetrievePayloadResponse } from "../types/retrieve/RetrievePayloadResponse";
import type { DeleteKeyResponse } from "../types/delete/DeleteKeyResponse";
import type { RegisterResponse } from "../types/auth/registerResponse";
import type { loginResponse } from "../types/auth/loginResponse";
import type { LogoutResponse } from "../types/auth/logoutResponse";

export function encryptInput(
  keyName: string,
  password: string,
  createSynced: boolean,
): Promise<EncryptResponse> {
  return postWebViewMessage<EncryptResponse>("encryption", {
    keyName,
    password,
    createSynced,
  });
}

export function retrieveKey(keyId: string): Promise<RetrievePayloadResponse> {
  return postWebViewMessage<RetrievePayloadResponse>("retrieveKey", {
    keyId,
  });
}

export function retrieveAllKeys(): Promise<RetrievePayloadResponse[]> {
  return postWebViewMessage<RetrievePayloadResponse[]>("retrieveAllKeys");
}

export function decryptInput(selectedKey: string): Promise<DecryptResponse> {
  return postWebViewMessage<DecryptResponse>("decryption", {
    keyId: selectedKey,
  });
}

export function deleteKeyById(selectedKey: string): Promise<DeleteKeyResponse> {
  return postWebViewMessage<DeleteKeyResponse>("deleteKey", {
    keyId: selectedKey,
  });
}

export function registerUser(email: string, password: string): Promise<RegisterResponse> {
  return postWebViewMessage<RegisterResponse>("register", {
    email,
    password,
  });
}

export function loginUser(email: string, password: string): Promise<loginResponse> {
  return postWebViewMessage("login", {
    email,
    password,
  });
}

export function logoutUser(): Promise<LogoutResponse> {
  return postWebViewMessage<LogoutResponse>("logout");
}
