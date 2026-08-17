import { useState } from "react";
import { loginUser } from "../../bridge/WebViewBridge";
import useAuth from "../../auth/useAuth";

interface LoginFormProps {
  onSuccess: () => void;
  onClose: () => void;
}

function LoginForm({ onSuccess, onClose }: LoginFormProps) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [error, setError] = useState("");

  const { login } = useAuth();

  async function handleSubmit(e: React.SubmitEvent<HTMLFormElement>) {
    e.preventDefault();

    try {
      const response = await loginUser(email, password);

      if (!response.isSuccessful) {
        setError(response.error ?? "Login Failed");
        return;
      }

      onSuccess();
      login();
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
              className="rounded-md border border-slate-400 bg-slate-700 px-2 py-0.5 text-white  hover:bg-slate-800"
              type="email"
              value={email}
              onChange={(e) => setEmail(e.currentTarget.value)}
            />
          </label>
          <label className="flex flex-col items-start gap-1">
            <span className="text-white hover:text-gray-300">Password</span>
            <input
              className="rounded-md border border-slate-400 bg-slate-700 px-2 py-0.5 text-white hover:bg-slate-800"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.currentTarget.value)}
            />
          </label>
        </div>
        <div className="flex justify-between pr-6 pl-6 pb-4">
          <button
            className="rounded-md border border-slate-400 bg-slate-800 px-4 py-0.5 text-white hover:border-white hover:text-white"
            type="submit"
          >
            Login
          </button>
          <button
            className="rounded-md border border-slate-400 bg-slate-800 px-4 py-0.5 text-white hover:border-white hover:text-white"
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

export default LoginForm;
