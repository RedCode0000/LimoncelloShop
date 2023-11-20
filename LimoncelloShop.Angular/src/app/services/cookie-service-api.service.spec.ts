import { TestBed } from '@angular/core/testing';

import { CookieServiceAPIService } from './cookie-service-api.service';

describe('CookieServiceAPIService', () => {
  let service: CookieServiceAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CookieServiceAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
