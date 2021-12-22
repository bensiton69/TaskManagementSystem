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
  responce: any;

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
    this.statisticsService.getStats().subscribe(val => {
      this.responce = val;
      console.log(this.responce);
    })
  }

}
