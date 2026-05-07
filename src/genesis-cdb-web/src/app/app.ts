import { Component } from '@angular/core';

import { CdbCalculator } from './pages/cdb-calculator/cdb-calculator';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CdbCalculator
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App { }