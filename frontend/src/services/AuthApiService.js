import http from "axios";

export default class AuthApiService {
  apiUrl = "http://localhost:5055/auth";

  httpOptions = {
    Headers: {
      "Content-Type": "application/json",
    },
  };

  async login(authData) {
    const url = this.apiUrl + "/login";
    try {
      const response = await http.post(url, authData, this.httpOptions);
      console.log(response)
      return response.data.data.token;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

  async register(user) {
    const url = this.apiUrl + "/register";
    try {
      const response = await http.post(url, user, this.httpOptions);
      console.log(response);
      return response;
    } catch (error) {
      throw error;
    }
  }
}
