import { Component, inject  } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { AgGridAngular } from 'ag-grid-angular';
import { ApiServiceAdquisition } from '../../services/adquisitions-api.service';

import type { ColDef } from "ag-grid-community";
import { ClientSideRowModelModule } from 'ag-grid-community';
import { ModuleRegistry } from 'ag-grid-community';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';


ModuleRegistry.registerModules([ClientSideRowModelModule]);

interface Card {  
  title: string;
  icon: string;
  endpoint: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule, AgGridAngular, FormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  private apiService = inject(ApiServiceAdquisition);
  private fb = inject(FormBuilder);
  

  selectedCardIndex: number | null = null;
  selectedMessage: string = '';
  selectedEntity: string = '';

  entityForm: FormGroup;
  colDefs: any[] = [];
  
  defaultColDef: ColDef = {
    editable: true,
    sortable: true,
    filter: true,
    resizable: true
  };

  rowData: any[] = [];

  showModal: boolean = false;
  isAdquisition: boolean = false;

  adminUnits: { adminUnitID: number, name: string }[] = [];
  assetServiceTypes: { assetServiceTypeID: number, name: string }[] = [];
  providers: { providerID: number, name: string }[] = [];
  

  constructor() {
    this.entityForm = this.fb.group({
      name: ['', Validators.required],
      referenceCode: ['', Validators.required]
    });
    
  }

  updateTotalPrice() {
    const quantity = this.entityForm.get('quantity')?.value || 0;
    const unitPrice = this.entityForm.get('unitPrice')?.value || 0;
    const totalPrice = quantity * unitPrice;
    this.entityForm.get('totalPrice')?.setValue(totalPrice, { emitEvent: false });
  }

  cards: Card[] = [
    { title: 'Adquisiciones', icon: 'https://www.adres.gov.co/Links de acceso/ico-consEPS.svg', endpoint: 'Adquisition/GetAll' },
    { title: 'Unidades', icon: 'https://www.adres.gov.co/Links de acceso/ico-porCiudadano.svg', endpoint: 'AdminUnit/GetAll' },
    { title: 'Proveedores', icon: 'https://www.adres.gov.co/Links de acceso/ico-tramLinea.svg', endpoint: 'Provider/GetAll' },
    { title: 'Bienes/Servicios', icon: 'https://www.adres.gov.co/Links de acceso/ico-pur.svg', endpoint: 'AssetServiceType/GetAll' },
    { title: 'Historial', icon: 'https://www.adres.gov.co/Links de acceso/ico-lup.svg', endpoint: 'api/AdquisitionHistory/GetAll' }
  ];

  selectCard(index: number) {
    this.selectedCardIndex = index;
    this.selectedEntity = this.cards[index].title;
    this.selectedMessage = `Seleccionaste: ${this.selectedEntity}`;
    
    
    this.isAdquisition = this.selectedEntity === 'Adquisiciones'; 

    switch (this.cards[index].title) {
      case 'Adquisiciones':
        this.entityForm = this.fb.group({
          adminUnitID: ['', Validators.required],
          assetServiceTypeID: ['', Validators.required],
          providerID: ['', Validators.required],
          budget: [0, [Validators.required, Validators.min(0)]],
          quantity: [1, [Validators.required, Validators.min(1)]],
          unitPrice: [0, [Validators.required, Validators.min(0.01)]],
          totalPrice: [{ value: 0, disabled: true }],
          documentation: ['', Validators.required],
          isActive: [false]
        });
  
        this.entityForm.get('quantity')?.valueChanges.subscribe(() => this.updateTotalPrice());
        this.entityForm.get('unitPrice')?.valueChanges.subscribe(() => this.updateTotalPrice());
  
        this.fetchDropdownData();

        this.colDefs = [
          { field: 'adquisitionID', headerName: 'ID', filter: true, editable:false },
          { field: 'adminUnitID', headerName: 'Admin Unit', filter: true, editable:true },
          { field: 'assetServiceTypeID', headerName: 'Asset Type', filter: true, editable:true },
          { field: 'providerID', headerName: 'Provider', filter: true, editable:true },
          { field: 'budget', headerName: 'Budget', filter: true, editable:true },
          { field: 'quantity', headerName: 'Quantity', filter: true, editable:true },
          { field: 'unitPrice', headerName: 'Unit Price', filter: true, editable:true },
          { field: 'totalPrice', headerName: 'Total Price', filter: true, editable:true }
        ];
        break;
      case 'Unidades':
        this.colDefs = [
          { field: 'adminUnitID', headerName: 'ID', filter: true, editable:false },
          { field: 'name', headerName: 'Nombre unidad', filter: true, editable:true },
          { field: 'referenceCode', headerName: 'Codigo de referencia', filter: true, editable:true }
        ];
        break;
      case 'Proveedores':
        this.colDefs = [
          { field: 'providerID', headerName: 'ID', filter: true, editable:false },
          { field: 'name', headerName: 'Nombre proveedor', filter: true, editable:true },
          { field: 'referenceCode', headerName: 'Codigo de referencia', filter: true, editable:true }
        ];
        break;
      case 'Bienes/Servicios':
        this.colDefs = [
          { field: 'assetServiceTypeID', headerName: 'ID', filter: true, editable:false },
          { field: 'name', headerName: 'Nombre Bien/Servicio', filter: true, editable:true },
          { field: 'referenceCode', headerName: 'Codigo de referencia', filter: true, editable:true }
        ];
        break;
      case 'Historial':
        this.colDefs = [
          { field: 'adquisitionHistoryID', headerName: 'ID', filter: true, editable:false },
          { field: 'adquisitionID', headerName: 'AdquisitionID', editable:true  },
          { field: 'operation', headerName: 'Operación', editable:true  },
          { field: 'TimeStamp', headerName: 'Fecha', editable:true  },
          { field: 'model', headerName: 'Snapshot', editable:true  }
        ];
        break;
    }
    console.log('Fetching Adquisiciones data...');
      this.apiService.getData(this.cards[index].endpoint).subscribe({
        next: (data) => {
          this.rowData = data; 
          console.log(`API Response for Adquisiciones:`, data);
        },
        error: (error) => {
          console.error('Error fetching Adquisiciones data:', error);
        }
      });
  }
  onRowClicked(event: any) {
    console.log('Fila clickeada:', event.data);
  }
  
  openModal() {
    this.entityForm.reset();
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  submitForm() {
    if (this.entityForm.invalid) {
      this.entityForm.markAllAsTouched(); 
      return;
    }
  

    let endpoint = '';
    switch (this.selectedEntity) {
      case 'Adquisiciones':
        endpoint = 'Adquisition/Add';
        break;
      case 'Unidades':
        endpoint = 'AdminUnit/Add';
        break;
      case 'Proveedores':
        endpoint = 'Provider/Add';
        break;
      case 'Bienes/Servicios':
        endpoint = 'AssetServiceType/Add';
        break;
      default:
        return;
    }
  

    this.apiService.postData(endpoint, this.entityForm.value).subscribe({
      next: (response) => {
        console.log("Registro exitoso:", response);
        alert(`guardado correctamente.`);
        this.entityForm.reset(); 
        this.closeModal(); 
        if (this.selectedCardIndex !== null) {
          this.selectCard(this.selectedCardIndex);
        }
      },
      error: (err) => {
        console.error("Error al guardar:", err);
        alert(`Error al guardar ${this.selectedEntity}. Inténtalo de nuevo.`);
      }
    });
  }
  

  fetchDropdownData() {
    this.apiService.getData(this.cards[1].endpoint).subscribe(data => {
      this.adminUnits = data;
    });

    this.apiService.getData(this.cards[3].endpoint).subscribe(data => {
      this.assetServiceTypes = data;
    });

    this.apiService.getData(this.cards[2].endpoint).subscribe(data => {
      this.providers = data;
    });
  }

  onCellValueChanged(event: any) {
    const updatedData = event.data;
    console.log('Celda editada:', updatedData);
  
    // // Enviar la actualización al backend
    // this.apiService.updateData(this.selectedEntity, updatedData).subscribe({
    //   next: () => {
    //     console.log('Datos actualizados correctamente.');
    //     alert('Registro actualizado correctamente.');
    //     this.refreshTable();
    //   },
    //   error: (error) => {
    //     console.error('Error al actualizar datos:', error);
    //     alert('Error al actualizar el registro.');
    //   }
    // });
  }
}
