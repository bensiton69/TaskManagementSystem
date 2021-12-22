import { StatisticsService } from './../../Services/statistics.service';
import { Component, OnInit } from '@angular/core';
import { KeyValuePair } from 'src/app/Interfaces/KeyVakuePair';
import { AccountService } from 'src/app/Services/account.service';
import { TaskService } from 'src/app/Services/task.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  private readonly PAGE_SIZE = 3;
  queryResult: any = {};
  users: KeyValuePair[] = [];
  model: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };


  columns = [
    { title: 'Place', key: 'place', isSortable: false },
    { title: 'User name', key: 'userName', isSortable: false },
    { title: 'Number of Done Tasks', key: 'taskNumber', isSortable: false },
    {}
  ];

  constructor(
    private statisticsService: StatisticsService,
    private taskService: TaskService,
    private accountService: AccountService) { }

  ngOnInit() {
    this.getStats();
  }

  getStats() {
    this.statisticsService.getStats().subscribe(z => console.log(z))
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
