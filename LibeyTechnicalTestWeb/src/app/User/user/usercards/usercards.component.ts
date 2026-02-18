import { Component, OnInit } from "@angular/core";
import { LibeyUserService } from "src/app/core/service/libeyuser/libeyuser.service";
@Component({
	selector: "app-usercards",
	templateUrl: "./usercards.component.html",
	styleUrls: ["./usercards.component.css"],
})
export class UsercardsComponent implements OnInit {
	constructor(private libeyUserService: LibeyUserService) {}
	ngOnInit(): void {
		// this.libeyUserService.Find("75519211").subscribe(response => {
		// 	console.log(response, "User");
		// });
	}

	// listLibeyUsers() {
	// 	this.libeyUserService.getAll().subscribe(response => {
	// 		console.log(response, "List of users");
	// 	});
	// }
}