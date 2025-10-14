import { Component, OnInit } from "@angular/core";
import { LibeyUserService } from "src/app/core/service/libeyuser/libeyuser.service";
import { LibeyUser } from "src/app/entities/libeyuser";
import { Router } from '@angular/router';

@Component({
	selector: "app-usercards",
	templateUrl: "./usercards.component.html",
	styleUrls: ["./usercards.component.css"],
})
export class UsercardsComponent implements OnInit {
	users: LibeyUser[] = [];
	filtered: LibeyUser[] = [];
	searchTerm: string = '';

	// simple client-side pagination
	page: number = 1;
	itemsPerPage: number = 10;

	constructor(private libeyUserService: LibeyUserService, private router: Router) {}

	ngOnInit(): void {
		this.loadUsers();
	}

	loadUsers() {
		this.libeyUserService.getAll().subscribe(data => {
			this.users = data || [];
			this.applyFilter();
		});
	}

	applyFilter() {
		const term = this.searchTerm.trim().toLowerCase();
		if (!term) {
			this.filtered = [...this.users];
		} else {
			this.filtered = this.users.filter(u =>
				(u.name || '').toLowerCase().includes(term) ||
				(u.fathersLastName || '').toLowerCase().includes(term) ||
				(u.mothersLastName || '').toLowerCase().includes(term) ||
				(u.documentNumber || '').toLowerCase().includes(term) ||
				(u.email || '').toLowerCase().includes(term)
			);
		}
		this.page = 1;
	}

	create() {
		this.router.navigate(['/user/maintenance']);
	}

	edit(user: LibeyUser) {
		this.router.navigate(['/user/maintenance'], { queryParams: { documentNumber: user.documentNumber } });
	}

	delete(user: LibeyUser) {
		if (confirm('¿Eliminar usuario?')) {
			this.libeyUserService.deleteUser(user.documentNumber).subscribe(() => this.loadUsers());
		}
	}

	// pagination helpers
	get paged() {
		const start = (this.page - 1) * this.itemsPerPage;
		return this.filtered.slice(start, start + this.itemsPerPage);
	}

	prevPage() { if (this.page > 1) this.page--; }
	nextPage() { if ((this.page * this.itemsPerPage) < this.filtered.length) this.page++; }
}