import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LimoncelloDetailsComponent } from './limoncello-details.component';

describe('LimoncelloDetailsComponent', () => {
  let component: LimoncelloDetailsComponent;
  let fixture: ComponentFixture<LimoncelloDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LimoncelloDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LimoncelloDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
