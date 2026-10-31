import { useState } from "react";
import AuthModal from "../modal/AuthModal";

function LoginButton() {
  const [open, setOpen] = useState(false);

  return (
    <>
      <button
        className="rounded-lg border bg-[#1b1b1b] px-2 text-white hover:bg-white hover:text-black active:bg-white active:text-black"
        onClick={() => setOpen(true)}
      >
        Login
      </button>
      <AuthModal open={open} mode="login" onClose={() => setOpen(false)} />
    </>
  );
}

export default LoginButton;
