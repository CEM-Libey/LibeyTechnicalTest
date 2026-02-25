import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { ActivatedRoute } from '@angular/router';
import { LibeyUser } from 'src/app/entities/libeyuser';
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  user: LibeyUser = {
    documentNumber: '',
    documentTypeId: 1,
    name: '',
    fathersLastName: '',
    mothersLastName: '',
    address: '',
    regionCode: '',
    provinceCode: '',
    ubigeoCode: '',
    phone: '',
    email: '',
    password: '',
    active: true
  };
  users: LibeyUser[] = [];
  isEditMode = false;

  constructor(private service: LibeyUserService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const doc = params['documentNumber'];
      if(doc){
        this.service.Find(doc).subscribe(u => {
          if(u){
            this.user = u;
            this.isEditMode = true;
          }
        });
      }
    });
    this.loadUsers();
  }

  loadUsers(){
    this.service.GetAll().subscribe(res => this.users = res || []);
  }

  save(){
    if(!this.user.documentNumber){
      swal.fire('Validación', 'Ingrese número de documento', 'warning');
      return;
    }
    if(this.isEditMode){
      this.service.Update(this.user.documentNumber, this.user).subscribe(() => {
        swal.fire('OK','Usuario actualizado','success');
        this.clearForm();
        this.loadUsers();
      }, err => swal.fire('Error','No se pudo actualizar','error'));
    } else {
      this.service.Create(this.user).subscribe(() => {
        swal.fire('OK','Usuario creado','success');
        this.clearForm();
        this.loadUsers();
      }, err => swal.fire('Error','No se pudo crear','error'));
    }
  }

  edit(u: LibeyUser){
    this.user = { ...u };
    this.isEditMode = true;
  }

  remove(documentNumber: string){
    swal.fire({
      title: 'Confirmar',
      text: '¿Eliminar usuario? ',
      icon: 'warning',
      showCancelButton: true
    }).then(r => {
      if(r.isConfirmed){
        this.service.Delete(documentNumber).subscribe(() => {
          swal.fire('OK','Usuario eliminado','success');
          this.loadUsers();
        }, err => swal.fire('Error','No se pudo eliminar','error'));
      }
    });
  }

  clearForm(){
    this.user = {
      documentNumber: '',
      documentTypeId: 1,
      name: '',
      fathersLastName: '',
      mothersLastName: '',
      address: '',
      regionCode: '',
      provinceCode: '',
      ubigeoCode: '',
      phone: '',
      email: '',
      password: '',
      active: true
    };
    this.isEditMode = false;
  }
}