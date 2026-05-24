export default function KeysTable({ keys, selectedKey ,setSelectedKey }) {
    return (
        <table>
            <thead>
              <tr>
               <th>Name</th>
               <th>Id</th>
               <th>Synced</th>
               <th>CreatedOn</th>
              </tr>
            </thead>

            <tbody>
                {keys.map((key) => (
                    <tr key={key.id}
                        onClick={() => setSelectedKey(key)}
                        className={selectedKey?.id === key.id ? "selected-row" : ""}>
                        <td>{key.name}</td>
                        <td>{key.id}</td>
                        <td>{key.synced ? "Synced" : "Not Synced"}</td>
                        <td>{key.createdOn}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}