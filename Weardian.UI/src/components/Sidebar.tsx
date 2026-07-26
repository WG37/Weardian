interface SidebarProps {
  setDisplay: (display: string) => void;
}

function Sidebar({ setDisplay }: SidebarProps) {
  return (
    <aside className="flex h-screen w-64 flex-col bg-slate-900 text-white">
      <div className="flex h-20 border-b border-slate-700 p-6">
        <h2 className="text-2xl font-bold">Weardian</h2>
      </div>

      <nav className="flex flex-1 flex-col gap-2 p-4">
        <button
          className="rounded-lg px-4 py-3 text-left transition hover:bg-slate-800"
          onClick={() => setDisplay("home")}
        >
          Home
        </button>

        <button
          className="rounded-lg px-4 py-3 text-left transition hover:bg-slate-800"
          onClick={() => setDisplay("encrypt")}
        >
          Encrypt
        </button>

        <button
          className="rounded-lg px-4 py-3 text-left transition hover:bg-slate-800"
          onClick={() => setDisplay("keyManagement")}
        >
          Key Management
        </button>
      </nav>
    </aside>
  );
}

export default Sidebar;
