import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { CdbService } from './cdb.service';

describe('CdbService', () => {
  let service: CdbService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CdbService, provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(CdbService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('deve fazer POST para /api/cdb/calcular com os parametros corretos', () => {
    const mockResponse = { valorBruto: 1100, valorLiquido: 1080, imposto: 20 };

    service.calcular(1000, 12).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne('http://localhost:5054/api/cdb/calcular');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ valor: 1000, prazoMeses: 12 });
    req.flush(mockResponse);
  });
});
