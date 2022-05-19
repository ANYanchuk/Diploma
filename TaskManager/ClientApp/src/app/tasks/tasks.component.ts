import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './tasks.component.html'
})
export class TasksComponent {
  public tasks: Task[] = [];

  constructor(http: HttpClient) {
    http.get<Task[]>('api/tasks').subscribe(result => {
      this.tasks = result;
    }, error => console.error(error));
  }
}

interface Task {
  id: number;
  title: string;
  body: string;
  categoryName: string;
  reportFormatName: string;
}
