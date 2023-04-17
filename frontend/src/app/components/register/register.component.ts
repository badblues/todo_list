import { Component } from '@angular/core';
import { User } from 'src/app/models/User';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  email!: string;
  password!: string;
  confirmPassword!: string;

  constructor(private userService: UserService) {}

  onSubmit() {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.email)) {
      alert('Incorrect email!');
    }
    if (this.password.length < 8) {
      alert('Password too short!');
    }
    if (this.password !== this.confirmPassword) {
      alert('Passwords don\'t match!');
    }

    var newUser = {
      email: this.email,
      password: this.password,
    }
    this.register(newUser);
  }

  register(user: User) {
    this.userService.register(user).subscribe();
  }

}
