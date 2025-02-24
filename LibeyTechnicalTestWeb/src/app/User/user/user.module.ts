import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsercardsComponent } from './usercards/usercards.component';
import { UsermaintenanceComponent } from './usermaintenance/usermaintenance.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from "@ng-select/ng-select";
import { UserlistComponent } from './userlist/userlist.component';
import { OrderListPipe } from './pipe/order-list.pipe';

@NgModule({
  declarations: [
    UsercardsComponent,
    UsermaintenanceComponent,
    UserlistComponent,
    OrderListPipe
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,

  ],
  exports:[
    OrderListPipe
  ]
})
export class UserModule { }
