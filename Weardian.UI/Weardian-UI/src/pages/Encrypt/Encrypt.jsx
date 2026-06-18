import "./Encrypt.css";
import { encryptInput } from "../../bridge/WebViewBridge";
import { useState } from "react";

function Encrypt() {

    const [keyName, setKeyName] = useState("");
    const [password, setPassword] = useState("");
    const [sync, setSync] = useState(false);
    const [result, setResult] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    async function handleSubmit(e) {
        e.preventDefault();

        setLoading(true);
        setResult("");

        try {
            await encryptInput(keyName, password, sync);
            setResult("Encryption successful");
            setKeyName("");
            setPassword("");
            setSync(false);
            
        } catch (err) {
            console.error(err);
            setError(`Failed to encrypt: ${err}`);
        }
        finally {
            setLoading(false);
        }
    }

    return (
        <div className="encryptPage-container">
            <h1 className="encrypt-title">Encrypt</h1>

            <form className="encryptForm-container" onSubmit={handleSubmit}>
                <input 
                    type="text" 
                    placeholder="Enter a name for your key" 
                    value={keyName}
                    onChange={(e) => setKeyName(e.target.value)}
                />
                
                <input 
                    type="password"
                    placeholder="Enter password to encrypt" 
                    value={password}
                    onChange={(e) => setPassword(e.target.value)} 
                />

                <button 
                    type="submit"
                    disabled={loading}>
                        {loading ? "Encrypting..." : "Encrypt"}
                </button>
                
                <label>Sync
                    <input
                        type="checkbox"
                        checked={sync}
                        onChange={(e) => setSync(e.target.checked)}
                    />
                </label>

                {result && 
                    (<div className="encrypt-result-box">
                        <h3>Results</h3>
                        <div className="key-result">
                            <p>{result}</p>
                        </div>
                    </div>
                )}
            </form>
        </div>
    );
}

export default Encrypt;