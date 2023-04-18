import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/User';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'http://localhost:5000/auth';

  constructor(private http: HttpClient) { }

  register(user: User): Observable<User> {
    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<User>(this.apiUrl + '/register', user, httpOptions);
  }

  login(user: User): Observable<string> {
    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      responseType: 'text'
    };
    //TODO: handle http errors and use httpOptions
    return this.http.post(this.apiUrl + '/login', user, {responseType: "text"});
  }

  refreshToken() {

  }

  getEmail(): Observable<string> {
    return this.http.get(this.apiUrl, {responseType: "text"});
  }
}
