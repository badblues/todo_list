import React, { useContext } from "react";
import "./Navbar.css";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../contexts/UserContext";
import { UiContext } from "../contexts/UiContext";

const Navbar = () => {
  const { user, logout } = useContext(UserContext);
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
      {user.loggedIn ? (
        <div>
          <button
            className={`btn add-btn ${addTaskVisible ? "pressed" : ""}`}
            onClick={onAddTask}
          >
            {addTaskVisible ? "CLOSE" : "ADD TASK"}
          </button>
        </div>
      ) : null}
      {user.loggedIn ? (
        <div className="mini-profile">
          <label className="email">{user.email}</label>
          <button className="btn log-out-btn" onClick={onLogout}>
            LOG OUT
          </button>
        </div>
      ) : null}
    </div>
  );
};

export default Navbar;
