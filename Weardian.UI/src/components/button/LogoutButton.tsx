import { useState } from "react";
import AuthModal from "../modal/AuthModal";

function LogoutButton() {
  const [open, setOpen] = useState(false);

  return (
    <>
      <button
        className="rounded-lg border bg-[#1b1b1b] px-2 text-white hover:bg-white hover:text-black active:bg-white active:text-black"
        onClick={() => setOpen(true)}
      >
        Logout
      </button>
      <AuthModal open={open} mode="logout" onClose={() => setOpen(false)}></AuthModal>
    </>
  );
}

export default LogoutButton;
