
import swal from 'sweetalert2';
import { Component, OnInit ,ViewEncapsulation } from '@angular/core';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css'],
  encapsulation: ViewEncapsulation.None

})
export class UserlistComponent implements OnInit {



  libeyUsers : Array<LibeyUser> = []

  constructor(private service:LibeyUserService ,private router: Router) { }

  optionSort:{
    property:string|null
    order:string
  }={
    property:null,
    order:'asc'
  }

  ngOnInit(): void {
    this.cargarData()
  }

  cargarData(){
    this.service.getAllLibeyUsers$()
    .subscribe((response:LibeyUser[]) => {
      console.log('Dataaa ---> ',response)
      this.libeyUsers=response
    })
  }

  cambiarOrden(property: string): void {
    const { order } = this.optionSort;

    // Cambiar el orden entre asc y desc
    this.optionSort = {
      property,
      order: order === 'asc' ? 'desc' : 'asc',
    };


    console.log('Orden actual:', this.optionSort, 'Datos ordenados:', this.libeyUsers);
  }


  editarUsuario(user: LibeyUser) {
    this.router.navigate(['/user/maintenance', user.documentNumber]);
  }

  cambiarEstatusUsuario(usuario: LibeyUser): void {
    const nuevoEstado = !usuario.active;
    const estadoTexto = nuevoEstado ? 'activar' : 'desactivar';

    swal.fire({
      title: `¿Estás seguro de que quieres ${estadoTexto} a ${usuario.name}?`,
      text: "¡Esta acción cambiará el estado del usuario!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: `Sí, ${estadoTexto}`,
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.service.updateUserStatus(usuario.documentNumber, nuevoEstado).subscribe(
          () => {
            swal.fire(
              '¡Actualizado!',
              `El estado del usuario ha sido ${nuevoEstado ? 'activado' : 'desactivado'}.`,
              'success'
            );
            // Actualiza el estado del usuario en la lista local
            usuario.active = nuevoEstado;
          },
          (error) => {
            console.error('Error al actualizar el estado del usuario:', error);
            swal.fire('Error', 'Hubo un problema al actualizar el estado del usuario.', 'error');
          }
        );
      }
    });
  }


  eliminarUsuario(usuario: LibeyUser): void {
    swal.fire({
      title: `¿Estás seguro de que quieres eliminar a ${usuario.name} ${usuario.fathersLastName} ${usuario.mothersLastName}?`,
      text: "¡Esta acción no se puede deshacer!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result: { isConfirmed: any; }) => {
      if (result.isConfirmed) {
        this.service.deleteLibeyUser(usuario.documentNumber).subscribe(
          () => {
            swal.fire('¡Eliminado!', 'El usuario ha sido eliminado.', 'success');
            // Actualiza la lista de usuarios después de la eliminación
            this.cargarData();
          },
          (error) => {
            console.error('Error al eliminar el usuario:', error);
            swal.fire('Error', 'Hubo un problema al eliminar el usuario.', 'error');
          }
        );
      }
    });
  }


  IrCrearUsuarioFrm() {
    this.router.navigate(['/user/maintenance']);
  }
}
