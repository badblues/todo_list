import "./App.css";
import axios from "axios";
import { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Navbar from "./components/Navbar";
import Register from "./components/Register";
import Tasks from "./components/Tasks";
import TasksApiService from "./services/TaskApiService";
import requestInterceptor from "./interceptors/RequestInterceptor";
import responseErrorInterceptor from "./interceptors/ResponseInterceptor";
import { UserContext } from "./contexts/UserContext";

function App() {
  const { refreshLogin, logout } = useContext(UserContext);

  axios.interceptors.request.use(requestInterceptor);
  axios.interceptors.response.use(
    (response) => response,
    (error) => responseErrorInterceptor(error, refreshLogin, logout)
  );

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
