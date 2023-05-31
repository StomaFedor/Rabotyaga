import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { ListModel } from 'src/app/models/list.model';
import { LocationsModel } from 'src/app/models/locations.model';
import { environment } from '../../../enviroments/enviroment';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.css']
})
export class LocationsComponent {
  private httpClient: HttpClient;
  public locationsStat: LocationsModel = {
    Name: '',
    AverageSalary: 0,
    Percent: 0,
    ResumeCount: 0
  };
  private location: string = '';

  myControl = new FormControl('');
  options: string[] = [];
  filteredOptions: Observable<string[]>;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
    this.httpClient.get<ListModel[]>(environment.baseUrl + 'Common/locations').subscribe(listInput => {
      listInput.forEach( (value) => {
        this.options.push(value.Name);
      });
    });
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }
  
  ChangeLocation(name: string) {
    this.location = name;
    this.httpClient.get<LocationsModel>(environment.baseUrl + 'Locations/' + name).subscribe(locationsStat => {
      this.locationsStat = locationsStat;
    });
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
}