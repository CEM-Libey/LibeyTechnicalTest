
import { Component, OnInit } from '@angular/core';
import {ConfigurationService} from  '../../../core/service/confi/Configuration.service';
import {LibeyUserService} from  '../../../core/service/libeyuser/libeyuser.service';
import {DocumentType} from '../../../entities/documentType'
import swal from 'sweetalert2';
import { Region } from '../../../entities/region';
import { Province } from '../../../entities/province';
import { Ubigeo } from '../../../entities/ubigeo';
import { ResponseMessage } from 'src/app/entities/responseMessage';
import {LibeyUserAddOrAlter } from './../../../entities/libeyuser';

@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  selectedRegion: string="01";
  selectedProvince:string="0101";

  libeyUserAddOrALter: LibeyUserAddOrAlter={
    documentNumber:"",
    documentTypeId:"",
    name:"",
    fathersLastName :"",
    mothersLastName :"",
    address :"",
    ubigeoCode :"",
    regionCode :"",
    provinceCode :"",
    phone :"",
    email :"",
    password :"",
  }
  documentTypes: DocumentType[]=[];
  regions: Region[]=[];
  provinces: Province[]=[];
  ubigeos:Ubigeo[]=[];

  messageResponse:ResponseMessage={
    message:"",
    messageType:""
  }

  constructor(private configService:ConfigurationService,private libeyUserService:LibeyUserService) { }
  ngOnInit(): void {
    this. getDocumentTypes();
    this.getRegion();
    this.getProvince();
    this.getDistrito();

  }
  getDocumentTypes() {
    this.configService.GetDocumentType().subscribe(
      (data: DocumentType[]) => {
        this.documentTypes = data;
        if (this.documentTypes.length > 0) {
          this.libeyUserAddOrALter.documentTypeId = this.documentTypes[0].documentTypeId;
          this.getProvince()
        }
      },
      error => {
      }
    );
  }

  getRegion() {
    this.configService.GetRegion().subscribe(
      (data: Region[]) => {
        this.regions = data;
        if (this.regions.length > 0) {
           this.libeyUserAddOrALter.regionCode = this.regions[0].regionCode;
        }
      },
      error => {
      }
    );
  }

  getProvince() {
    this.configService.GetProvince(this.libeyUserAddOrALter.regionCode).subscribe(
      (data: Province[]) => {
        this.provinces = data;
        if (this.provinces.length > 0) {
          this.libeyUserAddOrALter.provinceCode= this.provinces[0].provinceCode;
          this.getDistrito();
        }

      },
      error => {

      }
    );
  }

  getDistrito(){
    this.configService.GetUbigeo(this.libeyUserAddOrALter.regionCode,this.libeyUserAddOrALter.provinceCode).subscribe(
      (data:Ubigeo[])=>{
        this.ubigeos=data;
        if(this.ubigeos.length>0){
          this.libeyUserAddOrALter.ubigeoCode=this.ubigeos[0].ubigeoCode;
        }
      }
    )
  }

createlibeyUser(){
  this.libeyUserService.Create(this.libeyUserAddOrALter).subscribe(
    (data:ResponseMessage)=>{
      this.messageResponse=data;
      let TypeMessage :any =this.messageResponse.messageType
      swal.fire(this.messageResponse.messageType, this.messageResponse.message, TypeMessage);

    }, error => {
      swal.fire(this.messageResponse.messageType, this.messageResponse.message, 'error');
    }
  )
}

limpiar(){
  this.libeyUserAddOrALter={
    documentNumber:"",
    documentTypeId:"",
    name:"",
    fathersLastName :"",
    mothersLastName :"",
    address :"",
    ubigeoCode :"",
    regionCode :"",
    provinceCode :"",
    phone :"",
    email :"",
    password :"",
  }
}
  Submit(){
    swal.fire("Oops!", "Something went wrong!", "error");
  }

}
