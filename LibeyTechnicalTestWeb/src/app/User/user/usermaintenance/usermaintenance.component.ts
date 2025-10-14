import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';

import { DocumentTypeService } from 'src/app/core/service/documenttype/documenttype.service';
import { RegionService } from 'src/app/core/service/region/region.service';
import { ProvinceService } from 'src/app/core/service/province/province.service';
import { UbigeoService } from 'src/app/core/service/ubigeo/ubigeo.service';

@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  documentNumber: string | null = null;
  form: FormGroup;

  // Mock choices (puedes cambiarlos por llamadas a servicios cuando existan endpoints)
  //documentTypes = [ { id: 1, name: 'DNI' }, { id: 2, name: 'RUC' }, { id: 3, name: 'CE' } ];
  //departments = [ { code: '15', name: 'Lima' }, { code: '01', name: 'Amazonas' } ];
  //provinces = [ { code: '1501', name: 'Lima' }, { code: '0101', name: 'Chachapoyas' } ];
  //districts = [ { code: '150101', name: 'Lima' }, { code: '150102', name: 'Ancón' } ];

  documentTypes:any[] = [];
  departments:any[] = [];
  provinces:any[] = [];
  districts:any[] = [];

  provincesAux:any[] = [];
  districtsAux:any[] = [];

  _codigoDepartamento = "";
  _codigoProvincia = ""; 

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private libeyUserService: LibeyUserService,
    private documentTypeService: DocumentTypeService,
    private regionService: RegionService,
    private provinceService: ProvinceService,
    private ubigeoService: UbigeoService,
    private router: Router
  ) {
    this.form = this.fb.group({
      documentTypeId: [null, Validators.required],
      documentNumber: ['', Validators.required],
      name: ['', Validators.required],
      fathersLastName: ['', Validators.required],
      mothersLastName: [''],
      address: [''],
      regionCode: [null],
      provinceCode: [null],
      ubigeoCode: [null],
      phone: [''],
      email: ['', [Validators.email]],
      password: [''],
      active: [true]
    });
  }

  ngOnInit(): void { 
    this.route.queryParamMap.subscribe(qparams => {
      const qdoc = qparams.get('documentNumber');
      if (qdoc) {
        this.documentNumber = qdoc;
        this.loadUser(qdoc);
      }
    }); 
    this.loadInitialData();
  }


  loadInitialData() {

    this.documentTypeService.getAll().subscribe(data => {
      this.documentTypes = data || [];
      if(data && data.length > 0 && !this.form.get('documentTypeId')?.value) {
        this.form.get('documentTypeId')?.setValue(data[0].documentTypeId);
      }
    });

    this.regionService.getAll().subscribe(data => {
      debugger;
      this.departments = data || [];
      if(data && data.length > 0 && !this.form.get('regionCode')?.value) {
        this._codigoDepartamento = (data[0].regionCode);
        this.form.get('regionCode')?.setValue(this._codigoDepartamento); 
      }
    });

    this.provinceService.getAll().subscribe(data => {      
      if(data && data.length > 0 && !this.form.get('provinceCode')?.value) {  
        this.provincesAux = data || [];
        this.provinces = this.provincesAux.filter(p => p.regionCode === this._codigoDepartamento);
        this._codigoProvincia = (this.provinces[0].provinceCode);
        this.form.get('provinceCode')?.setValue(this._codigoProvincia);
      }
    });

    this.ubigeoService.getAll().subscribe(data => {   
      if(data && data.length > 0 && !this.form.get('ubigeoCode')?.value) { 
        this.districtsAux = data || [];
        this.districts = this.districtsAux.filter(p => p.provinceCode === this._codigoProvincia);        
        this.form.get('ubigeoCode')?.setValue(this.districts[0].ubigeoCode);
      }
    }); 

  }

  onRegionChange(regionCode: string) {
    debugger;
    const filteredProvinces = this.provincesAux.filter(p => p.regionCode === regionCode);
    if(filteredProvinces.length > 0) {
      this.provinces = filteredProvinces || [];
      this.form.get('provinceCode')?.setValue(filteredProvinces[0].provinceCode);
    } else {
      this.form.get('provinceCode')?.setValue(null);
    }
  }

  onProvinceChange(provinceCode: string) {
    debugger;
    const filteredDistricts = this.districtsAux.filter(d => d.provinceCode === provinceCode);
    if(filteredDistricts.length > 0) {
      this.districts = filteredDistricts || [];
      this.form.get('ubigeoCode')?.setValue(filteredDistricts[0].ubigeoCode);
    } else {
      this.form.get('ubigeoCode')?.setValue(null);
    }
  }


  loadUser(documentNumber: string) {
    this.libeyUserService.getUser(documentNumber).subscribe((u: LibeyUser) => {
      if (u) {
        this.form.patchValue({
          documentTypeId: u.documentTypeId,
          documentNumber: u.documentNumber,
          name: u.name,
          fathersLastName: u.fathersLastName,
          mothersLastName: u.mothersLastName,
          address: u.address,
          regionCode: u.regionCode,
          provinceCode: u.provinceCode,
          ubigeoCode: u.ubigeoCode,
          phone: u.phone,
          email: u.email,
          active: u.active
        });
      }
    }, err => {
      console.error('Error loading user', err);
      swal.fire('Error', 'No se pudo cargar el usuario', 'error');
    });
  }

  goBack() {
    this.router.navigate(['/user/card']);
  }

  Submit(){
    if (this.form.invalid) {
      swal.fire('Error', 'Complete los campos obligatorios', 'error');
      return;
    }

    const payload: LibeyUser = this.form.value as LibeyUser;

    if (this.documentNumber) {
      // update
      this.libeyUserService.updateUser(payload).subscribe(() => {
        swal.fire('Guardado', 'Usuario actualizado', 'success');
        this.router.navigate(['/user/card']);
      }, () => swal.fire('Error', 'No se pudo actualizar', 'error'));
    } else {
      // create
      this.libeyUserService.createUser(payload).subscribe(() => {
        swal.fire('Guardado', 'Usuario creado', 'success');
        this.router.navigate(['/user/card']);
      }, () => swal.fire('Error', 'No se pudo crear', 'error'));
    }
  }
}