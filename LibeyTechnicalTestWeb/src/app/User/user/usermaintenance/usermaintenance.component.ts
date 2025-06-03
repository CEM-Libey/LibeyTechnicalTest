import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  LibeyUserService,
  UserCreateRequest,
  DocumentType,
  Ubigeo
} from '../../../core/service/libeyuser/libeyuser.service';
import { LibeyUser } from '../../../entities/libeyuser';

@Component({
  selector: 'app-usermaintenance',
  templateUrl: './usermaintenance.component.html',
  styleUrls: ['./usermaintenance.component.css']
})
export class UsermaintenanceComponent implements OnInit {
  userForm: FormGroup;
  loading = false;
  saving = false;
  error: string | null = null;
  success: string | null = null;
  isEditMode = false;
  currentDocumentNumber: string | null = null;

  // Catálogos
  documentTypes: DocumentType[] = [];
  ubigeos: Ubigeo[] = [];
  loadingCatalogs = true;

  constructor(
    private fb: FormBuilder,
    private userService: LibeyUserService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.userForm = this.createForm();
  }

  ngOnInit(): void {
    this.loadCatalogs();
    this.checkEditMode();
  }

  private createForm(): FormGroup {
    return this.fb.group({
      documentNumber: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(11),
        Validators.pattern(/^[0-9]+$/)
      ]],
      documentTypeId: [1, [Validators.required]],
      name: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/)
      ]],
      fathersLastName: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/)
      ]],
      mothersLastName: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/)
      ]],
      address: ['', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(200)
      ]],
      ubigeoCode: ['', [Validators.required]],
      phone: ['', [
        Validators.required,
        Validators.minLength(7),
        Validators.maxLength(15),
        Validators.pattern(/^[0-9+\-\s]+$/)
      ]],
      email: ['', [
        Validators.required,
        Validators.email,
        Validators.maxLength(100)
      ]],
      password: ['', [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(50)
      ]]
    });
  }

  private loadCatalogs(): void {
    this.loadingCatalogs = true;

    // Cargar tipos de documento
    this.userService.getDocumentTypes().subscribe({
      next: (types) => {
        this.documentTypes = types;
        this.checkCatalogsLoaded();
      },
      error: (error) => {
        this.error = 'Error al cargar tipos de documento: ' + (error.error?.message || error.message);
        this.checkCatalogsLoaded();
      }
    });

    // Cargar ubigeos
    this.userService.getUbigeos().subscribe({
      next: (ubigeos) => {
        this.ubigeos = ubigeos;
        this.checkCatalogsLoaded();
      },
      error: (error) => {
        this.error = 'Error al cargar ubigeos: ' + (error.error?.message || error.message);
        this.checkCatalogsLoaded();
      }
    });
  }

  private checkCatalogsLoaded(): void {
    if (this.documentTypes.length > 0 || this.ubigeos.length > 0) {
      this.loadingCatalogs = false;
    }
  }

  private checkEditMode(): void {
    const documentNumber = this.route.snapshot.paramMap.get('documentNumber');
    if (documentNumber) {
      this.isEditMode = true;
      this.currentDocumentNumber = documentNumber;
      this.loadUser(documentNumber);
      // En modo edición, deshabilitar el campo de documento
      this.userForm.get('documentNumber')?.disable();
    }
  }

  private loadUser(documentNumber: string): void {
    this.loading = true;
    this.userService.getUserByDocument(documentNumber).subscribe({
      next: (user: LibeyUser) => {
        this.populateForm(user);
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Error al cargar usuario: ' + (error.error?.message || error.message);
        this.loading = false;
      }
    });
  }

  private populateForm(user: LibeyUser): void {
    this.userForm.patchValue({
      documentNumber: user.documentNumber,
      documentTypeId: user.documentTypeId,
      name: user.name,
      fathersLastName: user.fathersLastName,
      mothersLastName: user.mothersLastName,
      address: user.address,
      ubigeoCode: user.ubigeoCode,
      phone: user.phone,
      email: user.email,
      password: user.password
    });
  }

  onSubmit(): void {
    if (this.userForm.valid) {
      this.saving = true;
      this.error = null;
      this.success = null;

      const formData = this.getFormData();

      if (this.isEditMode) {
        this.updateUser(formData);
      } else {
        this.createUser(formData);
      }
    } else {
      this.markFormGroupTouched();
    }
  }

  private getFormData(): UserCreateRequest {
    const formValue = this.userForm.value;
    return {
      documentNumber: this.isEditMode ? this.currentDocumentNumber! : formValue.documentNumber,
      documentTypeId: formValue.documentTypeId,
      name: formValue.name,
      fathersLastName: formValue.fathersLastName,
      mothersLastName: formValue.mothersLastName,
      address: formValue.address,
      ubigeoCode: formValue.ubigeoCode,
      phone: formValue.phone,
      email: formValue.email,
      password: formValue.password
    };
  }

  private createUser(userData: UserCreateRequest): void {
    this.userService.createUser(userData).subscribe({
      next: () => {
        this.success = 'Usuario creado exitosamente';
        this.saving = false;
        setTimeout(() => {
          this.router.navigate(['/user/cards']);
        }, 2000);
      },
      error: (error) => {
        this.error = 'Error al crear usuario: ' + (error.error?.message || error.message);
        this.saving = false;
      }
    });
  }

  private updateUser(userData: UserCreateRequest): void {
    this.userService.updateUser(this.currentDocumentNumber!, userData).subscribe({
      next: () => {
        this.success = 'Usuario actualizado exitosamente';
        this.saving = false;
        setTimeout(() => {
          this.router.navigate(['/user/cards']);
        }, 2000);
      },
      error: (error) => {
        this.error = 'Error al actualizar usuario: ' + (error.error?.message || error.message);
        this.saving = false;
      }
    });
  }

  private markFormGroupTouched(): void {
    Object.keys(this.userForm.controls).forEach(key => {
      const control = this.userForm.get(key);
      control?.markAsTouched();
    });
  }

  onCancel(): void {
    this.router.navigate(['/user/cards']);
  }

  onReset(): void {
    if (this.isEditMode) {
      this.loadUser(this.currentDocumentNumber!);
    } else {
      this.userForm.reset();
      this.userForm.get('documentTypeId')?.setValue(1);
    }
    this.error = null;
    this.success = null;
  }

  // Métodos auxiliares para validaciones en el template
  hasError(fieldName: string, errorType: string): boolean {
    const field = this.userForm.get(fieldName);
    return field ? field.hasError(errorType) && (field.dirty || field.touched) : false;
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.userForm.get(fieldName);
    return field ? field.invalid && (field.dirty || field.touched) : false;
  }

  getFieldError(fieldName: string): string {
    const field = this.userForm.get(fieldName);
    if (field?.errors && (field.dirty || field.touched)) {
      const errors = field.errors;

      if (errors['required']) return `${this.getFieldDisplayName(fieldName)} es requerido`;
      if (errors['minlength']) return `${this.getFieldDisplayName(fieldName)} debe tener al menos ${errors['minlength'].requiredLength} caracteres`;
      if (errors['maxlength']) return `${this.getFieldDisplayName(fieldName)} no puede tener más de ${errors['maxlength'].requiredLength} caracteres`;
      if (errors['pattern']) return `${this.getFieldDisplayName(fieldName)} tiene un formato inválido`;
      if (errors['email']) return 'El email debe tener un formato válido';
    }
    return '';
  }

  private getFieldDisplayName(fieldName: string): string {
    const displayNames: { [key: string]: string } = {
      documentNumber: 'Número de documento',
      documentTypeId: 'Tipo de documento',
      name: 'Nombre',
      fathersLastName: 'Apellido paterno',
      mothersLastName: 'Apellido materno',
      address: 'Dirección',
      ubigeoCode: 'Ubigeo',
      phone: 'Teléfono',
      email: 'Email',
      password: 'Contraseña'
    };
    return displayNames[fieldName] || fieldName;
  }

  getDocumentTypeDescription(id: number): string {
    const docType = this.documentTypes.find(dt => dt.id === id);
    return docType ? docType.description : '';
  }

  getUbigeoDescription(code: string): string {
    const ubigeo = this.ubigeos.find(u => u.ubigeoCode === code);
    return ubigeo ? ubigeo.description : '';
  }

  getPageTitle(): string {
    return this.isEditMode ? 'Editar Usuario' : 'Crear Nuevo Usuario';
  }

  getSubmitButtonText(): string {
    if (this.saving) {
      return this.isEditMode ? 'Actualizando...' : 'Creando...';
    }
    return this.isEditMode ? 'Actualizar Usuario' : 'Crear Usuario';
  }
}
