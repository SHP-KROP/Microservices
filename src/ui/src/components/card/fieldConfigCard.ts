import Tennis from '../../images/Tennis.svg';
import Sneaker from '../../images/Sneakers.svg';
import Poker from '../../images/Poker.svg';
import Snooker from '../../images/Snooker.svg';

export interface IFieldConfigCard {
  title: string;
  timeClose: string;
  price: string;
  bg: string;
}

const fieldConfigCard = [
  {
    title: 'Tennis Set',
    timeClose: '11h : 35m : 47s',
    price: '15$',
    bg: Tennis,
  },
  {
    title: 'Modern Sneaker',
    timeClose: '11h : 35m : 47s',
    price: '15$',
    bg: Sneaker,
  },
  {
    title: 'Snooker Set',
    timeClose: '11h : 35m : 47s',
    price: '15$',
    bg: Snooker,
  },
  {
    title: 'Pocker Set',
    timeClose: '11h : 35m : 47s',
    price: '15$',
    bg: Poker,
  },
];
export default fieldConfigCard;
