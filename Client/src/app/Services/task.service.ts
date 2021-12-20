import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from '../Interfaces/Task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private baseUrl = "https://localhost:5001/api";

  constructor(private http: HttpClient) { }

  create(task) {
    return this.http.post(this.baseUrl + '/SystemTasks', task)
      .pipe();
  }

  getVehicle(id: number) {
    return this.http.get(this.baseUrl + '/SystemTasks/' + id)
      .pipe();
  }

  getVehicles(filter) {
    return this.http.get<Task[]>(this.baseUrl + '/SystemTasks/?' + this.toQueryString(filter))
      .pipe();
  }

  update(task: Task) {
    return this.http.put(this.baseUrl + /SystemTasks/ + task.id, task)
      .pipe();
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + /SystemTasks/ + id)
      .pipe();
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) 
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }
}
