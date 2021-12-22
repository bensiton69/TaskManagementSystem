import { AccountService } from 'src/app/Services/account.service';
import { TaskService } from './../../Services/task.service';
import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { KeyValuePair } from 'src/app/Interfaces/KeyVakuePair';
import { Task } from 'src/app/Interfaces/Task';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {
  model: any = {};

  owners = [
    "Ben",
    "Mic",
    "Aviram",
    "Moti"
  ];

  task: Task = {
    id: "",
    title: "",
    description: "",
    status: 0,
    urgentLevel: 0,
    ownerId: "90f19746-cda2-41d2-695a-08d9c482c6d1",
  };
  
  time = {}

  features: KeyValuePair[] = []

  constructor(
    private taskService: TaskService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) {
    route.params.subscribe(p => {
      this.task.id = p["id"];
    })
  }

  ngOnInit(): void {
    this.forkDataOnInit();
  }

  forkDataOnInit() {
    var sources: unknown[] = [
      //this.getOwners(),
    ]

    if (this.task.id) {
      sources.push(this.getTask());
    }

    forkJoin(sources)
      .subscribe(values => {
        //this.owners =values[0];
        if (this.task.id) {
          this.setTask(values[0]);
        }
      });
  }


  onMakeChange() {
    console.log(this.model.owner);

  }

  submit() {
    if (this.task.id) {
      this.taskService.update(this.task).subscribe(x => {
        this.toastr.success("Edited");
      });
    } else {
      delete this.task.id;
      this.taskService.create(this.task)
        .subscribe(x => this.toastr.success("Created"));
    }
  }

  Test() {
    this.toastr.success("success");
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
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.taskService.delete(this.task.id)
        .subscribe(x => {
          this.router.navigate(['/home']);
        })
    }

  }

}
