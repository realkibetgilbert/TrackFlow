import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { LocationToDisplay } from '../models/location-to-display.model';
import { LocationService } from '../services/location.service';
import { RealtimeService } from '../services/real-time.service';

@Component({
  selector: 'app-live-location',
  templateUrl: './live-location.component.html'
})
export class LiveLocationComponent implements OnInit, OnDestroy {
  deviceId = 1;
  location?: LocationToDisplay;
  history: LocationToDisplay[] = [];
  private sub?: Subscription;

  constructor(
    private locationService: LocationService,
    private realtime: RealtimeService
  ) {}

  ngOnInit(): void {
    this.loadHistory();
    this.realtime.startConnection(this.deviceId);

    this.sub = this.realtime.location$.subscribe(loc => {
      // update current
      this.location = loc;
      // add to top of history
      this.history.unshift(loc);
    });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
    this.realtime.stopConnection(this.deviceId);
  }

  private loadHistory() {
    this.locationService.getLocationHistory(this.deviceId).subscribe({
      next: data => this.history = data,
      error: err => console.error(err)
    });
  }
}
