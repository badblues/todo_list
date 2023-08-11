import http from "axios";

export default class TasksApiService {
  apiUrl = "http://localhost:5055/tasks";

  async getTasks() {
    let url = this.apiUrl;
    try {
      const response = await http.get(url);
      return response.data.data;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

  async createTask(task) {
    let url = this.apiUrl;
    try {
      const response = await http.post(url, task);
      return response.data.data;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

  async updateTask(id, task) {
    let url = this.apiUrl + `/${id}`;
    try {
      await http.put(url, task);
    } catch (error) {
      throw error;
    }
  }

  async deleteTask(id) {
    let url = this.apiUrl + `/${id}`;
    try {
      const response = await http.delete(url);
      return response.data.data;
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}
