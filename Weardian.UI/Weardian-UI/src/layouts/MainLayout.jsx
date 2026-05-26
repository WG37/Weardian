import "./MainLayout.css";

function MainContent({content}) {
    return (
        <div className="mainContent">
            <h3>{content}</h3>
        </div>
    );
}

export default MainContent;