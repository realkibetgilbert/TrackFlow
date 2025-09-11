import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveLocationComponent } from './live-location.component';

describe('LiveLocationComponent', () => {
  let component: LiveLocationComponent;
  let fixture: ComponentFixture<LiveLocationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LiveLocationComponent]
    });
    fixture = TestBed.createComponent(LiveLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
