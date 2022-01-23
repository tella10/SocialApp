import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {

  baseUrl = 'https://localhost:5001/api';
  validationError : string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

get404error(){
   this.http.get(this.baseUrl + '/Buggy/Not-Found').subscribe(
     Response => {
       console.log(Response);
     }, error => {
       console.log(error);
     }
   )
}

get400error(){
  this.http.get(this.baseUrl + '/Buggy/Bad-Request').subscribe(
    Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    }
  )
}

get500error(){
  this.http.get(this.baseUrl + '/Buggy/Server-error').subscribe(
    Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    }
  )
}

get401error(){
  this.http.get(this.baseUrl + '/Buggy/Auth').subscribe(
    Response => {
      console.log(Response);
    }, error => {
      console.log(error);
    }
  )
}


get400Validationerror(){
  this.http.post(this.baseUrl + '/Account/register', {}).subscribe(
    Response => {
      console.log(Response);
    }, error => {
      console.log(error);
      this.validationError = error;
    }
  )
}

}
