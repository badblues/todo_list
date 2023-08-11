import React, { useContext, useState } from "react";
import "./SortMenu.css";
import { Sorts, UiContext } from "../contexts/UiContext";

const SortMenu = () => {
  const [isOpen, setIsOpen] = useState(false);
  const { sort, setSort } = useContext(UiContext);
  return (
    <>
      <button
        className="menu-btn btn"
        onClick={() => {
          setIsOpen(!isOpen);
        }}
      >
        {sort}
      </button>
      {isOpen ? (
        <div className="menu">
          <label
            className="option"
            onClick={() => {
              setSort(Sorts.ALPHABETICAL);
              setIsOpen(!isOpen);
            }}
          >
            {Sorts.ALPHABETICAL}
          </label>
          <label
            className="option"
            onClick={() => {
              setSort(Sorts.OLD);
              setIsOpen(!isOpen);
            }}
          >
            {Sorts.OLD}
          </label>
          <label
            className="option"
            onClick={() => {
              setSort(Sorts.NEW);
              setIsOpen(!isOpen);
            }}
          >
            {Sorts.NEW}
          </label>
        </div>
      ) : null}
    </>
  );
};

export default SortMenu;
