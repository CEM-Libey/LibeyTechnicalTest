import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
import swal from 'sweetalert2';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent implements OnInit {

  users: LibeyUser[] = [];

  constructor( private route: ActivatedRoute,
  private service: LibeyUserService,
  private router: Router) { }



  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {

      const documentNumber = params['documentNumber'];

      if (documentNumber) {

        this.service.Find(documentNumber).subscribe({
          next: (response) => {
            this.users = [response];
          },
          error: () => {
            swal.fire("Oops!", "Usuario no encontrado", "error");
          }
        });

      } else {
        this.service.getAll().subscribe({
          next: (response) => {
            this.users = response;
          },
          error: () => {
            swal.fire("Oops!", "Error al cargar usuarios", "error");
          }
        });
      }
    });
  }

  deleteUser(documentNumber: string): void {
    swal.fire({
      title: '¿Estás seguro?',
      text: "¡No podrás revertir esto!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminarlo!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.service.delete(documentNumber).subscribe({
          next: () => {
            this.users = this.users.filter(user => user.documentNumber !== documentNumber);
            swal.fire('¡Eliminado!', 'El usuario ha sido eliminado.', 'success');
            this.service.getAll().subscribe({
              next: (response) => {
                this.users = response;
              }
            });
          },
          error: () => {
            swal.fire('Error', 'No se pudo eliminar el usuario.', 'error');
          }
        });
      }
    });
  }

  editUser(documentNumber: string) {
    this.router.navigate(['/user/maintenance'], {
      queryParams: { documentNumber: documentNumber }
    });
  }
}
