import { useState } from "react";
import useAuth from "../../auth/useAuth";
import { logoutUser } from "../../bridge/WebViewBridge";

interface LogoutFormProps {
  onSuccess: () => void;
  onClose: () => void;
}

function LogoutForm({ onSuccess, onClose }: LogoutFormProps) {
  const { logout } = useAuth();

  const [error, setError] = useState("");

  async function handleLogout() {
    try {
      const response = await logoutUser();

      if (!response.isSuccessful) {
        setError("Failed to logout");
        return;
      }

      onSuccess();
      logout();
    } catch (err: any) {
      setError("Something went wrong. Please try again");
    }
  }

  return (
    <div>
      <p className="text-white">Are you sure you wish to logout?</p>
      <div className="flex justify-between gap-6 mb-8">
        <button
          className="rounded-md bg-red-600 px-2 py-2 text-white hover:bg-red-700 active:scale-95"
          onClick={handleLogout}
        >
          Logout
        </button>

        <button
          className="rounded-md bg-gray-600 px-2 py-2 text-white hover:bg-gray-700 active:scale-95"
          onClick={onClose}
        >
          Cancel
        </button>
        {error && <p>{error}</p>}
      </div>
    </div>
  );
}

export default LogoutForm;
