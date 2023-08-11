import React, { useState } from "react";
import { useForm } from "react-hook-form";

const AddTask = (props) => {
  const { register, handleSubmit, formState } = useForm();
  const { errors } = formState;
  const [title, setTitle] = useState("");

  const { tasksApiService, onAddTask } = props;

  const onSubmit = async (data) => {
    try {
      const task = {
        title: data.title,
        details: data.details,
        completed: false,
      };
      await tasksApiService.createTask(task);
      onAddTask();
    } catch (error) {}
  };

  const handleTitleChange = (event) => {
    const input = event.target.value;
    if (input.length <= 50) {
      setTitle(input);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="add-form">
      <h2 className="form-title">ADD TASK</h2>
      <div className="form-control">
        <input
          type="text"
          id="title"
          placeholder="Title"
          autoComplete="off"
          onInput={handleTitleChange}
          value={title}
          {...register("title", {
            required: "ENTER TITLE",
            maxLength: {
              value: 50,
              message: "Title cannot exceed 50 characters",
            },
          })}
        />
        <label>{errors.title?.message}</label>
      </div>
      <div className="form-control">
        <input
          type="text"
          id="details"
          placeholder="Details"
          autoComplete="off"
          {...register("details")}
        />
      </div>
      <button type="submit" className="btn btn-block">
        ADD TASK
      </button>
    </form>
  );
};

export default AddTask;
