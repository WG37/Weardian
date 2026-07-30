import { useState } from "react";
import AuthModal from "../modal/AuthModal";

function LoginButton() {
  const [open, setOpen] = useState(false);

  return (
    <>
      <button
        className="rounded-lg border border-slate-500 px-2 text-white hover:border-slate-300 active:border-slate-700"
        onClick={() => setOpen(true)}
      >
        Login
      </button>
      <AuthModal open={open} mode="login" onClose={() => setOpen(false)} />
    </>
  );
}

export default LoginButton;
