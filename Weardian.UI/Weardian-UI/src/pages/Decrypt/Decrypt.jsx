import "./Decrypt.css";
import { decryptInput } from "../../bridge/WebViewBridge";
import { useState } from "react";

function Decrypt() {

    const [keyId, setKeyId] = useState("");
    const [loading, setLoading] = useState(false);
    const [results, setResults] = useState("");

async function handleSubmit(e) {
    e.preventDefault();

    setLoading(true);
    setResults("");

    try {
        await decryptInput(keyId);

        setResults("Decrypted response");
    } catch (error) {
        console.error(error);

        setResults("Decryption failed");
    } finally {
        setLoading(false);
    }
}

    return (
        <div className="decryptPage-container">
            <h1 className="decrypt-title">Decrypt</h1>

            <form className="decryptForm-container" onSubmit={handleSubmit}>
                <input 
                    type="text" 
                    id="keyId-input" 
                    placeholder="Enter key Id"
                    value={keyId}
                    onChange={(e) => setKeyId(e.target.value)} 
                />
                <button 
                    type="submit" disabled={loading}>
                        {loading ? "Decrypting..." : "Decrypt"}
                    </button>
                    
                {results && ( <section 
                    className="decrypt-results-box" 
                    id="decrypt-result">
                    <h4>Results</h4>
                    <p>{results}</p>
                </section> 
                )}
            </form>
        </div>
    );
}

export default Decrypt;