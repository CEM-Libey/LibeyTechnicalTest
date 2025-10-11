import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserListComponent implements OnInit {
  loading = false;
  error = '';
  search = '';
  users: LibeyUser[] = [];
  view: LibeyUser[] = [];

  constructor(
    private usersSvc: LibeyUserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll() {
    this.loading = true;
    this.usersSvc.FindAll().subscribe({
      next: (list) => {
        this.users = list ?? [];
        this.applyFilter();
        this.loading = false;
      },
      error: _ => {
        this.error = 'No se pudo cargar la lista';
        this.loading = false;
      }
    });
  }

  applyFilter() {
    const q = this.search.trim().toLowerCase();
    if (!q) { this.view = [...this.users]; return; }
    this.view = this.users.filter(u =>
      (u.documentNumber || '').toLowerCase().includes(q) ||
      (u.name || '').toLowerCase().includes(q) ||
      (u.fathersLastName || '').toLowerCase().includes(q) ||
      (u.mothersLastName || '').toLowerCase().includes(q) ||
      (u.email || '').toLowerCase().includes(q)
    );
  }

  newUser() {
    this.router.navigate(['/user/maintenance']);
  }

  editUser(u: LibeyUser) {
    this.router.navigate(
      ['/user/maintenance'],
      { queryParams: { documentNumber: u.documentNumber } }
    );
  }

  deleteUser(u: LibeyUser) {
    swal.fire({
      title: '¿Eliminar usuario?',
      text: `Documento: ${u.documentNumber}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then(res => {
      if (!res.isConfirmed) return;

      this.loading = true;
      this.usersSvc.Remove(u.documentNumber).subscribe({
        next: ok => {
          this.loading = false;
          if (ok) {
            swal.fire('Eliminado', 'El usuario fue eliminado', 'success');
            this.users = this.users.filter(x => x.documentNumber !== u.documentNumber);
            this.applyFilter();
          } else {
            swal.fire('Atención', 'No se pudo eliminar', 'info');
          }
        },
        error: _ => {
          this.loading = false;
          swal.fire('Error', 'Falló la eliminación', 'error');
        }
      });
    });
  }
}
