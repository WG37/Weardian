

function Navbar() {
    return (
        <nav className="sticky top-0 z-10 h-20 w-full border-b border-slate-700 bg-slate-900">
          <div className="flex h-full items-center justify-between px-6">
            <span>Settings</span>
            <span>Login</span>
          </div>
        </nav>
    )
}

export default Navbar;