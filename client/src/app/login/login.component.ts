import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model : any = {}
  loggedin: boolean = false;
  @Output() cancelLogin = new EventEmitter();

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }


  login(){
    this.accountService.login(this.model).subscribe(response => {
      response = this.model;
      this.loggedin = true;
      this.router.navigateByUrl('/members');
    })
  }

  cancel() {
    this.cancelLogin.emit(false);
  }
}
