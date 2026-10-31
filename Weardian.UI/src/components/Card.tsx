import type React from "react";

interface CardProps {
  children: React.ReactNode;
}

function Card({ children }: CardProps) {
  return <div className="p-5 bg-[#1b1b1b] text-white rounded-2xl">{children}</div>;
}

export default Card;
