import { Component, EventEmitter, Output } from '@angular/core';
import { Task } from 'src/app/models/Task';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})

export class AddTaskComponent {
  @Output() onAddTask: EventEmitter<Task> = new EventEmitter();

  title?: string;
  details?: string;
  completed: boolean = false;
  subscription: Subscription;
  showAddTask!: boolean;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService
    .onToggleAddTask()
    .subscribe(value => this.showAddTask = value);
  }

  onSubmit() {

    if (!this.title) {
      alert('Empty Title!');
      return;
    }
    if (!this.details)
      this.details = "";

    const newTask = {
      title: this.title,
      details: this.details,
      completed: this.completed
    }

    this.onAddTask.emit(newTask);

    this.title = "";
    this.details = "";
    this.completed = false;

  }

}
