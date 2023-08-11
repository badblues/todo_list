import React, { createContext, Component } from "react";
import AuthApiService from "../services/AuthApiService";
import jwtDecode from "jwt-decode";

export const UserContext = createContext({});

export class UserContextProvider extends Component {
  constructor(props) {
    super(props);
    this.authService = new AuthApiService();
    this.state = {
      user: this.loadUser(),
      login: this.login.bind(this),
      logout: this.logout.bind(this),
      registerUser: this.register.bind(this),
    };
  }

  login(authData) {
    return this.authService.login(authData).then((token) => {
      if (token) {
        localStorage.setItem("userToken", token);
        this.setUserData(token);
        this.updateUser();
      }
    });
  }

  register(authData) {
    return this.authService.register(authData);
  }

  setUserData(token) {
    try {
      const decodedToken = jwtDecode(token);
      const userInfo = {
        loggedIn: true,
        id: decodedToken.id,
        email: decodedToken.email,
      };
      const userInfoStr = JSON.stringify(userInfo);
      localStorage.setItem("user", userInfoStr);
    } catch (error) {
      console.error(error);
    }
  }

  logout() {
    localStorage.removeItem("userToken");
    localStorage.removeItem("user");
    this.updateUser();
  }

  updateUser = () => {
    this.setState({
      user: this.loadUser(),
      login: this.login.bind(this),
      logout: this.logout.bind(this),
    });
  };

  loadUser = () => {
    const user = {
      loggedIn: false,
      id: "",
      email: "",
    };
    const data = localStorage.getItem("user");
    if (data !== null) {
      const userInfo = JSON.parse(data);
      user.loggedIn = true;
      user.id = userInfo.id;
      user.email = userInfo.email;
    }
    return user;
  };

  render() {
    return (
      <UserContext.Provider value={this.state}>
        {this.props.children}
      </UserContext.Provider>
    );
  }
}
