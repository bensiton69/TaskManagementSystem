import { TaskService } from './../../Services/task.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from 'src/app/Interfaces/User';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  model: any ={};
  curretnUser$: Observable<User>;

  constructor(public accountService: AccountService, private router: Router
    ,private toastr: ToastrService, private taskService:TaskService) {
  }
    
  ngOnInit(): void {
    this.getCurrentUser();
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/Tasks');
    }, error => {
      console.log(error);
      this.toastr.error(error.error);
    })
  }

  logout() {
    this.accountService.logout();
  }

  getCurrentUser(){
    this.curretnUser$ = this.accountService.currentUser$;
  }


}
