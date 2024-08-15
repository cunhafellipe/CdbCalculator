import { Routes } from '@angular/router';
import { CdbCalculationComponent } from './cdb-calculation/components/cdb-calculation.component';

export const routes: Routes = [
  { path: 'cdb-calculation', component: CdbCalculationComponent },
  { path: '', redirectTo: '/cdb-calculation', pathMatch: 'full' } // Define a rota padrão
];
