import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CdbCalculationResult } from './cdb-calculation-result.model';

@Component({
  selector: 'app-cdb-calculation',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cdb-calculation.component.html'
})
export class CdbCalculationComponent implements OnInit {
  initialValue: number = 0;
  months: number = 2;
  result: CdbCalculationResult | undefined;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    console.log('CdbCalculationComponent initialized');
  }

  calculateCdb() {
    this.http.post<CdbCalculationResult>('http://localhost:5240/api/CdbCalculation', {
      InitialValue: this.initialValue,
      Months: this.months
    })
      .subscribe({
        next: (response: CdbCalculationResult) => {
          this.result = response;
          console.log('Monthly Values:', response.monthlyValues);
        },
        error: (error: any) => {
          console.error('Error calculating CDB:', error);
        }
      });
  }

}
