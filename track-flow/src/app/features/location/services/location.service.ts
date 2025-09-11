import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LocationToDisplay } from '../models/location-to-display.model';

@Injectable({
  providedIn: 'root'
})
export class LocationService {


  private baseUrl = 'https://localhost:7146/api/location'; 

  constructor(private http: HttpClient) { }

  
  getLatestLocation(deviceId: number): Observable<LocationToDisplay> {
    return this.http.get<LocationToDisplay>(`${this.baseUrl}/live?deviceId=${deviceId}`);
  }

  
  getLocationHistory(deviceId: number): Observable<LocationToDisplay[]> {
    return this.http.get<LocationToDisplay[]>(`${this.baseUrl}/history?deviceId=${deviceId}`);
  }
}
