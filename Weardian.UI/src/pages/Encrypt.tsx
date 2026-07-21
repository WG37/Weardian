import React, { useState } from "react";
import Card from "../components/Card";
import { encryptInput } from "../bridge/WebViewBridge";
import type { EncryptResponse } from "../types/encryption/EncryptResponse";

function Encrypt() {
  const [sync, setSync] = useState(false);
  const [keyName, setKeyName] = useState("");
  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  
  const [result, setResult] = useState<EncryptResponse | null>(null);

    async function HandleSubmit(e: React.SubmitEvent<HTMLFormElement>) {
        e.preventDefault();
        setLoading(true);
        setError("");
        setResult(null);

        try {
            const response = await encryptInput(keyName, password, sync);

            setResult(response);
            setKeyName("");
            setPassword("");
            setSync(false);
        } catch (err: any) {
            setError(`Failed to encrypt: ${err.message ?? err}`);
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className="p-8">
           <Card>
              <h2 className="mb-2 pb-4 text-2x1 font-semibold text-slate-800">Encrypt</h2>
              <form 
                onSubmit={HandleSubmit}
                className="space-y-5"
              >
                <input 
                  className="w-full rounded-lg border border-grey-300 p-3 focus:border-blue-500 focus:outline-none"
                  type="text"
                  placeholder="Enter a name for your key"
                  value={keyName}
                  onChange={(e) => setKeyName(e.currentTarget.value)}
                />
                
                <input
                  className="w-full rounded-lg border border-grey-300 p-3 focus:border-blue-500 focus:outline-none"
                  type="password"
                  placeholder="Enter a password to encrypt"
                  value={password}
                  onChange={(e) => setPassword(e.currentTarget.value)}
                />

                <label className="flex items-center gap-2">
                  <input
                    type="checkbox"
                    checked={sync}
                    onChange={(e) => setSync(e.currentTarget.checked)}
                  />
                  Sync
                </label>

                <button
                  className="w-full rounded-lg bg-blue-900 px-4 py-3 font-medium text-white transform hover:bg-blue-700 disabled:bg-gray-400"
                  type="submit"
                  disabled={loading}
                >
                  {loading ? "Encrypting..." : "Encrypt"}
                </button>

                {result && (
                  <div className="rounded-lg border border-green-200 bg-slate-800 p-4 text-green-700">
                    <p><strong>Key Name:</strong> {result.keyName}</p>
                    <p><strong>Key ID:</strong> {result.keyId}</p>
                    <p><strong>Algorithm:</strong> {result.algorithm}</p>
                  </div>
                )}

                {error && (
                  <div className="rounded-lg border border-red-200 bg-slate-800 p-4 text-red-700">
                    {error}
                  </div>
                )}
              </form>
           </Card>
        </div>
    );
}

export default Encrypt;