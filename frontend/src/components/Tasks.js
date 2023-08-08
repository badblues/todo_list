import React, { useContext, useEffect, useState } from "react";
import "./Tasks.css";
import { UserContext } from "../contexts/UserContext";
import { useNavigate } from "react-router-dom";
import AddTask from "./AddTask";
import { UiContext } from "../contexts/UiContext";

const Tasks = (props) => {
  const { tasksApiService } = props;
  const { user } = useContext(UserContext);
  const { addTaskVisible } = useContext(UiContext);
  const [tasks, setTasks] = useState(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const fetchData = async () => {
    try {
      const response = await tasksApiService.getTasks();
      setTasks(response);
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

  if (loading) {
    return <div>Loading</div>;
  }

  return (
    <>
      {addTaskVisible ? <AddTask /> : null}
      <div className="tasks-container">
        {tasks.map((task) => (
          <div
            key={task.id}
            className={`task-item ${task.completed ? "completed" : ""}`}
            onClick={() => onChangeComplition(task)}
          >
            <h3>{task.title}</h3>
            <p>{task.details}</p>
          </div>
        ))}
      </div>
    </>
  );
};

export default Tasks;
