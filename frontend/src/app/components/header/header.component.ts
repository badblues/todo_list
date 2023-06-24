import { Component, OnInit } from '@angular/core';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  title: string = 'TODO LIST';
  userEmail?: string;
  showAddTask!: boolean;
  userLogged!: boolean;
  showAddTaskSubscription!: Subscription;
  userLoggedSubscription!: Subscription;

  constructor(
    private uiService: UiService, 
    private router: Router, 
    private userService: UserService)
  {
    this.showAddTaskSubscription = this.uiService
      .onToggleAddTask()
      .subscribe(value => this.showAddTask = value);
    this.userLoggedSubscription = this.uiService
      .onToggleUserLogged()
      .subscribe(value => {
        this.userLogged = value;
        this.getUserEmail();
      });
  }

  toggleAddTask() {
    this.uiService.toggleAddTask();
  }

  ngOnInit(): void {
    if(this.userLogged)
      this.getUserEmail();
  }

  hasRoute(route: string) {
    return this.router.url === route;
  }

  getUserEmail() {
    this.userService.getEmail().subscribe((email) => this.userEmail = email);
  }

  onLogout() {
    this.uiService.toggleUserLogged();
    this.userService.logout();
    this.router.navigate(["/login"]);
  }

}
