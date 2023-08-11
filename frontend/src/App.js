import "./App.css";
import Login from "./components/Login";
import Navbar from "./components/Navbar";
import Register from "./components/Register";
import Tasks from "./components/Tasks";
import TasksApiService from "./services/TaskApiService";
import axios from "axios";
import { Routes, Route } from "react-router-dom";

function App() {
  axios.interceptors.request.use((request) => {
    const token = localStorage.getItem("userToken");
    if (token) {
      request.headers["Authorization"] = "Bearer " + token;
    }
    return request;
  });
  const tasksApiServivce = new TasksApiService();

  return (
    <div className="page">
      <Navbar />
      <Routes>
        <Route element={<Login />} path="/login" />
        <Route element={<Register />} path="/register" />
        <Route element={<Tasks tasksApiService={tasksApiServivce} />} path="" />
      </Routes>
    </div>
  );
}

export default App;
