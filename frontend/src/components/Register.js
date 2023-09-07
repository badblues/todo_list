import React, { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { UserContext } from "../contexts/UserContext";

const Register = () => {
  const navigate = useNavigate();
  const { register, handleSubmit, formState, setError } = useForm();
  const { errors } = formState;
  const { registerUser } = useContext(UserContext);

  useEffect(() => {
    if (localStorage.getItem("userToken") !== null) navigate("/");
  }, []);

  const onRegister = async (data) => {
    if (data.password !== data.confirmPassword || data.password === undefined) {
      setError("password", {
        type: "manual",
        message: "Password must be the same",
      });
      setError("confirmPassword", {
        type: "manual",
        message: "Password must be the same",
      });
      return;
    }
    try {
      const authData = { email: data.email, password: data.password };
      await registerUser(authData);
      navigate("/login");
    } catch (error) {
      setError("email", {
        type: "manual",
        message: "Email already taken",
      });
    }
  };

  return (
    <>
      <form onSubmit={handleSubmit(onRegister)}>
        <div className="form-control">
          <h2 className="form-title">REGISTER</h2>
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
        <div className="form-control">
          <label htmlFor="confirm-password">PASSWORD:</label>
          <input
            type="password"
            placeholder="PASSWORD"
            {...register("confirmPassword", {
              required: "Input password",
            })}
          />
          <label className="form-text">{errors.confirmPassword?.message}</label>
        </div>
        <button type="submit" className="btn btn-block" onClick={onRegister}>
          REGISTER
        </button>
      </form>
    </>
  );
};

export default Register;
