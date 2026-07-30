import { useState, useEffect } from "react";
import { retrieveAllKeys, decryptInput, deleteKeyById } from "../bridge/WebViewBridge";
import type { RetrievePayloadResponse } from "../types/retrieve/RetrievePayloadResponse";
import Card from "../components/Card";
import KeyTable from "../components/KeyTable";
import Modal from "../components/modal/Modal";

function KeyManagement() {
  const [loadingKeys, setLoadingKeys] = useState(false);
  const [decrypting, setDecrypting] = useState(false);
  const [deleting, setDeleting] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const [result, setResult] = useState<string>("");

  const [keys, setKeys] = useState<RetrievePayloadResponse[]>([]);
  const [selectedKey, setSelectedKey] = useState<RetrievePayloadResponse | null>(null);
  const [showKeyId, setShowKeyId] = useState<string | null>(null);

  const [error, setError] = useState("");

  async function handleDecryptKey(selectedKey: RetrievePayloadResponse) {
    setDecrypting(true);
    setResult("");
    setError("");

    try {
      const response = await decryptInput(selectedKey.keyId);

      setResult(response);
    } catch (err: any) {
      setError(`Failed to decrypt key: ${err.message ?? err}`);
    } finally {
      setDecrypting(false);
    }
  }

  async function handleDeleteKey(selectedKey: RetrievePayloadResponse) {
    setDeleting(true);
    setResult("");
    setError("");

    try {
      const deleteKey = await deleteKeyById(selectedKey.keyId);

      setKeys((prev) => prev.filter((k) => k.keyId !== selectedKey.keyId));
      setSelectedKey(null);
      setIsModalOpen(false);

      setResult(deleteKey);
    } catch (err: any) {
      setError(`Failed to delete key: ${err.message ?? err}`);
    } finally {
      setDeleting(false);
    }
  }

  useEffect(() => {
    async function loadKeys() {
      setLoadingKeys(true);
      setError("");

      try {
        const response = await retrieveAllKeys();

        setKeys(response);
      } catch (err: any) {
        setError(`Failed to load keys: ${err.message ?? err}`);
      } finally {
        setLoadingKeys(false);
      }
    }
    loadKeys();
  }, []);

  return (
    <div>
      <Card>
        <h2 className="pb-6">Keys</h2>

        <div className="flex justify-end gap-6 mb-8">
          <button
            className="rounded-md bg-emerald-800 px-4 py-2 font-medium text-white shadow-sm transition hover:bg-emerald-900 active:scale-95"
            disabled={!selectedKey || decrypting}
            onClick={() => {
              if (selectedKey) {
                handleDecryptKey(selectedKey);
              }
            }}
          >
            Decrypt
          </button>

          <button
            className="rounded-md bg-red-600 px-4 py-2 font-medium text-white shadow-sm transition hover:bg-red-700 active:scale-95"
            disabled={!selectedKey || deleting}
            onClick={() => setIsModalOpen(true)}
          >
            Delete
          </button>
        </div>

        <Modal open={isModalOpen} onClose={() => setIsModalOpen(false)}>
          <p>
            Keys are deleted permanently. This cannot be undone. Are you sure you wish to delete?
          </p>
          <div>
            <button
              className="rounded-md bg-red-600 px-2 py-2 text-white"
              onClick={() => {
                if (selectedKey) {
                  handleDeleteKey(selectedKey);
                }
              }}
            >
              Yes
            </button>

            <button
              className="rounded-md bg-gray-600 px-2 py-2 text-white"
              onClick={() => setIsModalOpen(false)}
            >
              Cancel
            </button>
          </div>
        </Modal>

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
            setShowKeyId={setShowKeyId}
          />
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
