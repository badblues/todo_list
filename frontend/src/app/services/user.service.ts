import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaderResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/User';
import { UiService } from './ui.service';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'http://localhost:5055/auth';

  constructor(private http: HttpClient, private uiService: UiService, private router: Router) { }

  register(user: User): Observable<User> {
    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<User>(this.apiUrl + '/register', user, httpOptions);
  }

  login(user: User){
    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      responseType: 'text' as 'json',
    };
    this.http.post(this.apiUrl + '/login', user, httpOptions).subscribe(
      (token) => {
        if (token) {
          localStorage.setItem('userToken', token.toString())
          this.uiService.toggleUserLogged();
          this.router.navigate([""]);
        } else {
          alert("Wrond email or password");
        }
      }
    );
  }

  logout() {
    localStorage.removeItem("userToken");
    this.router.navigate(["/login"]);
  }

  refreshToken() {

  }

  getEmail(): Observable<string> {
    return this.http.get(this.apiUrl, {responseType: "text"});
  }
}
