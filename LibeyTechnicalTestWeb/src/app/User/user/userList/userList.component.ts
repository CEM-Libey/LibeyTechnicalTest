import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { UbigeoService } from 'src/app/core/service/ubigeo/ubigeo.service';
import { Combo } from 'src/app/entities/combo';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { LibeyUserQuery } from 'src/app/entities/libeyUserQuery';

@Component({
  selector: 'app-userList',
  templateUrl: './userList.component.html',
  styleUrls: ['./userList.component.css']
})
export class UserListComponent implements OnInit {

  filterGroup!: FormGroup
  constructor(private LibeyUserService: LibeyUserService, private UbigeoService: UbigeoService, private formBuilder: FormBuilder) {
    this.query = {
      skip: this.skip,
      take: this.take
    } as LibeyUserQuery;
  }
  take = 10;
  skip = 0;
  query!: LibeyUserQuery;
  selectedUser!: LibeyUser | null;
  deleteUser!: LibeyUser | null;
  select!: string | null
  users: LibeyUser[] = []
  types: Combo[] = []
  regions: Combo[] = []
  ubigeos: Combo[] = []
  provinces: Combo[] = []
  total:number=0;
  ngOnInit() {
    this.loadUsers();
    this.loadDocumentTypes()
    this.loadRegions()
    this.filterGroup = this.formBuilder.group({
      documentTypeId: [null, []],
      documentNumber: ['', []],
      email: ['', []],
      phone: ['', []],
      name: ['', []],
      provinceCode: [{ value: null, disabled: true },],
      ubigeoCode: [{ value: null, disabled: true },],
      regionCode: [null,],
    })
  }
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
  loadUsers() {
    this.LibeyUserService.list(this.query).subscribe((response) => {
      this.users = response.list;
      this.total= response.total
    })
  }
  next() {
    
    this.query.skip += this.query.take;
    this.loadUsers();
  }
  previous() {
    if (this.query.skip > 0) {
      this.query.skip -= this.query.take;
      this.loadUsers();
    }
  }
  edit(user: LibeyUser) {
    this.selectedUser = { ...user };
    this.select=null;
    this.deleteUser=null;

  }
  detail(user: string) {
    this.select = user
    this.selectedUser=null;
    this.deleteUser=null;
  }
  
  delete(user: LibeyUser) {
    this.deleteUser = user;
    this.select=null;
    this.selectedUser==null

  }
  closeEdit() {
    this.selectedUser = null
    this.loadUsers()
  }
  closeDelete() {
    this.deleteUser = null
    this.loadUsers()
  }
  closeDetail() {
    this.select = null
    
  }
  loadDocumentTypes() {
    this.LibeyUserService.typeCombo().subscribe((response) => {
      this.types = response
    })
  }
  search() {
    const filter = this.filterGroup.value
    this.query.documentTypeId = filter.documentTypeId
    this.query.email = filter.email
    this.query.documentNumber = filter.documentNumber
    this.query.ubigeoCode = filter.ubigeoCode
    this.query.phone = filter.phone
    this.query.name = filter.name
    this.loadUsers()
  }
  onRegionChange() {
    const formValue = this.filterGroup.value;
    if (formValue.regionCode == null) {
      return;
    }
    this.filterGroup.get('provinceCode')?.enable();
    this.loadProvinces(formValue.regionCode);
  }
  onProvinceChange() {
    const formValue = this.filterGroup.value;
    if (formValue.provinceCode == null) {
      return;
    }
    this.filterGroup.get('ubigeoCode')?.enable();
    this.loadUbigeos(formValue.provinceCode);
  }
  onClear() {
    this.filterGroup.reset({
    });
    this.provinces = [];
    this.ubigeos = [];
    this.filterGroup.get('provinceCode')?.disable();
    this.filterGroup.get('ubigeoCode')?.disable();
    this.filterGroup.markAsPristine();
    this.filterGroup.markAsUntouched();
    this.query = {
      skip: 0,
      take: 10
    }
  }

}
