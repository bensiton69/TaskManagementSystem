import { TaskService } from './../../Services/task.service';
import { Component, OnInit } from '@angular/core';
import { KeyValuePair } from 'src/app/Interfaces/KeyVakuePair';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  private readonly PAGE_SIZE = 3;
  queryResult: any = {};
  model:any={};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  owners = [
    "Ben",
    "Mic",
    "Aviram",
    "Moti"
  ];
  
  columns = [
    { title: 'Urgent Level', key: 'urgentLevel', isSortable: true },
    { title: 'Title', key: 'title', isSortable: true },
    { title: 'Description', key: 'description', isSortable: true },
    { title: 'Status', key: 'status', isSortable: true },
    {}
  ];

  constructor(private taskService: TaskService) { }

  ngOnInit() {
    this.getTasks();
  }

  getTasks() {
    this.taskService.getTasks(this.query)
      .subscribe(result => this.queryResult = result);
  }


  onFilterChange() {
    this.query.page = 1; 
    this.getTasks();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.getTasks();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = !this.query.isSortAscending;
    }
    this.getTasks();
  }

  onPageChange(page) {
    this.query.page = page;
    this.getTasks();
  }

}
