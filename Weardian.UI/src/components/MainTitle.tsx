import { useEffect, useRef, useState } from "react";

const SYMBOLS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#$%&";

type LetterProps = {
  letter: string;
};

function CipherLetter({ letter }: LetterProps) {
  const [display, setDisplay] = useState(letter);
  const [active, setActive] = useState(false);
  const frameRef = useRef<number | null>(null);

  const scramble = () => {
    if (frameRef.current !== null) {
      cancelAnimationFrame(frameRef.current);
    }

    const duration = 420;
    const interval = 32;
    const start = performance.now();
    let lastUpdate = 0;

    setActive(true);

    const animate = (time: number) => {
      const elapsed = time - start;
      const progress = Math.min(elapsed / duration, 1);

      if (progress >= 1) {
        setDisplay(letter);
        setActive(false);
        frameRef.current = null;
        return;
      }

      if (time - lastUpdate >= interval) {
        lastUpdate = time;

        const index = Math.floor(Math.random() * SYMBOLS.length);

        setDisplay(SYMBOLS[index]);
      }

      frameRef.current = requestAnimationFrame(animate);
    };

    frameRef.current = requestAnimationFrame(animate);
  };

  useEffect(() => {
    return () => {
      if (frameRef.current !== null) {
        cancelAnimationFrame(frameRef.current);
      }
    };
  }, []);

  return (
    <span
      onMouseEnter={scramble}
      className="relative inline-block cursor-default"
    >
      <span className="invisible">{letter}</span>

      <span
        className={`
          absolute inset-0 flex items-center justify-center
          transition-all duration-150
          ${active ? "text-green-400" : "text-white"}
        `}
      >
        {display}
      </span>
    </span>
  );
}

export default function MainTitle({ text = "Weardian" }: { text?: string }) {
  return (
    <h2 className="text-2xl font-bold">
      {text.split("").map((letter, index) => (
        <CipherLetter key={index} letter={letter} />
      ))}
    </h2>
  );
}
