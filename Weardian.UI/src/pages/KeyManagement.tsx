import { useState, useEffect } from "react";
import { retrieveAllKeys, decryptInput, deleteKeysByIds } from "../bridge/WebViewBridge";
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
  const [selectedKeys, setSelectedKeys] = useState<RetrievePayloadResponse[]>([]);
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

  async function handleDeleteKey(selectedKeys: RetrievePayloadResponse[]) {
    setDeleting(true);
    setResult("");
    setError("");

    try {
      const keyIds = selectedKeys.map((key) => key.keyId);
      const deletedKeys = await deleteKeysByIds(keyIds);

      setKeys((prev) =>
        prev.filter((key) => !selectedKeys.some((selectedKey) => selectedKey.keyId === key.keyId)),
      );
      setSelectedKeys([]);
      setIsModalOpen(false);

      setResult(
        `${deletedKeys.length} key${deletedKeys.length === 1 ? "" : "s"} successfully deleted`,
      );
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
            disabled={selectedKeys.length !== 1 || decrypting}
            onClick={() => {
              if (selectedKeys.length === 1) {
                handleDecryptKey(selectedKeys[0]);
              }
            }}
          >
            Decrypt
          </button>

          <button
            className="rounded-md bg-red-600 px-4 py-2 font-medium text-white shadow-sm transition hover:bg-red-700 active:scale-95"
            disabled={!selectedKeys || deleting}
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
                if (selectedKeys.length > 0) {
                  handleDeleteKey(selectedKeys);
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
            selectedKeys={selectedKeys}
            setSelectedKeys={setSelectedKeys}
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
