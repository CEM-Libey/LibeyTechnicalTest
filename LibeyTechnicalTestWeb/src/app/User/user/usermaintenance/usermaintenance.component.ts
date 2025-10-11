import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { ChoicesService } from 'src/app/core/service/choices/choices.service';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { DocumentType } from 'src/app/entities/documentType';
import { Region } from 'src/app/entities/region';
import { Province } from 'src/app/entities/province';
import { Ubigeo } from 'src/app/entities/ubigeo';

@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {

  mode: 'create' | 'edit' = 'create';
  loading = false;

  model: LibeyUser = {
    documentTypeId: 1,
    documentNumber: '',
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

  docTypes: DocumentType[] = [];
  regions: Region[] = [];
  provinces: Province[] = [];
  ubigeos: Ubigeo[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private users: LibeyUserService,
    private choices: ChoicesService
  ) {}

  ngOnInit(): void {
    this.choices.FindDocumentsTypes().subscribe(d => this.docTypes = d);
    this.choices.FindRegions().subscribe(r => this.regions = r);

    const doc = this.route.snapshot.queryParamMap.get('documentNumber');
    if (doc) {
      this.mode = 'edit';
      this.loadUser(doc);
    }
  }

  loadUser(documentNumber: string) {
    this.loading = true;
    this.users.Find(documentNumber).subscribe({
      next: (u) => {
        this.model = { ...u };
        this.loading = false;

        if (u.regionCode) this.loadProvinces(u.regionCode);
        if (u.regionCode && u.provinceCode)
          this.loadUbigeos(u.provinceCode, u.regionCode);
      },
      error: () => {
        this.loading = false;
        swal.fire('Oops', 'No se encontró el usuario', 'error');
      }
    });
  }

  loadProvinces(regionCode: string) {
    this.choices.FindProvinces(regionCode).subscribe(p => this.provinces = p);
  }

  loadUbigeos(provinceCode: string, regionCode: string) {
    this.choices.FindUbigeos(provinceCode, regionCode).subscribe(u => this.ubigeos = u);
  }

  onRegionChange(regionCode: string) {
    this.model.regionCode = regionCode;
    this.model.provinceCode = '';
    this.model.ubigeoCode = '';
    this.provinces = [];
    this.ubigeos = [];
    if (regionCode) this.loadProvinces(regionCode);
  }

  onProvinceChange(provinceCode: string) {
    this.model.provinceCode = provinceCode;
    this.model.ubigeoCode = '';
    this.ubigeos = [];
    if (provinceCode && this.model.regionCode)
      this.loadUbigeos(provinceCode, this.model.regionCode);
  }

  Submit() {
    if (!this.model.documentNumber || !this.model.name) {
      swal.fire('Validación', 'Documento y nombre son obligatorios', 'warning');
      return;
    }

    this.loading = true;
    const op = this.mode === 'create'
      ? this.users.Create(this.model)
      : this.users.Update(this.model);

    op.subscribe({
      next: _ => {
        this.loading = false;
        swal.fire('Éxito', this.mode === 'create' ? 'Usuario creado' : 'Usuario actualizado', 'success');
        if (this.mode === 'create') this.clean();
      },
      error: _ => {
        this.loading = false;
        swal.fire('Oops', 'Error al guardar', 'error');
      }
    });
  }

  clean() {
    this.mode = 'create';
    this.model = {
      documentTypeId: 1,
      documentNumber: '',
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
    this.provinces = [];
    this.ubigeos = [];
  }

  goBack() {
    this.router.navigate(['/user/card']);
  }
}
