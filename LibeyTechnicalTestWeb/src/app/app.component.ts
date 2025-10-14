import { Component, OnInit } from '@angular/core';
import { LibeyUserService } from './core/service/libeyuser/libeyuser.service';
import { LibeyUser } from './entities/libeyuser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'LibeyTechnicalTest'; 

  users: LibeyUser[] = [];
  page: number = 1;
  itemsPerPage: number = 10;

  constructor(
    private service: LibeyUserService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.service.getAll().subscribe(data => this.users = data);
  }

  create() {
    this.router.navigate(['/user/maintenance']);
  }

  edit(user: LibeyUser) {
    this.router.navigate(['/user/maintenance'], { queryParams: { documentNumber: user.documentNumber } });
  }

  delete(user: LibeyUser) {
    if (confirm('¿Eliminar usuario?')) {
      this.service.deleteUser(user.documentNumber!).subscribe(() => this.loadUsers());
    }
  }

}
