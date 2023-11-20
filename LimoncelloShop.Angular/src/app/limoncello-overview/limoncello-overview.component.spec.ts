import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LimoncelloOverviewComponent } from './limoncello-overview.component';

describe('LimoncelloOverviewComponent', () => {
  let component: LimoncelloOverviewComponent;
  let fixture: ComponentFixture<LimoncelloOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LimoncelloOverviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LimoncelloOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
