import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { CommonComponent } from './components/common/common.component';
import { FacultiesComponent } from './components/faculties/faculties.component';
import { SpecializationsComponent } from './components/specializations/specializations.component';
import { LocationsComponent } from './components/locations/locations.component';
import { BarChartComponent } from './components/bar-chart/bar-chart.component';
import { HttpClientModule } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { FullChartComponent } from './components/full-chart/full-chart.component';
import { ScatterChartComponent } from './components/scatter-chart/scatter-chart.component';
import { PredictionsComponent } from './components/predictions/predictions.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CommonComponent,
    FacultiesComponent,
    SpecializationsComponent,
    LocationsComponent,
    BarChartComponent,
    FullChartComponent,
    ScatterChartComponent,
    PredictionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatInputModule,
    MatSelectModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatAutocompleteModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
