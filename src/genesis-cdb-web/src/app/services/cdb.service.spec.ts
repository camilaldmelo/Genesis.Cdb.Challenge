import {
  HttpTestingController,
  provideHttpClientTesting
} from '@angular/common/http/testing';

import {
  provideHttpClient
} from '@angular/common/http';

import { TestBed } from '@angular/core/testing';

import { CdbService } from './cdb.service';

describe('CdbService', () => {
  let service: CdbService;
  let httpMock: HttpTestingController;

  const apiUrl =
    'https://localhost:7063/api/financial/calculate';

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        CdbService,
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(CdbService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should send POST request to calculate CDB', () => {
    const request = {
      initialAmount: 1000,
      months: 12
    };

    const response = {
      grossAmount: 1123.08,
      netAmount: 1098.47
    };

    service.calculate(request).subscribe(result => {
      expect(result).toEqual(response);
    });

    const httpRequest = httpMock.expectOne(apiUrl);

    expect(httpRequest.request.method).toBe('POST');
    expect(httpRequest.request.body).toEqual(request);

    httpRequest.flush(response);
  });
});