import "./App.css"
import { useState } from "react";
import Sidebar from "./components/Sidebar/Sidebar";
import MainLayout from "./layouts/MainLayout"
import Home from "./pages/Home/Home";
import Encrypt from "./pages/Encrypt/Encrypt";
import Decrypt from "./pages/Decrypt/Decrypt";
import Retrieve from "./pages/Retrieve/Retrieve";

function App() {
    const [display, setDisplay] = useState("home");

    return (
        <div className="app">
            <Sidebar setDisplay={setDisplay} />
        
            <div className="main">
                {display === "home" && <Home />}
                {display === "encrypt" && <Encrypt />}
                {display === "decrypt" && <Decrypt />}
                {display === "retrieve" && <Retrieve />}
            </div>
        </div>
    );
}

export default App;