import { TestBed } from '@angular/core/testing';

import { CookieServiceAPI } from './cookie-service-api.service';

describe('CookieServiceAPIService', () => {
  let service: CookieServiceAPI;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CookieServiceAPI);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
