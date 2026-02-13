import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CdbRequest {
  valor: number;
  prazoMeses: number;
}

export interface CdbResponse {
  valorBruto: number;
  valorLiquido: number;
  imposto: number;
}

@Injectable({
  providedIn: 'root',
})
export class CdbService {
  private readonly apiUrl = 'http://localhost:5054/api/cdb';

  constructor(private readonly http: HttpClient) {}

  calcular(valor: number, prazoMeses: number): Observable<CdbResponse> {
    return this.http.post<CdbResponse>(`${this.apiUrl}/calcular`, {
      valor,
      prazoMeses,
    });
  }
}
