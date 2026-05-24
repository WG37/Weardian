import "./Retrieve.css";
import { retrieveKey, retrieveKeys, deleteKeyById, decryptInput } from "../../bridge/WebViewBridge";
import { use, useEffect, useState } from "react";
import KeysTable from "../../components/Keys/KeysTable";

function Retrieve() {

    const [keyId, setKeyId] = useState("");
    const [keys, setKeys] = useState([]);
    const [selectedKey, setSelectedKey] = useState(null);
    const [loading, setLoading] = useState(false);
    const [result, setResult] = useState("");
    const [error, setError] = useState("");

    async function handleRetrieveKey(e) {
        e.preventDefault();

        setLoading(true);
        setError("");

        try {
            await retrieveKey(keyId);

            setResult("Key retrieved");
            setKeyId("");

        } catch (error) {
            console.error(error);

            setError("Failed to retrieve key");
            setKeyId("");

        } finally {
            setLoading(false);
        }
    }

    async function handleDecryptKey(selectedKeyId) {

        setLoading(true);
        setError("");

        try {
            await decryptInput(selectedKeyId);
            setResult("Key deleted");

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
            <h1 className="keyBox-title">Key Retrieval</h1>

            <form className="retrievalControls-container" onSubmit={handleRetrieveKey}>
                <input
                    className="retrieve-input"
                    type="text"
                    placeholder="Enter key id"
                    value={keyId}
                    onChange={(e) => setKeyId(e.target.value)}
                />

                <div className="retrieveButtons-container">
                    <button 
                        className="retrieve-button" 
                        type="submit">
                        Retrieve Key
                    </button>

                    <button 
                        className="delete-button"
                        type="button"
                        disabled={!selectedKey || loading}
                        onClick={() => handleDeletekey(selectedKey.id)}>
                        Delete Key
                    </button>

                    <button 
                        className="decrypt-button"
                        type="button"
                        disabled={!selectedKey || loading}
                        onClick={() => handleDecryptKey(selectedKey.id)}>
                        Decrypt Key
                    </button>

                </div>
            </form>

            <div className="keyRetrieval-box">
                {loading && <p>Loading...</p>}

                {error && <p>{error}</p>}
                
                {!loading && keys.length > 0 && (
                    <KeysTable 
                        keys={keys}
                        selectedKey={selectedKey}
                        setSelectedKey={setSelectedKey} />
                )}
            </div>
        </div>
    );
}

export default Retrieve;