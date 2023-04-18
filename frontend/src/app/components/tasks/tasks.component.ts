import { Component, OnInit } from '@angular/core';
import { TaskService } from  'src/app/services/task.service';
import { Task } from 'src/app/models/Task';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit{
  tasks: Task[] = []

  constructor(private taskService: TaskService, private router: Router) {
  }

  ngOnInit(): void {
    if (!localStorage.getItem("userToken"))
      this.router.navigate(["/login"]);
    this.taskService.getTasks().subscribe((tasks)=> this.tasks = tasks);
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
  }

}
