import React, { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { UserContext } from "../contexts/UserContext";

const Login = () => {
  const navigate = useNavigate();
  const { register, handleSubmit, formState } = useForm();
  const { errors } = formState;
  const { login } = useContext(UserContext);

  useEffect(() => {
    if (localStorage.getItem("userToken") !== null) navigate("/");
  }, []);

  const onLogin = async (data) => {
    try {
      await login(data);
      navigate("/");
    } catch (error) {
      console.log(error);
    }
  };

  const onRegister = () => {
    navigate("/register");
  };

  return (
    <>
      <form onSubmit={handleSubmit(onLogin)}>
        <div className="form-control">
          <h2 className="form-title">LOG IN</h2>
          <label htmlFor="email">EMAIL:</label>
          <input
            type="email"
            placeholder="EMAIL"
            {...register("email", {
              required: "Input email",
            })}
          />
          <label className="form-text">{errors.email?.message}</label>
        </div>
        <div className="form-control">
          <label htmlFor="password">PASSWORD:</label>
          <input
            type="password"
            placeholder="PASSWORD"
            {...register("password", {
              required: "Input password",
            })}
          />
          <label className="form-text">{errors.password?.message}</label>
        </div>
        <button type="submit" className="btn btn-block">
          LOG IN
        </button>
        <button className="btn btn-block" onClick={onRegister}>
          REGISTER
        </button>
      </form>
    </>
  );
};

export default Login;
