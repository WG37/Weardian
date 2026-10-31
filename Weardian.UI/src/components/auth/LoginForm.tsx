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
        <div className="flex flex-col gap-6 p-6 bg-[#202020] rounded-2xl">
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
        <div className="flex justify-between pr-6 pl-6 pb-4 bg-[#202020] rounded-2xl">
          <button
            className="rounded-md bg-[#1b1b1b] p-2 text-white hover:bg-white hover:text-black active:bg-white active:text-black"
            type="submit"
          >
            Login
          </button>
          <button
            className="rounded-md bg-[#1b1b1b] p-2 text-white hover:bg-white hover:text-black active:bg-white active:text-black"
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
