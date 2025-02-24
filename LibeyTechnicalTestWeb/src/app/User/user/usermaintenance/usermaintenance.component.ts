
import swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LibeyUser } from 'src/app/entities/libeyuser';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { TipoDocumento } from 'src/app/entities/documenttype';
import { Distrito, Provincia, Region } from 'src/app/entities/ubigeo';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {

  documentNumber!: string;
  modoEdicion: boolean = false;

  documentTypes: TipoDocumento[] = [];

  selectedRegionCode: string | null = null;
  selectedProvinceCode: string | null = null;
  selectedDistrictCode: string | null = null;

  regiones: Region[] = [];
  provincias: Provincia[] = [];
  distritos: Distrito[] = [];

  userFormData: LibeyUser = {
    documentNumber: '',
    documentTypeId: null,
    documentTypeDescription: '',
    name: '',
    fathersLastName: '',
    mothersLastName: '',
    address: '',
    regionCode: '',
    regionDescription: '',
    provinceCode: '',
    provinceDescription: '',
    ubigeoCode: '',
    ubigeoDescription: '',
    phone: '',
    email: '',
    password: '',
    active: null,
  };

  constructor(private route: ActivatedRoute,  private userService: LibeyUserService ,private router: Router ) {}

  ngOnInit(): void {
    this.CargarTipoDocumentos();
    this.CargarUbigeo();

    this.route.paramMap.subscribe((params) => {
      this.documentNumber = params.get('documentNumber') ?? '';
      if (this.documentNumber) {
        this.cargarDatosUsuario(this.documentNumber);
        this.modoEdicion=true;
      }
    });
  }

  Submit(): void {
    this.userFormData.ubigeoCode = this.selectedDistrictCode;
    this.userFormData.provinceCode = this.selectedProvinceCode;
    this.userFormData.regionCode = this.selectedRegionCode;

    if (this.modoEdicion) {
      this.userService.updateLibeyUser(this.userFormData).subscribe(
        (response: HttpResponse<any>) => {
          if (response.status === 200) {
            const mensaje = response.body || 'El usuario se actualizó correctamente.';
            swal.fire('¡Actualizado!', mensaje, 'success');
            this.router.navigate(['/user/list']);
          } else {
            swal.fire('Error', 'Ocurrió un error inesperado.', 'error');
          }
        },
        (error: HttpErrorResponse) => {
          console.error('Error al actualizar:', error);
          const mensajeError = error.error || 'Ocurrió un error al actualizar el usuario.';
          swal.fire('Error', mensajeError, 'error');
        }
      );
    } else {
      console.log('Crear usuario:', this.userFormData);
    this.userService.createLibeyUser(this.userFormData).subscribe(
      (response: HttpResponse<any>) => {
        console.log('Respuesta Create', response);
        if (response.status === 200 || response.status === 201) {
          const mensaje = response.body?.message || 'El usuario se creó correctamente.';
          swal.fire('¡Creado!', mensaje, 'success');
          this.router.navigate(['/user/list']);
        } else {
          swal.fire('Error', 'Ocurrió un error inesperado.', 'error');
        }
      },
      (error: HttpErrorResponse) => {
        console.error('Error al crear:', error);
        const mensajeError = error.error?.message || 'Ocurrió un error al crear el usuario.';
        swal.fire('Error', mensajeError, 'error');
      }
    );
    }
  }

  cargarDatosUsuario(documentNumber: string) {
    this.userService.Find(documentNumber).subscribe(
      (response: LibeyUser) => {
        this.userFormData = response;

      this.selectedRegionCode = this.userFormData.regionCode;
      this.selectedProvinceCode = this.userFormData.provinceCode;
      this.selectedDistrictCode = this.userFormData.ubigeoCode;

      // Carga las opciones relacionadas
      this.cargarProvincias();
      this.cargarDistritos();
      },
      (error) => {
        console.error('Error al cargar usuario:', error);
        swal.fire('Error', 'No se pudo cargar la información del usuario.', 'error');
      }
    );
  }



  CargarTipoDocumentos(): void {
    this.userService.getAllDocumentTypes$().subscribe(
      (data) => {
        this.documentTypes = data;
        console.log('Tipos de documento cargados:', this.documentTypes);
      },
      (error) => {
        console.error('Error al cargar los tipos de documento', error);
      }
    );
  }

  CargarUbigeo(): void {
    this.userService.getUbigeo$().subscribe(
      (data) => {
        console.log('Ubigeo cargado:', data);
      this.regiones = data.regiones;
      this.precargaUbigeo();
      },
      (error) => {
        console.error('Error al cargar ubigeo:', error);
      }
    );
  }


  precargaUbigeo(): void {
    if (this.userFormData.regionCode) {
      const selectedRegion = this.regiones.find(
        (region) => region.codigoRegion === this.userFormData.regionCode
      );
      this.provincias = selectedRegion ? selectedRegion.provincias : [];
    }

    if (this.userFormData.provinceCode) {
      const selectedProvince = this.provincias.find(
        (provincia) => provincia.codigoProvincia === this.userFormData.provinceCode
      );
      this.distritos = selectedProvince ? selectedProvince.distritos : [];
    }
  }


  cargarProvincias(): void {
    if (this.selectedRegionCode) {
      const regionSeleccionada = this.regiones.find(region => region.codigoRegion === this.selectedRegionCode);
      this.provincias = regionSeleccionada ? regionSeleccionada.provincias : [];

      if (this.selectedRegionCode === this.userFormData.regionCode) {
        //recuperar las provicinas
        this.selectedProvinceCode = this.userFormData.provinceCode;
        this.cargarDistritos();

      }else{
        // resetear las provincias y distritos
        this.selectedProvinceCode = null;
        this.selectedDistrictCode = null;
      }
    } else {
      this.provincias = this.regiones.flatMap(region => region.provincias);
    }

  }
  cargarDistritos(): void {
    if (this.selectedProvinceCode) {
      const provinciaSeleccionada = this.provincias.find(prov => prov.codigoProvincia === this.selectedProvinceCode);
      this.distritos = provinciaSeleccionada ? provinciaSeleccionada.distritos : [];

      if (this.selectedProvinceCode===this.userFormData.provinceCode) {
        //recuperar los distritos
        this.selectedDistrictCode = this.userFormData.ubigeoCode;
      }else{
        //resetear distritos
        this.selectedDistrictCode = null;
      }
    } else {
      this.distritos = this.provincias.flatMap(prov => prov.distritos);
    }
  }

  VolverForm() {
    this.router.navigate(['/user/list']);
  }

  LimpiarForm() {
    this.userFormData = {
      documentNumber: '',
      documentTypeId: null,
      documentTypeDescription: '',
      name: '',
      fathersLastName: '',
      mothersLastName: '',
      address: '',
      regionCode: null,
      regionDescription: '',
      provinceCode: null,
      provinceDescription: '',
      ubigeoCode: null,
      ubigeoDescription: '',
      phone: '',
      email: '',
      password: '',
      active: null,
    };

    this.selectedRegionCode = null;
    this.selectedProvinceCode = null;
    this.selectedDistrictCode = null;
  }


}
