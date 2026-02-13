import { TestBed } from '@angular/core/testing';
import { LOCALE_ID } from '@angular/core';
import { CdbCalculatorComponent } from './cdb-calculator.component';
import { CdbService } from '../../services/cdb.service';
import { of } from 'rxjs';

describe('CdbCalculatorComponent', () => {
  let mockCdbService: { calcular: jest.Mock };

  beforeEach(async () => {
    mockCdbService = { calcular: jest.fn() };
    await TestBed.configureTestingModule({
      imports: [CdbCalculatorComponent],
      providers: [
        { provide: CdbService, useValue: mockCdbService },
        { provide: LOCALE_ID, useValue: 'pt' },
      ],
    }).compileComponents();
  });

  it('deve exibir erro ao clicar em calcular com valor invalido', () => {
    const fixture = TestBed.createComponent(CdbCalculatorComponent);
    const component = fixture.componentInstance;
    component.valor = 0;
    component.prazoMeses = 12;

    component.calcular();

    expect(component.erro).toBe('O valor deve ser positivo.');
  });

  it('deve exibir erro ao clicar em calcular com prazo invalido', () => {
    const fixture = TestBed.createComponent(CdbCalculatorComponent);
    const component = fixture.componentInstance;
    component.valor = 1000;
    component.prazoMeses = 1;

    component.calcular();

    expect(component.erro).toBe('O prazo deve ser maior que 1 mês.');
  });

  it('deve exibir resultado ao clicar em calcular com dados validos', () => {
    mockCdbService.calcular.mockReturnValue(of({ valorBruto: 1100, valorLiquido: 1080, imposto: 20 }));
    const fixture = TestBed.createComponent(CdbCalculatorComponent);
    const component = fixture.componentInstance;
    component.valor = 1000;
    component.prazoMeses = 12;

    component.calcular();

    expect(component.resultado).toEqual({ valorBruto: 1100, valorLiquido: 1080, imposto: 20 });
  });
});
