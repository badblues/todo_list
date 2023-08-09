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
  const navigate = useNavigate();

  const fetchData = async () => {
    try {
      const tasks = await tasksApiService.getTasks();
      setTasks(tasks);
      setLoading(false);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (!user.loggedIn) {
      navigate("/login");
    }
    fetchData();
  }, []);

  const onChangeComplition = async (task) => {
    try {
      task.completed = !task.completed;
      await tasksApiService.updateTask(task.id, task);
      fetchData();
    } catch (error) {
      console.log(error);
    }
  };

  const onDelete = async (id) => {
    try {
      await tasksApiService.deleteTask(id);
      fetchData();
    } catch (error) {
      console.log(error);
    }
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
          if (a.title < b.title) {
            return -1;
          }
          if (a.title > b.title) {
            return 1;
          }
          return a.id < b.id ? -1 : 1;
        };
    }
  };

  if (loading) {
    return <div>Loading</div>;
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
      <h2>TASKS:</h2>
      <SortMenu />
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
