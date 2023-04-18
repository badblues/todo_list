import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UiService {
  private showAddTask: boolean = false;
  private userLogged!: boolean;
  private addTaskSubject = new BehaviorSubject<boolean>(this.showAddTask);
  private userLoggedSubject = new BehaviorSubject<boolean>(this.userLogged);

  constructor() {
    if (localStorage.getItem("userToken")) {
      this.userLogged = true;
      this.userLoggedSubject.next(this.userLogged);
    }
  }

  toggleAddTask(): void {
    this.showAddTask = !this.showAddTask;
    this.addTaskSubject.next(this.showAddTask);
  }

  onToggleAddTask(): Observable<boolean> {
    return this.addTaskSubject.asObservable();
  }

  toggleUserLogged(): void {
    this.userLogged = !this.userLogged;
    this.userLoggedSubject.next(this.userLogged);
  }

  onToggleUserLogged(): Observable<boolean> {
    return this.userLoggedSubject.asObservable();
  }


}
