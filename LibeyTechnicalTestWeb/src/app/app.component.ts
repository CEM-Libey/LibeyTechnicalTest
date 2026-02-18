import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'LibeyTechnicalTest';
  documentNumber: string = '';

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  findLibeyUser() {
    if (this.documentNumber && this.documentNumber.trim() !== '') {
      this.router.navigate(['/user/list'], {
        queryParams: { documentNumber: this.documentNumber }
      });
    } else {
      this.router.navigate(['/user/list']);
    }
  }
}
