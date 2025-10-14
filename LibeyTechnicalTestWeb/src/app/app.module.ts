import { NgModule } from "@angular/core";
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NotFoundComponent } from './not-found/not-found.component'; 
import { RouterModule } from "@angular/router";
import { UserModule } from "./User/user/user.module";
@NgModule({
	declarations: [AppComponent, NotFoundComponent],
	imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, RouterModule,HttpClientModule , UserModule],
	providers: [],
	bootstrap: [AppComponent],
})
export class AppModule {}