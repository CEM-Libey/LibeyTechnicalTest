import { NgModule } from "@angular/core";
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { RouterModule } from "@angular/router";
import { UserModule } from "./User/user/user.module";


// import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
	declarations: [AppComponent],
	imports: [BrowserModule, AppRoutingModule, BrowserAnimationsModule, RouterModule,HttpClientModule , UserModule],
	providers: [],
	bootstrap: [AppComponent],
})
export class AppModule {}
