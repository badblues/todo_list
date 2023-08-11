import React, { createContext, Component } from "react";
import AuthApiService from "../services/AuthApiService";

export const UserContext = createContext({});

export class UserContextProvider extends Component {
  constructor(props) {
    super(props);
    this.authService = new AuthApiService();
    this.state = {
      user: this.loadUser(),
      login: this.login.bind(this),
      refreshLogin: this.refreshLogin.bind(this),
      logout: this.logout.bind(this),
      registerUser: this.register.bind(this),
    };
  }

  async login(authData) {
    await this.authService.login(authData);
    this.updateUser();
  }

  async refreshLogin() {
    await this.authService.refreshLogin();
    this.updateUser();
  }

  register(authData) {
    return this.authService.register(authData);
  }

  logout() {
    this.authService.logout();
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
