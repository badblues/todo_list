import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { User } from 'src/app/models/User';
import { UiService } from 'src/app/services/ui.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email!: string;
  password!: string;

  constructor(
    private userService: UserService,
    private router: Router,
    private uiService: UiService) {}

  ngOnInit(): void {
    if (localStorage.getItem("userToken")) {
      this.router.navigate([""]);
      return;
    }
  }

  onSubmit() {
    var user = {
      email: this.email,
      password: this.password,
    }
    this.userService.login(user);
  }

}
