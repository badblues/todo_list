import { Component, OnInit } from '@angular/core';
import { TaskService } from  'src/app/services/task.service';
import { Task } from 'src/app/models/Task';
import { Router } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { Subscription } from 'rxjs';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
  animations: [
    trigger('menuAnimation', [
      state('open', style({
        transform: 'scale(1)'
      })),
      state('closed', style({
        transform: 'scale(0)'
      })),
      transition('open <=> closed', [
        animate('0.2s')
      ])
    ])
  ]
})
export class TasksComponent implements OnInit{
  tasks: Task[] = []
  selectedSort: string = "";
  selectedSortSubscription!: Subscription;
  
  constructor(private uiService: UiService, private taskService: TaskService, private router: Router) {
    this.selectedSortSubscription = this.uiService
      .onSortSelected()
      .subscribe(value => {
        this.selectedSort = value;
        this.sortTasks();
      });
  }

  ngOnInit(): void {
    if (!localStorage.getItem("userToken")) {
      this.router.navigate(["/login"]);
      return;
    }
    this.taskService.getTasks().subscribe((tasks)=> this.tasks = tasks);
    this.sortTasks();
  }

  deleteTask(task: Task) {
    this.taskService
      .deleteTask(task)
      .subscribe(
        () => (this.tasks = this.tasks.filter(t => t.id !== task.id)));
  }

  toggleTask(task: Task) {
    task.completed = !task.completed;
    this.taskService.updateTaskCompleted(task).subscribe();
  }

  addTask(task: Task) {
    this.taskService.addTask(task).subscribe((task) => (this.tasks.push(task)));
    this.sortTasks();
  }

  sortTasks(): void {
    switch (this.selectedSort) {
      case "newest":
        this.tasks.sort((a, b) => {
          //useless if statement, creationDate is always not null
          if (a.creationDate != null && b.creationDate != null) {
            var dateA = new Date(a.creationDate);
            var dateB = new Date(b.creationDate);
            return dateB.getTime() - dateA.getTime();
          }
          return 0;
        });
        break;
      case "oldest":
        this.tasks.sort((a, b) => {
          if (a.creationDate != null && b.creationDate != null) {
            var dateA = new Date(a.creationDate);
            var dateB = new Date(b.creationDate);
            return dateA.getTime() - dateB.getTime();
          }
          return 0;
        });
        break;
      case "alphabetical":
        this.tasks.sort((a, b) => {
          return a.title < b.title ? -1 : 1;
        });
        break;
    }
  }

}
