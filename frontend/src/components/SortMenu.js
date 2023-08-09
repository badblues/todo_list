import React, { useContext, useState } from "react";
import { Sorts, UiContext } from "../contexts/UiContext";

const SortMenu = () => {
  const [isOpen, setIsOpen] = useState(false);
  const { sort, setSort } = useContext(UiContext);
  return (
    <div>
      <button
        className="btn"
        onClick={() => {
          setIsOpen(!isOpen);
        }}
      >
        {sort}
      </button>
      {isOpen ? (
        <div>
          <label onClick={() => setSort(Sorts.ALPHABETICAL)}>
            {Sorts.ALPHABETICAL}
          </label>
          <label onClick={() => setSort(Sorts.OLD)}>{Sorts.OLD}</label>
          <label onClick={() => setSort(Sorts.NEW)}>{Sorts.NEW}</label>
        </div>
      ) : null}
    </div>
  );
};

export default SortMenu;
