import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UiService {
  private showAddTask: boolean = false;
  private addTaskSubject = new BehaviorSubject<boolean>(this.showAddTask);

  constructor() {}

  toggleAddTask(): void {
    this.showAddTask = !this.showAddTask;
    this.addTaskSubject.next(this.showAddTask);
  }

  onToggleAddTask(): Observable<boolean> {
    return this.addTaskSubject.asObservable();
  }

}
