import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { CdbRequest } from '../models/cdb-request';
import { CdbResponse } from '../models/cdb-response';

@Injectable({
  providedIn: 'root'
})
export class CdbService {

  private apiUrl =
    'https://localhost:7063/api/financial/calculate';

  constructor(
    private http: HttpClient
  ) { }

  calculate(
    request: CdbRequest
  ): Observable<CdbResponse> {

    return this.http.post<CdbResponse>(
      this.apiUrl,
      request);
  }
}