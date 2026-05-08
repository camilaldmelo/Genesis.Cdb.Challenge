import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';

import { CdbCalculator } from './cdb-calculator';
import { CdbService } from '../../services/cdb.service';
import { CdbRequest } from '../../models/cdb-request';
import { CdbResponse } from '../../models/cdb-response';

class CdbServiceMock {
  lastRequest?: CdbRequest;

  response: CdbResponse = {
    grossAmount: 1123.08,
    netAmount: 1098.47
  };

  shouldThrowError = false;

  errorResponse: unknown = {};

  calculate(request: CdbRequest) {
    this.lastRequest = request;

    if (this.shouldThrowError) {
      return throwError(() => this.errorResponse);
    }

    return of(this.response);
  }
}

describe('CdbCalculator', () => {
  let component: CdbCalculator;
  let fixture: ComponentFixture<CdbCalculator>;
  let cdbServiceMock: CdbServiceMock;

  beforeEach(async () => {
    cdbServiceMock = new CdbServiceMock();

    await TestBed.configureTestingModule({
      imports: [CdbCalculator],
      providers: [
        {
          provide: CdbService,
          useValue: cdbServiceMock
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CdbCalculator);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should calculate investment successfully', () => {
    component.initialAmount = 1000;
    component.months = 12;

    component.calculate();

    expect(cdbServiceMock.lastRequest).toEqual({
      initialAmount: 1000,
      months: 12
    });

    expect(component.grossAmount).toBe(1123.08);
    expect(component.netAmount).toBe(1098.47);
    expect(component.errorMessage).toBe('');
  });

  it('should show backend validation errors', () => {
    cdbServiceMock.shouldThrowError = true;

    cdbServiceMock.errorResponse = {
      error: {
        errors: {
          InitialAmount: [
            'Initial amount must be greater than zero.'
          ],
          Months: [
            'Months must be greater than 1.'
          ]
        }
      }
    };

    component.calculate();

    expect(component.errorMessage)
      .toContain('Initial amount must be greater than zero.');

    expect(component.errorMessage)
      .toContain('Months must be greater than 1.');
  });

  it('should show generic error when backend error is unknown', () => {
    cdbServiceMock.shouldThrowError = true;
    cdbServiceMock.errorResponse = {};

    component.calculate();

    expect(component.errorMessage)
      .toBe('Erro ao calcular investimento.');
  });
});