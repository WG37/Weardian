interface Key {
  keyId: string;
  keyName: string;
  algorithm: string;
  createdOn: string;
}

interface KeyTableProps {
  keys: Key[];
  selectedKeys: Key[];
  setSelectedKeys: React.Dispatch<React.SetStateAction<Key[]>>;
  showKeyId: string | null;
  setShowKeyId: (keyId: string | null) => void;
}

function KeyTable({ keys, selectedKeys, setSelectedKeys, showKeyId, setShowKeyId }: KeyTableProps) {
  const allSelectedKeys = keys.length > 0 && selectedKeys.length === keys.length;

  function toggleKey(key: Key) {
    setSelectedKeys((prev) =>
      prev.some((k) => k.keyId === key.keyId)
        ? prev.filter((k) => k.keyId !== key.keyId)
        : [...prev, key],
    );
  }

  function toggleAllKeys() {
    if (allSelectedKeys) {
      setSelectedKeys([]);
    } else {
      setSelectedKeys([...keys]);
    }
  }

  return (
    <table className="min-w-full divide-y divide-gray-500">
      <thead className="bg-gray-800">
        <tr>
          <th className="px-4 py-3">
            <input type="checkbox" checked={allSelectedKeys} onChange={toggleAllKeys} />
          </th>

          <th className="px-4 py-3 text-left text-sm font-semibold">Key ID</th>
          <th className="px-4 py-3 text-left text-sm font-semibold">Name</th>
          <th className="px-4 py-3 text-left text-sm font-semibold">Algorithm</th>
          <th className="px-4 py-3 text-left text-sm font-semibold">Date Created</th>
        </tr>
      </thead>

      <tbody className="divide-y divide-gray-500 bg-slate-800">
        {keys.map((key) => {
          const selectedKey = selectedKeys.some((selected) => selected.keyId === key.keyId);

          return (
            <tr
              key={key.keyId}
              className={`transition-colors ${selectedKey ? "bg-blue-700" : "bg-gray-700"}`}
            >
              <td className="px-4 py-3">
                <input type="checkbox" checked={selectedKey} onChange={() => toggleKey(key)} />
              </td>

              <td
                className="px-4 py-3 text-blue-600 hover:underline cursor-pointer"
                onClick={() => setShowKeyId(showKeyId === key.keyId ? null : key.keyId)}
              >
                {showKeyId === key.keyId ? key.keyId : "Show Key ID"}
              </td>

              <td className="px-4 py-3">{key.keyName}</td>
              <td className="px-4 py-3">{key.algorithm}</td>
              <td className="px-4 py-3">{key.createdOn.slice(0, 10)}</td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

export default KeyTable;
