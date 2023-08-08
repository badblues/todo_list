import "./App.css";
import LoginPage from "./components/LoginPage";
import Navbar from "./components/Navbar";
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
        <Route element={<LoginPage />} path="/login" />
        <Route element={<Tasks tasksApiService={tasksApiServivce} />} path="" />
      </Routes>
    </div>
  );
}

export default App;
