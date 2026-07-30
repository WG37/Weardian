import { useState } from "react";
import AuthModal from "../modal/AuthModal";

function LogoutButton() {
  const [open, setOpen] = useState(false);

  return (
    <>
      <button
        className="rounded-lg border border-slate-500 px-2 text-white hover:border-slate-300 active:border-slate-700"
        onClick={() => setOpen(true)}
      >
        Logout
      </button>
      <AuthModal open={open} mode="logout" onClose={() => setOpen(false)}></AuthModal>
    </>
  );
}

export default LogoutButton;
