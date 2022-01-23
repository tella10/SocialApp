import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_model/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

baseUrl = 'https://localhost:5001/api'

private currentUserSource = new ReplaySubject(1);
currentUser$ = this.currentUserSource.asObservable();

constructor(private http: HttpClient, private toastr : ToastrService) { }

  login(model: any){
    return this.http.post<User>(this.baseUrl + '/Account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      }
    ))
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + '/Account/register', model).pipe(
      map((response: User) => {
        const user = response;
        if (user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      }
    ))
  }

setCurrentUser(user: User){
  this.currentUserSource.next(user);
}


getCurrentUser(user: User) {
  if (user) {
    return JSON.parse(localStorage.getItem('user') as string);
   }
   else{
     this.toastr.warning("No User Found!");
   }
}


  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

}
