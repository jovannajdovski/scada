import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlarmTriggersComponent } from './alarm-triggers.component';

describe('AlarmTriggersComponent', () => {
  let component: AlarmTriggersComponent;
  let fixture: ComponentFixture<AlarmTriggersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlarmTriggersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlarmTriggersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
