import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsercardsComponent } from './usercards/usercards.component';
import { UsermaintenanceComponent } from './usermaintenance/usermaintenance.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from "@ng-select/ng-select";
import { UserListComponent } from './userList/userList.component';
import { UserEditCardComponent } from './cardAction/user-edit-card/user-edit-card.component';
import { UserDeleteCardComponent } from './cardAction/user-delete-card/user-delete-card.component';
import { UserDetailCardComponent } from './cardAction/user-detail-card/user-detail-card.component';
@NgModule({
  declarations: [   
    UsercardsComponent,
    UsermaintenanceComponent,
    UserListComponent,
    UserEditCardComponent,
    UserDeleteCardComponent,
    UserDetailCardComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule    
  ]
})
export class UserModule { }