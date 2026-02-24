import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUserDetail } from 'src/app/entities/libeyUserDetail';

@Component({
  selector: 'app-user-detail-card',
  templateUrl: './user-detail-card.component.html',
  styleUrls: ['./user-detail-card.component.css']
})
export class UserDetailCardComponent implements OnInit {
  @Input() user!: string;
  @Output() close = new EventEmitter<void>();
  userDetail!: LibeyUserDetail
  constructor(private LibeyUserService: LibeyUserService) { }

  ngOnInit() {
    this.loadDetail()
  }
  loadDetail() {
    this.LibeyUserService.find(this.user).subscribe((response) => {
      this.userDetail = response
    })
  }
  closeModal() {
    this.close.emit();
  }
}
