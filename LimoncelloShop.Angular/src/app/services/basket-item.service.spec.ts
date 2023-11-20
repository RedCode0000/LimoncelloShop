import { TestBed } from '@angular/core/testing';

import { BasketItemService } from './basket-item.service';

describe('BasketItemService', () => {
  let service: BasketItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BasketItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
