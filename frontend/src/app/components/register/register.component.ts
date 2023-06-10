import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{

  email!: string;
  password!: string;
  confirmPassword!: string;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    if (localStorage.getItem("userToken")) {
      this.router.navigate([""]);
      return;
    }
  }

  onSubmit() {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.email)) {
      alert('Incorrect email!');
      return;
    }
    if (this.password.length < 8) {
      alert('Password too short!');
      return;
    }
    if (this.password !== this.confirmPassword) {
      alert('Passwords don\'t match!');
      return;
    }

    var newUser = {
      email: this.email,
      password: this.password,
    }
    this.register(newUser);
  }

  register(user: User) {
    this.userService.register(user).subscribe();
    this.router.navigate(["\login"]);
  }

}
