interface SidebarProps {
  setDisplay: (display: string) => void;
}

function Sidebar({ setDisplay }: SidebarProps) {
  return (
    <aside className="flex h-screen w-64 flex-col bg-[#1b1b1b] text-white border-r rounded-4xl rounded-tr-none">
      <div className="flex h-20 border-b border-white p-6">
        <h2 className="text-2xl font-bold">Weardian</h2>
      </div>

      <nav className="flex flex-1 flex-col gap-2 p-4">
        <button
          className="rounded-lg px-4 py-3 text-left transition bg-[#1b1b1b] text-white hover:bg-white hover:text-black active:bg-white active:text-black"
          onClick={() => setDisplay("home")}
        >
          Home
        </button>

        <button
          className="rounded-lg px-4 py-3 text-left transition bg-[#1b1b1b] text-white hover:bg-white hover:text-black active:bg-white active:text-black"
          onClick={() => setDisplay("encrypt")}
        >
          Encrypt
        </button>

        <button
          className="rounded-lg px-4 py-3 text-left transitionbg-[#1b1b1b] text-white hover:bg-white hover:text-black active:bg-white active:text-black"
          onClick={() => setDisplay("keyManagement")}
        >
          Key Management
        </button>
      </nav>
    </aside>
  );
}

export default Sidebar;
