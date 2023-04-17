import { Component } from '@angular/core';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  title: string = 'TODO LIST';
  userEmail?: string;
  showAddTask!: boolean;
  subscription!: Subscription;

  constructor(
    private uiService: UiService, 
    private router: Router, 
    private userService: UserService)
  {
    this.subscription = this.uiService
      .onToggle()
      .subscribe(value => this.showAddTask = value);
  }

  toggleAddTask() {
    this.uiService.toggleAddTask();
  }

  hasRoute(route: string) {
    return this.router.url === route;
  }

  getUserEmail() {
    this.userService.getEmail().subscribe((email) => this.userEmail = email);
  }

}
