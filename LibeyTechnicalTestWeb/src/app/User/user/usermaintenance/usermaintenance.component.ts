import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { Combo } from 'src/app/entities/combo';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { UbigeoService } from 'src/app/core/service/ubigeo/ubigeo.service';
@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  public createform!: FormGroup;
  LibeyuserCommand!: LibeyUser
  constructor(private UbigeoService: UbigeoService, private LibeyUserService: LibeyUserService, private formBuilder: FormBuilder) {
    this.LibeyuserCommand = {} as LibeyUser;
  }
  ubigeos: Combo[] = []
  types: Combo[] = []
  provinces: Combo[] = []
  regions: Combo[] = []
  loadRegions() {
    this.UbigeoService.region().subscribe((response) => {
      this.regions = response
    })
  }
  loadProvinces(regionCode: string) {
    this.UbigeoService.provinces(regionCode).subscribe((response) => {
      this.provinces = response
    })
  }
  loadUbigeos(provinceCode: string) {
    this.UbigeoService.ubigeos(provinceCode).subscribe((response) => {
      this.ubigeos = response
    })
  }
  loadDocumentTypes() {
    this.LibeyUserService.typeCombo().subscribe((response) => {
      this.types = response
    })
  }
  onRegionChange() {
    const formValue = this.createform.value;
    if (formValue.regionCode == null) {
      return;
    }
    this.createform.get('provinceCode')?.enable();
    this.loadProvinces(formValue.regionCode);
  }
  onProvinceChange() {
    const formValue = this.createform.value;
    if (formValue.provinceCode == null) {
      return;
    }
    this.createform.get('ubigeoCode')?.enable();
    this.loadUbigeos(formValue.provinceCode);
  }
  ngOnInit(): void {
    this.loadRegions();
    this.loadDocumentTypes();
    this.createform = this.formBuilder.group({
      documentNumber: ['', [Validators.required, Validators.maxLength(200)]],
      documentTypeId: [null, [Validators.required]],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      fathersLastName: ['', [Validators.required, Validators.maxLength(200)]],
      mothersLastName: ['', [Validators.required, Validators.maxLength(200)]],
      address: ['', [Validators.required, Validators.maxLength(200)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(20)]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{9}$/)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      provinceCode: [{ value: null, disabled: true }, Validators.required],
      ubigeoCode: [{ value: null, disabled: true }, Validators.required],
      regionCode: [null, Validators.required],
      active: [true]
    })
  }
  Submit() {
    if (this.createform.invalid) {
      this.createform.markAllAsTouched();
      return;
    }
    const formValue = this.createform.value;
    this.LibeyUserService.create(formValue).subscribe((response) => {
      if (response.isSuccess) {
        swal.fire('Correcto', response.message, 'success');
      } else {
        swal.fire("Oops!", response.message, "error");
      }
    }, err => {
      swal.fire("Oops!", "Ocurrio un Error!", "error");
    })
  }
  onClear() {
    this.createform.reset({
      active: true
    });
    this.provinces = [];
    this.ubigeos = [];
    this.createform.get('provinceCode')?.disable();
    this.createform.get('ubigeoCode')?.disable();
    this.createform.markAsPristine();
    this.createform.markAsUntouched();
  }
  get f() {
    return this.createform.controls;
  }
}