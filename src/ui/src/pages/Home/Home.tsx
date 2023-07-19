import Header from '../../components/homepage/Header';
import Search from '../../components/search/Search';
import Card from '../../components/card/Card';
import fieldConfigCard from '../../components/card/fieldConfigCard';

export default function Home() {
  return (
    <div className="home bg-home w-full h-screen">
      <Header />
      <Search />
      <div className="cards flex m-auto justify-around items-center w-10/12">
        {fieldConfigCard.map((card) => (
          <Card key={card.title} card={card} />
        ))}
      </div>
    </div>
  );
}
