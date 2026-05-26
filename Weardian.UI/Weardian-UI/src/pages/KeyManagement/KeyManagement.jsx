import "./KeyManagement.css";
import { retrieveKey, retrieveKeys, deleteKeyById, decryptInput } from "../../bridge/WebViewBridge";
import { use, useEffect, useState } from "react";
import KeysTable from "../../components/Keys/KeysTable";

function Retrieve() {

    const [keys, setKeys] = useState([]);
    const [selectedKey, setSelectedKey] = useState(null);
    const [showKeyId, setShowKeyId] = useState(null);

    const [searchTerm, setSearchTerm] = useState("");

    const [loading, setLoading] = useState(false);
    const [result, setResult] = useState("");
    const [error, setError] = useState("");

    const filteredKeys = keys.filter((key) => {
        const query = searchTerm.toLowerCase();

        return (
            key.keyId?.toLowerCase().includes(query) ||
            key.name?.toLowerCase().includes(query)
        );
    });

    async function handleDecryptKey(selectedKeyId) {
        setLoading(true);
        setError("");

        try {
            const keyResult = await decryptInput(selectedKeyId);
            setResult(keyResult);
        } catch (err) {
            console.error(err);
            setError(`Failed to decrypt: ${err}`);
        } finally {
            setLoading(false);
        }
    }

    async function handleDeleteKey(selectedKeyId) {
        setLoading(true);
        setError("");

        try {
            await deleteKeyById(selectedKeyId)
            setKeys(currentKeys => currentKeys.filter(key => key.keyId !== selectedKeyId));
            setSelectedKey(null);
        } catch (error) {
            console.error(error);
            setError("Failed to delete key");
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        async function loadKeys() {
            setLoading(true);
    
            try {    
                const keyResults = await retrieveKeys();
                setKeys(keyResults);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        }

        loadKeys();

    }, []);

    return (
        <div className="keyRetrieval-container">
            <div className="keyRetrieval-left">    
                <h1 className="keyBox-title">Key Management</h1>

                <div className="retrievalControls-container">
                    <input
                        className="retrieve-input"
                        type="text"
                        placeholder="Search for key name or key id"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />

                    <div className="retrieveButtons-container">
                        <button 
                            className="delete-button"
                            type="button"
                            disabled={!selectedKey || loading}
                            onClick={() => handleDeleteKey(selectedKey.keyId)}>
                            Delete Key
                        </button>

                        <button 
                            className="decrypt-button"
                            type="button"
                            disabled={!selectedKey || loading}
                            onClick={() => handleDecryptKey(selectedKey.keyId)}>
                            Decrypt Key
                        </button>

                    </div>
                </div>
                <div className="keyRetrieval-box">
                    {loading && <p>Loading...</p>}

                    {error && <p>{error}</p>}
                    
                    {!loading && (
                        <KeysTable 
                        keys={filteredKeys}
                        selectedKey={selectedKey}
                        setSelectedKey={setSelectedKey}
                        showKeyId={showKeyId}
                        setShowKeyId={setShowKeyId} />
                    )}
                </div>
            </div>

            {result && (
                <div className="keyDetails-container">
                <h3>Key Details</h3>
                <p>{result}</p>
            </div>
            )}
        </div>
    );
}

export default Retrieve;