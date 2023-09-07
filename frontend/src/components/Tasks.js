import React, { useContext, useEffect, useState } from "react";
import "./Tasks.css";
import { UserContext } from "../contexts/UserContext";
import { useNavigate } from "react-router-dom";
import AddTask from "./AddTask";
import { Sorts, UiContext } from "../contexts/UiContext";
import SortMenu from "./SortMenu";

const Tasks = (props) => {
  const { tasksApiService } = props;
  const { user } = useContext(UserContext);
  const { addTaskVisible, sort } = useContext(UiContext);
  const [tasks, setTasks] = useState(null);
  const [loading, setLoading] = useState(true);
  const [loadingMessage, setLoadingMessage] = useState("Loading");
  const navigate = useNavigate();

  const fetchData = async () => {
    try {
      const tasks = await tasksApiService.getTasks();
      setTasks(tasks);
      setLoading(false);
    } catch (error) {
      if (error.response?.status === 401) {
        setLoadingMessage("Loading error, refresh page");
      }
    }
  };

  useEffect(() => {
    if (!user.loggedIn) {
      navigate("/login");
    }
    fetchData();
  }, [user.loggedIn]);

  const onChangeComplition = async (task) => {
    try {
      task.completed = !task.completed;
      await tasksApiService.updateTask(task.id, task);
      fetchData();
    } catch (error) {}
  };

  const onDelete = async (id) => {
    try {
      await tasksApiService.deleteTask(id);
      fetchData();
    } catch (error) {}
  };

  const getSortFunc = () => {
    switch (sort) {
      case Sorts.OLD:
        return (a, b) => {
          if (a.creationDate < b.creationDate) return -1;
          return 1;
        };
      case Sorts.NEW:
        return (a, b) => {
          if (a.creationDate < b.creationDate) return 1;
          return -1;
        };
      case Sorts.ALPHABETICAL:
      default:
        return (a, b) => {
          if (a.title.toUpperCase() < b.title.toUpperCase()) {
            return -1;
          }
          if (a.title.toUpperCase() > b.title.toUpperCase()) {
            return 1;
          }
          return a.id < b.id ? -1 : 1;
        };
    }
  };

  if (loading) {
    return <div>{loadingMessage}</div>;
  }

  return (
    <>
      {addTaskVisible ? (
        <AddTask
          onAddTask={() => {
            fetchData();
          }}
          tasksApiService={tasksApiService}
        />
      ) : null}
      <SortMenu className="sort-btn" />
      <div className="tasks-container">
        {tasks.sort(getSortFunc()).map((task) => (
          <div
            key={task.id}
            className={`task-item ${task.completed ? "completed" : ""}`}
          >
            <div className="task-text" onClick={() => onChangeComplition(task)}>
              <h3 className="text">{task.title}</h3>
              <p className="text">{task.details}</p>
            </div>
            <button
              className={`delete-btn ${task.completed ? "completed" : ""}`}
              onClick={() => onDelete(task.id)}
            >
              X
            </button>
          </div>
        ))}
      </div>
    </>
  );
};

export default Tasks;
