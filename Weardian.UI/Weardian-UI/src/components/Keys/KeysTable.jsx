export default function KeysTable({ keys, selectedKey ,setSelectedKey }) {
    return (
        <table>
            <thead>
              <tr>
               <th>Id</th>
               <th>Name</th>
               <th>Algorithm</th>
               <th>CreatedOn</th>
              </tr>
            </thead>

            <tbody>
                {keys?.map((key) => (
                    <tr key={key.keyId}
                        onClick={() => setSelectedKey(key)}
                        className={selectedKey?.id === key.id ? "selected-row" : ""}>
                        <td>{key.Id}</td>
                        <td>{key.name}</td>
                        <td>{key.algorithm}</td>
                        <td>{key.createdOn}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}