import React from 'react';
import UserIcon from '../../images/User_5 1.svg';
import navItems from './linkHomeConfig';

export default function Header() {
  return (
    <div className="header flex justify-around items-center h-20">
      <div className="text-white text-3xl not-italic font-black leading-8">
        AuctionOnline
      </div>
      <div className="navbar">
        <ul className="flex justify-around gap-5">
          {navItems.map((navItem) => (
            <li key={navItem.id}>
              <a
                href={navItem.link}
                className="text-white text-base not-italic font-medium leading-4"
              >
                {navItem.title}
              </a>
            </li>
          ))}
        </ul>
      </div>
      <div className="user">
        <button type="button">
          <img src={UserIcon} alt="profile" />
        </button>
      </div>
    </div>
  );
}
