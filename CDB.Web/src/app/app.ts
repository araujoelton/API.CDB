import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CdbCalculatorComponent } from './components/cdb-calculator/cdb-calculator.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CdbCalculatorComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {}
