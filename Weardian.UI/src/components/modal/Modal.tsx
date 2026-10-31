interface ModalProps {
  open: boolean;
  onClose: () => void;
  children: React.ReactNode;
}

function Modal({ open, onClose, children }: ModalProps) {
  if (!open) {
    return null;
  }

  return (
    <div
      className="fixed inset-0 flex items-center justify-center bg-black/40 backdrop-blur-sm"
      onClick={onClose}
    >
      <div
        className="relative rounded-lg border bg-[#202020] "
        onClick={(e) => e.stopPropagation()}
      >
        <button
          className="absolute top-2 right-2 pr-1 pl-1 text-white hover:rounded-full hover:bg-red-500 active:scale-95"
          onClick={onClose}
        >
          ✕
        </button>
        {children}
      </div>
    </div>
  );
}

export default Modal;
