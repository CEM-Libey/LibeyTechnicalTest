import { Component, OnInit } from '@angular/core';
import {ConfigurationService} from  '../../../core/service/confi/Configuration.service';
import {LibeyUserService} from  '../../../core/service/libeyuser/libeyuser.service';
import {DocumentType} from '../../../entities/documentType'
import swal from 'sweetalert2';
import { Region } from '../../../entities/region';
import { Province } from '../../../entities/province';
import { Ubigeo } from '../../../entities/ubigeo';
import { ResponseMessage } from 'src/app/entities/responseMessage';
import {LibeyUserAddOrAlter ,LibeyUser } from './../../../entities/libeyuser';


@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})

export class UserlistComponent implements OnInit {


  libeyUserAddOrALter: LibeyUserAddOrAlter={
    documentNumber:"",
    documentTypeId:"",
    name:"",
    fathersLastName :"",
    mothersLastName :"",
    address :"",
    ubigeoCode :"",
    regionCode :"01",
    provinceCode :"0101",
    phone :"",
    email :"",
    password :"",
  }
  documentTypes: DocumentType[]=[];
  regions: Region[]=[];
  provinces: Province[]=[];
  ubigeos:Ubigeo[]=[];

  validateConteo:boolean=true

  libeyUsers:LibeyUser[]=[];

  textBuscar:string="";
  messageResponse:ResponseMessage={
    message:"",
    messageType:""
  }
  constructor(private libeyUserService:LibeyUserService,private configService:ConfigurationService) { }

  ngOnInit(): void {
    this.Get();
    this.getDocumentTypes();
    this.getRegion() ;
    this.getProvince();
    this.getDistrito();

  }
  Get(){
      this.libeyUserService.Get().subscribe(
        (data: LibeyUser[]) => {
          this.libeyUsers = data;
        },
        error => {
        }
      );
    }
  GetText(){
      this.libeyUserService.GetText(this.textBuscar).subscribe(
        (data: LibeyUser[]) => {
          this.libeyUsers = data;
        },
        error => {
        }
      );
    }
  Delete(NumeroDoc:string ){
      this.libeyUserService.Delete(NumeroDoc).subscribe(
        (data: ResponseMessage) => {
          this.messageResponse=data;
          let TypeMessage :any =this.messageResponse.messageType
          swal.fire(this.messageResponse.messageType, this.messageResponse.message, TypeMessage);
          this.Get()
        },
        error => {
          swal.fire("Error", "Error al Eliminar", error);
        }
      );
    }

    ChangeState(NumeroDoc:string){
      this.libeyUserService.ChangeState(NumeroDoc).subscribe(
        (data: ResponseMessage) => {
          this.messageResponse=data;
          let TypeMessage :any =this.messageResponse.messageType
          swal.fire(this.messageResponse.messageType, this.messageResponse.message, TypeMessage);
          this.Get()
        },
        error => {
          swal.fire("Error", "Error al Cambiar estado", error);
        }
      );
    }

    FindUser(documento: string) {
      this.validateConteo = false;
      this.libeyUserService.Find(documento).subscribe(
        async (data: LibeyUserAddOrAlter) => {
          console.log(data);
            this.libeyUserAddOrALter = data;
            this.validateConteo = true;

        },
        error => {
          // Manejo de errores
        }
      );
    }
    getDocumentTypes() {
      this.configService.GetDocumentType().subscribe(
        (data: DocumentType[]) => {
          this.documentTypes = data;
          if (this.documentTypes.length > 0) {
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
            if(this.validateConteo==true){
            }
            this.getProvince()
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

            if(this.validateConteo==true){
            }
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
            if(this.validateConteo==true){
            }

          }

        }
      )
      this.validateConteo=true;
    }

    UpdatelibeyUser(){
      this.libeyUserService.Update(this.libeyUserAddOrALter).subscribe(
        (data:ResponseMessage)=>{
          this.messageResponse=data;
          let TypeMessage :any =this.messageResponse.messageType

          swal.fire(this.messageResponse.messageType, this.messageResponse.message, TypeMessage);

          this.Get();
        }, error => {
          swal.fire(this.messageResponse.messageType, this.messageResponse.message, 'error');
        }
      )
    }



}

