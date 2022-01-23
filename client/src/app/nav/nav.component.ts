import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_model/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  usern : any = {}
  loggedin: boolean = false;


  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
   this.getusername();
  }

  getusername(){
    this.usern = this.accountService.getCurrentUser(this.usern);
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/home')
  }


}


