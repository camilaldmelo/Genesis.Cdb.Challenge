import { Component } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CdbService } from '../../services/cdb.service';
import { ChangeDetectorRef } from '@angular/core';

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
    private cdbService: CdbService,
    private changeDetectorRef: ChangeDetectorRef
  ) { }

  calculate(): void {

    this.errorMessage = '';
    this.grossAmount = undefined;
    this.netAmount = undefined;
    
    this.cdbService.calculate({
      initialAmount: this.initialAmount,
      months: this.months
    })
    .subscribe({
      next: (response) => {
        this.grossAmount = response.grossAmount;
        this.netAmount = response.netAmount;

        this.changeDetectorRef.detectChanges();
      },
      error: (error) => {
      console.error('API error:', error);

      this.errorMessage = this.getBackendErrorMessage(error);

      this.changeDetectorRef.detectChanges();
      }
    });
  }
  
  private getBackendErrorMessage(error: any): string {
  if (error?.error?.errors) {
    const errors = error.error.errors;

    return Object.keys(errors)
      .map(key => errors[key].join(' '))
      .join(' ');
  }

  if (error?.error?.title) {
    return error.error.title;
  }

  if (error?.error?.message) {
    return error.error.message;
  }

  if (typeof error?.error === 'string') {
    return error.error;
  }

  return 'Erro ao calcular investimento.';
}
}