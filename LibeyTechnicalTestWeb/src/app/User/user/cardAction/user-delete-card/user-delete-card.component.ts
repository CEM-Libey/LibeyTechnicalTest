import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
import swal from 'sweetalert2';

@Component({
  selector: 'app-user-delete-card',
  templateUrl: './user-delete-card.component.html',
  styleUrls: ['./user-delete-card.component.css']
})
export class UserDeleteCardComponent implements OnInit {

  @Input() user!: LibeyUser;
  @Output() close = new EventEmitter<void>();
  constructor(private LibeyUserService: LibeyUserService) { }

  ngOnInit() {
  }
  closeModal() {
    this.close.emit();
  }
  deleteUser() {
    this.LibeyUserService.delete(this.user.documentNumber).subscribe((response) => {

      if (response.isSuccess) {
        swal.fire('Correcto', response.message, 'success');
      } else {
        swal.fire("Oops!", response.message, "error");
      }
      this.close.emit()
    }, err => {
      swal.fire("Oops!", "Ocurrio un Error!", "error");
    })
  }

}
