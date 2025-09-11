import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import {HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LiveLocationComponent } from './features/location/live-location/live-location.component';
import { LocationHistoryComponent } from './features/location/location-history/location-history.component';

@NgModule({
  declarations: [
    AppComponent,
    LiveLocationComponent,
    LocationHistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
