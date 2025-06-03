import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
	LibeyUserService,
	UserSearchQuery,
	UserListResponse
} from '../../../core/service/libeyuser/libeyuser.service';
import { LibeyUser } from '../../../entities/libeyuser';

@Component({
	selector: 'app-usercards',
	templateUrl: './usercards.component.html',
	styleUrls: ['./usercards.component.css']
})
export class UsercardsComponent implements OnInit {
	users: LibeyUser[] = [];
	loading = false;
	error: string | null = null;

	// Paginación
	currentPage = 1;
	pageSize = 10;
	totalItems = 0;
	totalPages = 0;

	// Filtros de búsqueda
	searchQuery: UserSearchQuery = {
		documentNumber: '',
		name: '',
		email: '',
		active: undefined,
		page: 1,
		pageSize: 10
	};

	// Para mostrar modal de confirmación
	showDeleteModal = false;
	userToDelete: LibeyUser | null = null;

	constructor(
		private userService: LibeyUserService,
		private router: Router
	) { }

	ngOnInit(): void {
		this.loadUsers();
	}

	loadUsers(): void {
		this.loading = true;
		this.error = null;

		this.searchQuery.page = this.currentPage;
		this.searchQuery.pageSize = this.pageSize;

		// Limpiar campos vacíos
		const query = { ...this.searchQuery };
		if (!query.documentNumber) delete query.documentNumber;
		if (!query.name) delete query.name;
		if (!query.email) delete query.email;
		if (query.active === undefined) delete query.active;

		this.userService.getUsers(query).subscribe({
			next: (response: UserListResponse) => {
				this.users = response.users;
				this.totalItems = response.totalCount;
				this.totalPages = response.totalPages;
				this.loading = false;
			},
			error: (error) => {
				this.error = 'Error al cargar usuarios: ' + (error.error?.message || error.message);
				this.loading = false;
			}
		});
	}

	onSearch(): void {
		this.currentPage = 1;
		this.loadUsers();
	}

	onClearSearch(): void {
		this.searchQuery = {
			documentNumber: '',
			name: '',
			email: '',
			active: undefined,
			page: 1,
			pageSize: 10
		};
		this.currentPage = 1;
		this.loadUsers();
	}

	onPageChange(page: number): void {
		this.currentPage = page;
		this.loadUsers();
	}

	onCreateUser(): void {
		this.router.navigate(['/user/maintenance']);
	}

	onEditUser(user: LibeyUser): void {
		this.router.navigate(['/user/maintenance', user.documentNumber]);
	}

	onDeleteUser(user: LibeyUser): void {
		this.userToDelete = user;
		this.showDeleteModal = true;
	}

	confirmDelete(): void {
		if (this.userToDelete) {
			this.userService.deleteUser(this.userToDelete.documentNumber).subscribe({
				next: () => {
					this.showDeleteModal = false;
					this.userToDelete = null;
					this.loadUsers(); // Recargar la lista
				},
				error: (error) => {
					this.error = 'Error al eliminar usuario: ' + (error.error?.message || error.message);
					this.showDeleteModal = false;
					this.userToDelete = null;
				}
			});
		}
	}

	cancelDelete(): void {
		this.showDeleteModal = false;
		this.userToDelete = null;
	}

	getActiveStatusText(active: boolean): string {
		return active ? 'Activo' : 'Inactivo';
	}

	getActiveStatusClass(active: boolean): string {
		return active ? 'badge badge-success' : 'badge badge-danger';
	}

	// Utilidades para paginación
	getPaginationArray(): number[] {
		const pages: number[] = [];
		const startPage = Math.max(1, this.currentPage - 2);
		const endPage = Math.min(this.totalPages, this.currentPage + 2);

		for (let i = startPage; i <= endPage; i++) {
			pages.push(i);
		}

		return pages;
	}
}
