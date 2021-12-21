import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  model: any = {}
  explain: string = "You have to insert a valid email and A Strong password At least 8 charts with: One capital letter, One digit, One non-alphanumeric char."

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe(response => {
      this.cancel();
    }, error => {
      if (error.error) {
        this.toastr.error(error.error);
      }
      else{
        this.toastr.error("Please read the instructions again.");
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }


}
