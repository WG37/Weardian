import React, { useState } from "react";
import { registerUser } from "../../bridge/WebViewBridge";

interface RegisterFormProps {
  onSuccess: () => void;
  onClose: () => void;
}

function RegisterForm({ onSuccess, onClose }: RegisterFormProps) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [error, setError] = useState("");

  async function handleSubmit(e: React.SubmitEvent<HTMLFormElement>) {
    e.preventDefault();

    try {
      const response = await registerUser(email, password);

      if (!response.isSuccessful) {
        setError(response.error ?? "Registration Failed.");
        return;
      }

      onSuccess();
    } catch (err: any) {
      setError("Something went wrong. Please try again");
    }
  }

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div className="flex flex-col gap-6 p-6">
          <label className="flex flex-col items-start gap-1">
            <span className="text-white hover:text-gray-300">Email</span>
            <input
              className="rounded-md border bg-[#1b1b1b] px-2 text-white"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.currentTarget.value)}
            />
          </label>
          <label className="flex flex-col items-start gap-1">
            <span className="text-white hover:text-gray-300">Password</span>
            <input
              className="rounded-md border bg-[#1b1b1b] px-2 text-white"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.currentTarget.value)}
            />
          </label>
        </div>
        <div className="flex justify-between pr-4 pl-4 pb-4">
          <button
            className="rounded-md bg-[#1b1b1b] px-2 text-white hover:bg-white hover:text-black active:bg-white active:text-black"
            type="submit"
          >
            Register
          </button>
          <button
            className="rounded-md bg-[#1b1b1b] px-2 text-white hover:bg-white hover:text-black active:bg-white active:text-black"
            onClick={onClose}
          >
            Cancel
          </button>
        </div>
      </form>
      <div className="flex justify-center">
        {error && <p className="mx-auto max-w-48 text-center">{error}</p>}
      </div>
    </div>
  );
}

export default RegisterForm;
