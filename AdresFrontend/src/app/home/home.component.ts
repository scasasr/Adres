import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridAngular } from 'ag-grid-angular';
import { ApiServiceAdquisition } from '../../services/adquisitions-api.service';
import type { ColDef } from "ag-grid-community";
import { ClientSideRowModelModule } from 'ag-grid-community';
import { ModuleRegistry } from 'ag-grid-community';


ModuleRegistry.registerModules([ClientSideRowModelModule]);

interface Card {  
  title: string;
  icon: string;
  endpoint: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, AgGridAngular],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  private apiService = inject(ApiServiceAdquisition);

  selectedCardIndex: number | null = null;
  selectedMessage: string = '';
  apiResponse: any = null;

  colDefs: any[] = [];
  
  defaultColDef: ColDef = {
    sortable: true,
    filter: true,
    resizable: true
  };

  rowData: any[] = [];

  showModal: boolean = false;

  constructor() {}

  cards: Card[] = [
    { title: 'Adquisiciones', icon: 'https://www.adres.gov.co/Links de acceso/ico-consEPS.svg', endpoint: 'api/Adquisition/GetAll' },
    { title: 'Unidades', icon: 'https://www.adres.gov.co/Links de acceso/ico-porCiudadano.svg', endpoint: 'api/AdminUnit/GetAll' },
    { title: 'Proveedores', icon: 'https://www.adres.gov.co/Links de acceso/ico-tramLinea.svg', endpoint: 'api/Provider/GetAll' },
    { title: 'Bienes/Servicios', icon: 'https://www.adres.gov.co/Links de acceso/ico-pur.svg', endpoint: 'api/AssetServiceType/GetAll' },
    { title: 'Historial', icon: 'https://www.adres.gov.co/Links de acceso/ico-lup.svg', endpoint: 'api/AdquisitionHistory/GetAll' }
  ];

  selectCard(index: number) {
    this.selectedCardIndex = index;
    this.selectedMessage = `Seleccionaste: ${this.cards[index].title}`;

    switch (this.cards[index].title) {
      case 'Adquisiciones':
        this.colDefs = [
          { field: 'adquisitionID', headerName: 'ID' },
          { field: 'adminUnitID', headerName: 'Admin Unit' },
          { field: 'assetServiceTypeID', headerName: 'Asset Type' },
          { field: 'providerID', headerName: 'Provider' },
          { field: 'budget', headerName: 'Budget' },
          { field: 'quantity', headerName: 'Quantity' },
          { field: 'unitPrice', headerName: 'Unit Price' },
          { field: 'totalPrice', headerName: 'Total Price' }
        ];
        break;
      case 'Unidades':
        this.colDefs = [
          { field: 'adminUnitID', headerName: 'ID' },
          { field: 'name', headerName: 'Nombre unidad' },
          { field: 'referenceCode', headerName: 'Codigo de referencia' }
        ];
        break;
      case 'Proveedores':
        this.colDefs = [
          { field: 'providerID', headerName: 'ID' },
          { field: 'name', headerName: 'Nombre proveedor' },
          { field: 'referenceCode', headerName: 'Codigo de referencia' }
        ];
        break;
      case 'Bienes/Servicios':
        this.colDefs = [
          { field: 'assetServiceTypeID', headerName: 'ID' },
          { field: 'name', headerName: 'Nombre Bien/Servicio' },
          { field: 'referenceCode', headerName: 'Codigo de referencia' }
        ];
        break;
      case 'Historial':
        this.colDefs = [
          { field: 'adquisitionHistoryID', headerName: 'ID' },
          { field: 'adquisitionID', headerName: 'AdquisitionID' },
          { field: 'operation', headerName: 'Operación' },
          { field: 'TimeStamp', headerName: 'Fecha' },
          { field: 'model', headerName: 'Snapshot' }
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

  openModal() {
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }
}
