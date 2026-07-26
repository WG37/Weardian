import { useState } from "react";
import AuthModal from "../modal/AuthModal";

function LoginButton() {
  const [open, setOpen] = useState(false);

  return (
    <>
      <button onClick={() => setOpen(true)}>Login</button>
      <AuthModal open={open} mode="login" onClose={() => setOpen(false)} />
    </>
  );
}

export default LoginButton;
