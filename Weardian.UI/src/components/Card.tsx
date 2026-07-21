import type React from "react";

interface CardProps {
    children: React.ReactNode;
}

function Card({children}: CardProps) {
    return (
        <div className="rounded-xl bg-slate-800 p-6 shadow">
            {children}
        </div>
    );
}

export default Card;