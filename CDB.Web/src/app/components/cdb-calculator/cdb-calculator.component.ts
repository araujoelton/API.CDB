import { ChangeDetectorRef, Component } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CdbService, CdbResponse } from '../../services/cdb.service';

@Component({
  selector: 'app-cdb-calculator',
  standalone: true,
  imports: [FormsModule, DecimalPipe],
  templateUrl: './cdb-calculator.component.html',
  styleUrl: './cdb-calculator.component.css',
})
export class CdbCalculatorComponent {
  valor = 0;
  prazoMeses = 0;
  resultado: CdbResponse | null = null;
  erro = '';
  carregando = false;

  constructor(
    private readonly cdbService: CdbService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  calcular(): void {
    this.erro = '';
    this.resultado = null;

    if (this.valor <= 0) {
      this.erro = 'O valor deve ser positivo.';
      return;
    }

    if (this.prazoMeses <= 1) {
      this.erro = 'O prazo deve ser maior que 1 mês.';
      return;
    }

    this.carregando = true;
    this.cdbService.calcular(this.valor, this.prazoMeses).subscribe({
      next: (res) => {
        this.resultado = res;
        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.erro =
          err.error?.mensagem || err.message || 'Erro ao calcular. Verifique se a API está rodando.';
        this.carregando = false;
        this.cdr.detectChanges();
      },
    });
  }
}
