import { Component, OnInit } from '@angular/core';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
import swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent implements OnInit {
  users: LibeyUser[] = [];
  constructor(private service: LibeyUserService, private router: Router) { }

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.service.GetAll().subscribe(res => this.users = res || []);
  }

  edit(u: LibeyUser){
    this.router.navigate(['/user/maintenance'], { queryParams: { documentNumber: u.documentNumber } });
  }

  remove(documentNumber: string){
    swal.fire({
      title: 'Confirmar',
      text: '¿Eliminar usuario? ',
      icon: 'warning',
      showCancelButton: true
    }).then(r => {
      if(r.isConfirmed){
        this.service.Delete(documentNumber).subscribe(()=>{
          swal.fire('OK','Usuario eliminado','success');
          this.load();
        }, err => swal.fire('Error','No se pudo eliminar','error'));
      }
    });
  }

}
