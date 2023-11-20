import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LimoncelloInfoComponent } from './limoncello-info.component';

describe('LimoncelloInfoComponent', () => {
  let component: LimoncelloInfoComponent;
  let fixture: ComponentFixture<LimoncelloInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LimoncelloInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LimoncelloInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
