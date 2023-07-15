import SearchStyles from '../../components/homepage/SearchStyles';
import SearchIcon from '../../images/Search.svg';
import Header from '../../components/homepage/Header';

export default function Home() {
  return (
    <div className="home bg-home w-full h-screen">
      <Header />
      <div className="search w-10/12 m-auto">
        <SearchStyles.Search className="rounded-xl">
          <SearchStyles.SearchIconWrapper>
            <img src={SearchIcon} alt="search" className="w-6 h-6" />
          </SearchStyles.SearchIconWrapper>
          <SearchStyles.StyledInputBase
            placeholder="Find Actions..."
            inputProps={{ 'aria-label': 'search' }}
          />
        </SearchStyles.Search>
      </div>
    </div>
  );
}
