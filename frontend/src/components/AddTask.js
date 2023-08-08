import React from "react";
import { useForm } from "react-hook-form";

const AddTask = (props) => {
  const { register, handleSubmit, formState } = useForm();
  const { errors } = formState;

  const { tasksApiService, onAddTask } = props;

  const onSubmit = async (data) => {
    try {
      await tasksApiService.createTask(data);
      onAddTask();
    } catch (error) {}
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="add-form">
      <label>ADD TASK</label>
      <div className="form-control">
        <input
          type="text"
          id="title"
          placeholder="Title"
          {...register("title", { required: "ENTER TITLE" })}
        />
        <label>{errors.title?.message}</label>
      </div>
      <div className="form-control">
        <input
          type="text"
          id="details"
          placeholder="Details"
          {...register("details")}
        />
      </div>
      <div className="form-control form-control-check">
        <label>SET COMPLETED</label>
        <input type="checkbox" name="completed" {...register("completed")} />
      </div>
      <button type="submit" className="btn btn-block">
        ADD TASK
      </button>
    </form>
  );
};

export default AddTask;
