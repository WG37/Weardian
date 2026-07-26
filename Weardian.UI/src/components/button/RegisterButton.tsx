import { useState } from "react";
import AuthModal from "../modal/AuthModal";

function RegisterButton() {
  const [open, setOpen] = useState(false);

  return (
    <>
      <button onClick={() => setOpen(true)}>Register</button>
      <AuthModal open={open} mode="register" onClose={() => setOpen(false)} />
    </>
  );
}

export default RegisterButton;
