import axios from "axios";
import jwtDecode from "jwt-decode";

export default class AuthApiService {
  apiUrl = "http://localhost:5055/auth";

  http = axios.create({
    withCredentials: true,
  });

  async login(authData) {
    const url = this.apiUrl + "/login";
    try {
      await this.http.post(url, authData).then((response) => {
        this.handleResponse(response);
      });
    } catch (error) {
      throw error;
    }
  }

  async refreshLogin() {
    const url = this.apiUrl + "/refresh-token";
    try {
      await this.http.post(url).then((response) => {
        this.handleResponse(response);
      });
    } catch (error) {
      this.logout();
    }
  }

  logout() {
    localStorage.removeItem("userToken");
    localStorage.removeItem("user");
  }

  async register(user) {
    const url = this.apiUrl + "/register";
    try {
      const response = await this.http.post(url, user, this.httpOptions);
      return response.data.data;
    } catch (error) {
      throw error.response.data.error;
    }
  }

  handleResponse(response) {
    const token = response.data.data.token;
    localStorage.setItem("userToken", token);
    this.setUserData(token);
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
}
