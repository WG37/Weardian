import LoginButton from "./button/LoginButton";
import RegisterButton from "./button/RegisterButton";

function Navbar() {
  return (
    <nav className="sticky top-0 z-10 h-20 w-full border-b border-slate-700 bg-slate-900">
      <div className="flex h-full items-center justify-between px-6">
        <RegisterButton />
        <LoginButton />
      </div>
    </nav>
  );
}

export default Navbar;
