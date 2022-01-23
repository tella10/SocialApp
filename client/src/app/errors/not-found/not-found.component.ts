import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.css']
})
export class NotFoundComponent implements OnInit {

  longText = `We could not find the page.`;

  constructor(private route : Router) { }

  ngOnInit(): void {
  }

  back(){
    this.route.navigate;
  }

}
