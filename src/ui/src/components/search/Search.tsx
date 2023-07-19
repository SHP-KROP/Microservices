import React from 'react';
import SearchStyles from './SearchStyles';
import SearchIcon from '../../images/Search.svg';

export default function Search() {
  return (
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
  );
}
