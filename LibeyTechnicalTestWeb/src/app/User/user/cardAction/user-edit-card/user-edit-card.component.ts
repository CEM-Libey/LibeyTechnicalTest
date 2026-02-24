import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { UbigeoService } from 'src/app/core/service/ubigeo/ubigeo.service';
import { Combo } from 'src/app/entities/combo';
import { LibeyUser } from 'src/app/entities/libeyuser';
import swal from 'sweetalert2';

@Component({
  selector: 'app-user-edit-card',
  templateUrl: './user-edit-card.component.html',
  styleUrls: ['./user-edit-card.component.css']
})
export class UserEditCardComponent implements OnInit {

  updateform!: FormGroup
  constructor(private UbigeoService: UbigeoService, private LibeyUserService: LibeyUserService, private formBuilder: FormBuilder) { }
  ubigeos: Combo[] = []
  types: Combo[] = []
  provinces: Combo[] = []
  regions: Combo[] = []
  @Input() user!: LibeyUser;
  @Output() close = new EventEmitter<void>();

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
    const formValue = this.updateform.value;
    if (formValue.regionCode == null) {
      return;
    }
    this.updateform.get('provinceCode')?.enable();
    this.loadProvinces(formValue.regionCode);
  }
  onProvinceChange() {
    const formValue = this.updateform.value;
    if (formValue.provinceCode == null) {
      return;
    }
    this.updateform.get('ubigeoCode')?.enable();
    this.loadUbigeos(formValue.provinceCode);
  }
  ngOnInit(): void {
    this.loadRegions();
    this.loadProvinces(this.user.regionCode)
    this.loadUbigeos(this.user.provinceCode)
    this.loadDocumentTypes();
    this.updateform = this.formBuilder.group({
      documentNumber: [{ value: this.user.documentNumber, disabled: true }, Validators.required],
      documentTypeId: [this.user.documentTypeId, [Validators.required]],
      name: [this.user.name, [Validators.required, Validators.maxLength(100)]],
      fathersLastName: [this.user.fathersLastName, [Validators.required, Validators.maxLength(200)]],
      mothersLastName: [this.user.mothersLastName, [Validators.required, Validators.maxLength(200)]],
      address: [this.user.address, [Validators.required, Validators.maxLength(200)]],
      email: [this.user.email, [Validators.required, Validators.email, Validators.maxLength(20)]],
      phone: [this.user.phone, [Validators.required, Validators.pattern(/^[0-9]{9}$/)]],
      password: [this.user.password, [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      provinceCode: [this.user.provinceCode, Validators.required],
      ubigeoCode: [this.user.ubigeoCode, Validators.required],
      regionCode: [this.user.regionCode, Validators.required],
      active: [true]
    })
   
  }
  get f() {
    return this.updateform.controls;
  }
  closeModal() {
    this.close.emit();
  }
  updateUser() {
    const formValue = this.updateform.getRawValue();
    this.LibeyUserService.update(formValue).subscribe((response) => {

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
