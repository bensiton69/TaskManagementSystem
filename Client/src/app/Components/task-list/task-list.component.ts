import { AccountService } from 'src/app/Services/account.service';
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
  users: KeyValuePair[] = [];
  model: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };


  columns = [
    { title: 'Urgent Level', key: 'urgentLevel', isSortable: true },
    { title: 'User name', key: 'userName', isSortable: true },
    { title: 'Deadline', key: 'deadline', isSortable: true },
    { title: 'Title', key: 'title', isSortable: true },
    { title: 'Description', key: 'description', isSortable: true },
    { title: 'Status', key: 'status', isSortable: true },
    {}
  ];

  constructor(private taskService: TaskService, private accountService: AccountService) { }

  ngOnInit() {
    this.getTasks();
    this.getUsers();
  }

  getTasks() {
    this.taskService.getTasks(this.query)
      .subscribe(result => {
        this.queryResult = result;
      });
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

  getUsers() {
    this.accountService.getUsers().subscribe(users => {
      this.users = users;
      console.log(users);
    }
    )
  }

}
