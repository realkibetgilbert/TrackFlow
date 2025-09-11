import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { LocationToDisplay } from '../models/location-to-display.model';
import { Observable, Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RealtimeService {
  private hubUrl = 'https://localhost:7146/locationHub';
  private connection!: signalR.HubConnection;
  private locationSubject = new Subject<LocationToDisplay>();

  location$ = this.locationSubject.asObservable();

  startConnection(deviceId: number) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubUrl, { withCredentials: true })
      .withAutomaticReconnect()
      .build();

    this.connection.on('ReceiveLocation', (data: LocationToDisplay) => {
      this.locationSubject.next(data);
    });

    this.connection
      .start()
      .then(() => {
        console.log('SignalR connected');
        // Join group for this device so we only get relevant updates
        this.connection
          .invoke('JoinDeviceGroup', deviceId)
          .catch((err) => console.error('JoinDeviceGroup error', err));
      })
      .catch((err) => console.error('SignalR connection error', err));
  }

  stopConnection(deviceId: number) {
    if (!this.connection) return;
    // leave group and stop
    this.connection.invoke('LeaveDeviceGroup', deviceId).catch(() => {});
    this.connection.stop().catch(() => {});
  }
}
