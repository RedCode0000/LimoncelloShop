import { TestBed } from '@angular/core/testing';

import { LimoncelloService } from './limoncello.service';

describe('LimoncelloService', () => {
  let service: LimoncelloService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LimoncelloService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
