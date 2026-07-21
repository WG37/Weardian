import { useState, useEffect } from "react";
import { retrieveAllKeys, decryptInput, deleteKeyById } from "../bridge/WebViewBridge";
import type { RetrievePayloadResponse } from "../types/retrieve/RetrievePayloadResponse";
import Card from "../components/Card";
import KeyTable from "../components/KeyTable";

function KeyManagement() {
    /*create 1 more loading states */
    const [loadingKeys, setLoadingKeys] = useState(false);
    const [decrypting, setDecrypting] = useState(false);
    const [deleting, setDeleting] = useState(false);
    
    const [result, setResult] = useState<string>("");
    
    const [keys, setKeys] = useState<RetrievePayloadResponse[]>([])
    const [selectedKey, setSelectedKey] = useState<RetrievePayloadResponse | null>(null);
    const [showKeyId, setShowKeyId] = useState<string | null>(null);

    const [error, setError] = useState("");

    async function HandleDecryptKey(selectedKey: RetrievePayloadResponse) {
        setDecrypting(true);
        setResult("");
        setError("");

        try {
            const response = await decryptInput(selectedKey.keyId);

            if (!response.success) {
                setError("Failed to decrypt key");
                return;
            }
            
            setResult(response.data);
        } catch (err: any) {
            setError(`Failed to decrypt key: ${err.message ?? err}`);
        } finally {
            setDecrypting(false);
        }
    }

    async function HandleDeleteKey(selectedKey: RetrievePayloadResponse) {
        setDeleting(true);
        setResult("");
        setError("");

        try {
            const deleteKey = await deleteKeyById(selectedKey.keyId);

            if (!deleteKey.success) {
                setError("Failed to delete key");
                return;
            }

            setResult(deleteKey.data);
        } catch (err: any) {
            setError(`Failed to delete key: ${err.message ?? err}`);
        } finally {
            setDeleting(false);
        }
    }
    
    useEffect(() => {
        async function LoadKeys() {
            setLoadingKeys(true);
            setError("");

            try {
                const response = await retrieveAllKeys();
                
                setKeys(response);
            } catch (err: any) {
                setError("Failed to load keys");
            } finally {
                setLoadingKeys(false);
            }
        }
        LoadKeys();
    }, []);

    return (
        <div>
            <Card>
            <h2 className="pb-6">Keys</h2>
              <div className="flex justify-end gap-3 mb-8">
                <button className="rounded-md bg-emerald-800 px-4 py-2 font-medium text-white shadow-sm transition hover:bg-emerald-900 active:scale-95"
                  disabled={!selectedKey || decrypting}
                  onClick={() => {
                    if (selectedKey) {
                        HandleDecryptKey(selectedKey);
                    }
                  }}
                >
                  Decrypt
                </button>

                <button className="rounded-md bg-red-600 px-4 py-2 font-medium text-white shadow-sm transition hover:bg-red-700 active:scale-95"
                  disabled={!selectedKey || deleting}
                  onClick={() => {
                    if (selectedKey) {
                        HandleDeleteKey(selectedKey);
                    }
                  }}
                >
                  Delete
                </button>

              </div>

                {loadingKeys ? (
                    <p>Loading Keys...</p>
                ) : keys.length === 0 ? (
                    <p>No keys found</p>
                ) : (
                  <KeyTable
                    keys={keys}
                    selectedKey={selectedKey}
                    setSelectedKey={setSelectedKey}
                    showKeyId={showKeyId}
                    setShowKeyId={setShowKeyId}>
                  </KeyTable>
                )}

                {result && (
                  <div>
                    <p>{result}</p>
                  </div>
                )}

                {error && (
                  <div className="justify-self-center w-40 m-8 text-white">
                    <p className="p-2 ">{error}</p>
                  </div>
                )}

            </Card>
        </div>
    );
}

export default KeyManagement;