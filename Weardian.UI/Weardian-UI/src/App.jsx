import "./App.css"
import { useState } from "react";
import Sidebar from "./components/Sidebar/Sidebar";
import MainLayout from "./layouts/MainLayout"
import Home from "./pages/Home/Home";
import Encrypt from "./pages/Encrypt/Encrypt";
import KeyManagement from "./pages/KeyManagement/KeyManagement";

function App() {
    const [display, setDisplay] = useState("home");

    return (
        <div className="app">
            <Sidebar setDisplay={setDisplay} />
        
            <div className="main">
                {display === "home" && <Home />}
                {display === "encrypt" && <Encrypt />}
                {display === "keyManagement" && <KeyManagement />}
            </div>
        </div>
    );
}

export default App;