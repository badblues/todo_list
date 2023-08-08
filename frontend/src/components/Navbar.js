import React, { useContext } from "react";
import "./Navbar.css";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../contexts/UserContext";
import { UiContext } from "../contexts/UiContext";

const Navbar = () => {
  const { logout } = useContext(UserContext);
  const { addTaskVisible, toggleAddTaskVisible } = useContext(UiContext);

  const navigate = useNavigate();

  const onLogout = () => {
    logout();
    navigate("/login");
  };

  const onAddTask = () => {
    toggleAddTaskVisible();
  };

  return (
    <div className="navbar">
      <button
        className={`btn add-btn ${addTaskVisible ? "pressed" : ""}`}
        onClick={onAddTask}
      >
        {addTaskVisible ? "CLOSE" : "ADD TASK"}
      </button>
      <button className="btn" onClick={onLogout}>
        LOG OUT
      </button>
    </div>
  );
};

export default Navbar;
