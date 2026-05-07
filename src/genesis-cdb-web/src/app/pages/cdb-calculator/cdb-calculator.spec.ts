import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CdbCalculator } from './cdb-calculator';

describe('CdbCalculator', () => {
  let component: CdbCalculator;
  let fixture: ComponentFixture<CdbCalculator>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CdbCalculator],
    }).compileComponents();

    fixture = TestBed.createComponent(CdbCalculator);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
