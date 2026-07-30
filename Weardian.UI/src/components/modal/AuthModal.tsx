import LoginForm from "../auth/LoginForm";
import LogoutForm from "../auth/LogoutForm";
import RegisterForm from "../auth/RegisterForm";
import Modal from "./Modal";

export type AuthFormMode = "register" | "login" | "logout";

interface AuthModalProps {
  open: boolean;
  mode: AuthFormMode;
  onClose: () => void;
}

function AuthModal({ open, mode, onClose }: AuthModalProps) {
  return (
    <Modal open={open} onClose={onClose}>
      {mode === "register" && <RegisterForm onSuccess={onClose} onClose={onClose} />}
      {mode === "login" && <LoginForm onSuccess={onClose} onClose={onClose} />}
      {mode == "logout" && <LogoutForm onSuccess={onClose} onClose={onClose} />}
    </Modal>
  );
}

export default AuthModal;
