import { Component, OnInit } from '@angular/core';
import { CdbCalculationService } from '../services/cdb-calculation.service';
import { CdbCalculationResult } from '../models/cdb-calculation-result.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cdb-calculation',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cdb-calculation.component.html',
  styleUrls: ['./cdb-calculation.component.css']
})
export class CdbCalculationComponent implements OnInit {
  initialValue: number = 0;
  months: number = 2;
  result: CdbCalculationResult | undefined;
  errorMessage: string | undefined;

  constructor(private cdbCalculationService: CdbCalculationService) { }

  ngOnInit() {
    console.log('CdbCalculationComponent initialized');
  }

  calculateCdb() {
    this.errorMessage = undefined;
    this.result = undefined;

    this.cdbCalculationService.calculateCdb(this.initialValue, this.months)
      .subscribe({
        next: (response: CdbCalculationResult) => {
          this.result = response;
          console.log('Monthly Values:', response.monthlyValues);
        },
        error: (error: Error) => {
          this.errorMessage = error.message;
        }
      });
  }
}
