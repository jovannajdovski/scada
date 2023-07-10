import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RtuPopupComponent } from './rtu-popup.component';

describe('RtuPopupComponent', () => {
  let component: RtuPopupComponent;
  let fixture: ComponentFixture<RtuPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RtuPopupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RtuPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
