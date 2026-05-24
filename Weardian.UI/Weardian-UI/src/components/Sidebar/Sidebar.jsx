import "./sidebar.css";

function Sidebar({ setDisplay }) {
    return (
        <div className="sidebar">
            <h2>Weardian</h2>

            <button className="sidebar-button" onClick={() => setDisplay("home")}>
                Home
            </button>

            <button className="sidebar-button" onClick={() => setDisplay("encrypt")}>
                Encrypt
            </button>

            <button className="sidebar-button" onClick={() => setDisplay("decrypt")}>
                Decrypt
            </button>

            <button className="sidebar-button" onClick={() => setDisplay("retrieve")}>
                Retrieve
            </button>

            <button className="sidebar-button" onClick={() => setDisplay("delete")}>
                Delete
            </button>
        </div>
    );
}

export default Sidebar;