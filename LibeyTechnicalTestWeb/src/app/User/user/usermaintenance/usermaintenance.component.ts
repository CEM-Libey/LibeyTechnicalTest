import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  user: LibeyUser = {
    documentNumber: '',
    documentTypeId: 0,
    name: '',
    fathersLastName : '',
    mothersLastName : '',
    address : '',
    regionCode : '',
    provinceCode : '',       
    ubigeoCode : '',
    phone : '',
    email : '',
    password : '',
    active : true
  };

  isEditMode: boolean = false;

  constructor(private service: LibeyUserService,
  private route: ActivatedRoute) { }

  ngOnInit(): void {
     this.route.queryParams.subscribe(params => {

    const documentNumber = params['documentNumber'];

    if (documentNumber) {
      this.isEditMode = true;

      this.service.Find(documentNumber).subscribe({
        next: (response) => {
          this.user = response;
        }, 
        error: () => {
          swal.fire("Oops!", "Usuario no encontrado", "error");
        }
      });
    }

  });
  }

  clearFields(){
    this.user = {
      documentNumber: '',
      documentTypeId: 0,
      name: '',
      fathersLastName : '',
      mothersLastName : '',
      address : '',
      regionCode : '',
      provinceCode : '',       
      ubigeoCode : '',
      phone : '',
      email : '',
      password : '',
      active : true
    };
  }

  Submit(){
    if (this.isEditMode) {

      this.service.update(this.user).subscribe({
        next: () => {
          swal.fire("Actualizado!", "Usuario actualizado correctamente", "success");
          window.history.back();
        },
        error: () => {
          swal.fire("Oops!", "Error al actualizar", "error");
        }
      });

    } else {

      this.service.create(this.user).subscribe({
        next: () => {
          swal.fire("Creado!", "Usuario creado correctamente", "success");
        },
        error: () => {
          swal.fire("Oops!", "Error al crear", "error");
        }
      });

    }
  }

  goBack(){
    window.history.back();
  }
}