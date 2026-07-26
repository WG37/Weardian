import type { ReactNode } from "react";
import Sidebar from "../components/Sidebar";
import Navbar from "../components/Navbar";

interface MainLayoutProps {
  children: ReactNode;
  setDisplay: (display: string) => void;
}

function MainLayout({ children, setDisplay }: MainLayoutProps) {
  return (
    <div className="flex h-screen">
      <Sidebar setDisplay={setDisplay} />

      <div className="flex flex-1 flex-col">
        <Navbar />

        <main className="flex-1 overflow-auto p-6">{children}</main>
      </div>
    </div>
  );
}

export default MainLayout;
