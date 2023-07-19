import React from 'react';
import { IFieldConfigCard } from './fieldConfigCard';

export default function Card({ card }: { card: IFieldConfigCard }) {
  const { bg, title, price, timeClose } = card;
  return (
    <div className="card bg-[#152D2D] bg-slate-700 w-60 rounded-lg m-2">
      <div className="card-img flex justify-center ">
        <img src={bg} alt="bg" className="w-56 m-2" />
      </div>
      <div className="card-info ml-4">
        <div className="card-title text-white text-sm not-italic font-semibold">
          {title}
        </div>
        <div className="card-time text-gray-500">{timeClose}</div>
      </div>
      <div className="card-price flex justify-center gap-20 items-center m-2 ">
        <p className="text-white text-lg not-italic font-bold">{price}</p>
        <button
          type="button"
          className="bg-[#F8B509] p-2 rounded text-white text-center not-italic font-bold uppercase text-xs"
        >
          Bid now
        </button>
      </div>
    </div>
  );
}
