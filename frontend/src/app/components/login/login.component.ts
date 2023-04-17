import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  email!: string;
  password!: string;

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.email)) {
      alert('Incorrect email!');
    }
    if (this.password.length < 8) {
      alert('Password too short!');
    }

    var user = {
      email: this.email,
      password: this.password,
    }
    this.login(user);
  }

  login(user: User) {
    this.userService.login(user).subscribe(
      (token: string) => localStorage.setItem('userToken', token));
    //TODO: navigate in case of successful login
    this.router.navigate(['/tasks'])
  }

}
