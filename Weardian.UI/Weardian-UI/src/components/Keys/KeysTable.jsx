import "./KeysTable.css";

export default function KeysTable({ 
    keys, 
    selectedKey,
    setSelectedKey, 
    showKeyId, 
    setShowKeyId }) {
    return (
        <table className="keysTable">
            <thead>
              <tr>
               <th>Id</th>
               <th>Name</th>
               <th>Algorithm</th>
               <th>CreatedOn</th>
              </tr>
            </thead>

            <tbody>
                {keys.map((key) => (
                    <tr key={key.keyId}
                        onClick={() => setSelectedKey(key)}
                        className={selectedKey?.keyId === key.keyId ? "selected-row" : ""}>
                        <td onClick={(e) => {
                            e.stopPropagation();

                            setShowKeyId(
                                showKeyId === key.keyId
                                    ? null : key.keyId 
                            );
                        }}>{showKeyId === key.keyId
                            ? key.keyId : "Show Key ID"}</td>
                            
                        <td>{key.name}</td>
                        <td>{key.algorithm}</td>
                        <td>{key.createdOn.slice(0,10)}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}