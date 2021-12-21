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

  task: Task = {
    id: "",
    title: "",
    description: "",
    status: 0,
    urgentLevel: 0,
  };

  features: KeyValuePair[] = []

  constructor(
    private taskService: TaskService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router
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
    ]

    if (this.task.id) {
      sources.push(this.getTask());
    }

    forkJoin(sources)
      .subscribe(values => {
        if (this.task.id) {
          this.setTask(values[0]);
          //this.populateModels();
        }
      });
  }


  onMakeChange() {
    //this.populateModels();
    //delete this.task.modelId;
  }

  // private populateModels() {
  //   var selectedMake = this.makes.find(m => m.id == this.task.makeId);
  //   this.models = selectedMake ? selectedMake.models : [];
  // }

  // onFeatureToggle(featureId, $event) {
  //   if ($event.target.checked)
  //     this.task.features.push(featureId);
  //   else {
  //     var index = this.task.features.indexOf(featureId);
  //     this.task.features.splice(index, 1);
  //   }
  // }

  submit() {
    if (this.task.id) {
      this.taskService.update(this.task).subscribe(x => {
        console.log(x);
        console.log(this.task);
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
