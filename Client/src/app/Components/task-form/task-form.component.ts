import { AccountService } from 'src/app/Services/account.service';
import { TaskService } from './../../Services/task.service';
import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { KeyValuePair } from 'src/app/Interfaces/KeyVakuePair';
import { Task } from 'src/app/Interfaces/Task';
import { FeaturesService } from 'src/app/Services/features.service';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {
  model: any = {};
  statuses: string[] = [];
  urgentLevels: string[] = [];
  owners: KeyValuePair[] = [];

  task: Task = {
    id: "",
    title: "",
    description: "",
    status: 0,
    urgentLevel: 0,
    ownerId: "",
    deadline: "",
  };

  constructor(
    private taskService: TaskService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private featuresService: FeaturesService,) {
    route.params.subscribe(p => {
      this.task.id = p["id"];
    })
  }

  ngOnInit(): void {
    this.forkDataOnInit();
  }

  forkDataOnInit() {
    var sources: unknown[] = []


    sources.push(this.getStatuses());
    sources.push(this.getUrgentLevel());
    sources.push(this.getOwners());

    if (this.task.id) {
      sources.push(this.getTask());
    }

    forkJoin(sources)
      .subscribe(values => {
        this.statuses = values[0];
        this.urgentLevels = values[1];
        this.owners = values[2];

        if (this.task.id) {
          this.setTask(values[3]);
        }
      });
  }

  getStatuses() {
    return this.featuresService.getStatuses();
  }

  getUrgentLevel() {
    return this.featuresService.getUrgentLevel();
  }


  onDateChange() {
    this.task.deadline = `${this.model.year}-${this.model.month}-${this.model.day}`;
  }

  onMakeChange() {
  }

  submit() {
    this.numberizeFields();
    console.log(this.task);
    if (this.task.id) {
      this.taskService.update(this.task).subscribe(x => {
        this.toastr.success("Edited");
      });
    } else {
      delete this.task.id;
      delete this.task.deadline;
      this.Test();
      this.taskService.create(this.task)
        .subscribe(x => this.toastr.success("Created"));
    }
    this.router.navigate(['/Tasks']);
  }

  Test() {

  }

  numberizeFields() {
    this.task.status = Number(this.task.status);
    this.task.urgentLevel = Number(this.task.urgentLevel);
  }

  getTask() {
    return this.taskService.getTask(this.task.id);
  }

  getOwners() {
    return this.accountService.getUsers();
  }

  private setTask(task: Task) {
    this.task.id = task.id;
    this.task.title = task.title;
    this.task.description = task.description;
    this.task.status = task.status;
    this.task.urgentLevel = task.urgentLevel;
    this.task.deadline = task.deadline;
    this.task.ownerId = task.ownerId;
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.taskService.delete(this.task.id)
        .subscribe(x => {
          this.router.navigate(['/Tasks']);
        })
    }
  }

}


