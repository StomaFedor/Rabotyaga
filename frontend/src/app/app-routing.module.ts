import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CommonComponent } from './components/common/common.component';
import { FacultiesComponent } from './components/faculties/faculties.component';
import { SpecializationsComponent } from './components/specializations/specializations.component';
import { LocationsComponent } from './components/locations/locations.component';
import { PredictionsComponent } from './components/predictions/predictions.component';

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'common', component:CommonComponent},
  {path: 'faculties', component:FacultiesComponent},
  {path: 'specializations', component:SpecializationsComponent},
  {path: 'locations', component:LocationsComponent},
  {path: 'predictions', component:PredictionsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
