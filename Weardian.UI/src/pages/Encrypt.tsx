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

  async function handleSubmit(e: React.SubmitEvent<HTMLFormElement>) {
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
        <h2 className="mb-2 pb-10 text-2xl font-semibold">Encrypt Password</h2>
        <form onSubmit={handleSubmit} className="flex flex-col items-center gap-5">
          <div className="flex flex-col gap-5 pb-10 w-full max-w-xs">
            <input
              className="w-full rounded-lg border border-gray-300 p-3 focus:border-white focus:outline-none"
              type="text"
              placeholder="Enter a name for your key"
              value={keyName}
              onChange={(e) => setKeyName(e.currentTarget.value)}
            />

            <input
              className="w-full rounded-lg border border-gray-300 p-3 focus:border-white focus:outline-none"
              type="password"
              placeholder="Enter a password to encrypt"
              value={password}
              onChange={(e) => setPassword(e.currentTarget.value)}
            />

            <label className="flex items-center gap-2 cursor-pointer">
              <input
                type="checkbox"
                checked={sync}
                onChange={(e) => setSync(e.currentTarget.checked)}
                className="appearance-none w-5 h-5 rounded-full border-2 border-white
               checked:bg-green-600
               hover:scale-110 transition-transform duration-200
               cursor-pointer"
              />
              Sync
            </label>
          </div>

          <button
            className="w-full max-w-48 rounded-lg bg-[#f0f0f0] px-4 py-3 font-medium text-black hover:bg-white disabled:bg-gray-400 cursor-pointer"
            type="submit"
            disabled={loading}
          >
            {loading ? "Encrypting..." : "Encrypt"}
          </button>

          {result && (
            <div className="rounded-lg border border-white bg-[3d3d3d] p-4 text-white">
              <p>
                <strong>Key Name:</strong> {result.keyName}
              </p>
              <p>
                <strong>Key ID:</strong> {result.keyId}
              </p>
              <p>
                <strong>Algorithm:</strong> {result.algorithm}
              </p>
            </div>
          )}

          {error && (
            <div className="rounded-lg border  border-white bg-[3d3d3d] p-4 text-white">
              {error}
            </div>
          )}
        </form>
      </Card>
    </div>
  );
}

export default Encrypt;
