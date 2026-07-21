
interface Key {
  keyId: string;
  keyName: string;
  algorithm: string;
  createdOn: string;
}

interface KeyTableProps {
    keys: Key[];
    selectedKey: Key | null;
    setSelectedKey: (key: Key) => void;
    showKeyId: string | null;
    setShowKeyId: (keyId: string | null) => void;
}

function KeyTable({keys, selectedKey, setSelectedKey, showKeyId, setShowKeyId}: KeyTableProps) {
    return (
      <table className="min-w-full divide-y divide-gray-500">
        <thead className="bg-gray-800">
         <tr>
          <th className="px-4 py-3 text-left text-sm font-semibold">
            Key ID
          </th>
          <th className="px-4 py-3 text-left text-sm font-semibold">
            Name
          </th>
          <th className="px-4 py-3 text-left text-sm font-semibold">
            Algorithm
          </th>
          <th className="px-4 py-3 text-left text-sm font-semibold">
            Date Created
          </th>
         </tr>
        </thead>

        <tbody className="divide-y divide-gray-500 bg-slate-800">
          {keys.map((key) => (
            <tr
              className={`cursor-pointer transition-colors 
                ${selectedKey?.keyId === key.keyId
                  ? "bg-blue-700"
                  : "bg-gray-700"
                }`} 
              key={key.keyId}
              onClick={() => setSelectedKey(key)}
            >
              <td 
                className="px-4 py-3 text-blue-600 hover:underline"
                onClick={(e) => {
                  e.stopPropagation();

                  setShowKeyId(showKeyId === key.keyId ? null : key.keyId);
                }}
              >
                {showKeyId === key.keyId
                  ? key.keyId
                  : "Show Key ID"}
              </td>

              <td className="px-4 py-3">{key.keyName}</td>

              <td className="px-4 py-3">{key.algorithm}</td>

              <td className="px-4 py-3">{key.createdOn.slice(0, 10)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
}

export default KeyTable;