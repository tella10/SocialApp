import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material.module';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaterialModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })

  ],
  exports : [
    MaterialModule,
    ToastrModule
  ]
})
export class SharedModule { }
