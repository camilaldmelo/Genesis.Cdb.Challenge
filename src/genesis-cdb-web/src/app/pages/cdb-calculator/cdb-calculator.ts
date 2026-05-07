import { Component } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CdbService } from '../../services/cdb.service';

@Component({
  selector: 'app-cdb-calculator',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './cdb-calculator.html',
  styleUrl: './cdb-calculator.css'
})
export class CdbCalculator {

  initialAmount = 1000;

  months = 2;

  grossAmount?: number;

  netAmount?: number;

  errorMessage = '';

  constructor(
    private cdbService: CdbService
  ) { }

  calculate(): void {

    this.errorMessage = '';

    this.cdbService.calculate({
      initialAmount: this.initialAmount,
      months: this.months
    })
    .subscribe({
      next: (response) => {

        this.grossAmount =
          response.grossAmount;

        this.netAmount =
          response.netAmount;
      },

      error: () => {

        this.errorMessage =
          'Error calculating investment.';
      }
    });
  }
}