import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { UsercardsComponent } from "./User/user/usercards/usercards.component";
import { UsermaintenanceComponent } from "./User/user/usermaintenance/usermaintenance.component";

const routes: Routes = [
	{
		path: "",
		redirectTo: "/user/cards",
		pathMatch: "full",
	},
	{
		path: "user",
		children: [
			{ path: "cards", component: UsercardsComponent },
			{ path: "maintenance", component: UsermaintenanceComponent },
			{ path: "maintenance/:documentNumber", component: UsermaintenanceComponent },
			{ path: "", redirectTo: "cards", pathMatch: "full" }
		],
	},
	{ path: "**", redirectTo: "/user/cards" },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
