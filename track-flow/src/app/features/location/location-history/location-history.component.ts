import { Component } from '@angular/core';
import { LocationService } from '../services/location.service';
import { LocationToDisplay } from '../models/location-to-display.model';

@Component({
  selector: 'app-location-history',
  templateUrl: './location-history.component.html',
  styleUrls: ['./location-history.component.css']
})
export class LocationHistoryComponent {
history: LocationToDisplay[] = [];
  deviceId: number = 1; // same deviceId for now

  constructor(private locationService: LocationService) { }

  ngOnInit(): void {
    this.getHistory();
  }

  getHistory(): void {
    this.locationService.getLocationHistory(this.deviceId).subscribe({
      next: data => this.history = data,
      error: err => console.error(err)
    });
  }
}
