import { TestBed } from '@angular/core/testing';

import { AdquisitionsApiService } from './adquisitions-api.service';

describe('AdquisitionsApiService', () => {
  let service: AdquisitionsApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdquisitionsApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
