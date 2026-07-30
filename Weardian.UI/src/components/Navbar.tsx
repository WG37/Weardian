import useAuth from "../auth/useAuth";
import LoginButton from "./button/LoginButton";
import LogoutButton from "./button/LogoutButton";
import RegisterButton from "./button/RegisterButton";

function Navbar() {
  const { isAuthenticated } = useAuth();

  return (
    <nav className="sticky top-0 z-10 h-20 w-full border-b border-slate-700 bg-slate-900">
      <div className="flex h-full items-center justify-end px-6">
        {isAuthenticated ? (
          <LogoutButton />
        ) : (
          <div className="flex gap-5">
            <RegisterButton />
            <LoginButton />
          </div>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
