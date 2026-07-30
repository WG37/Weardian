import { useContext } from "react";
import { AuthContext } from "./AuthContext";

function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth function must be used within AuthProvider");
  }

  return context;
}

export default useAuth;
