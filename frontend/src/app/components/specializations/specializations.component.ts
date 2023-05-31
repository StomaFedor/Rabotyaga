import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { ListModel } from 'src/app/models/list.model';
import { Salary2ExperienceModel } from 'src/app/models/salary2experience.model';
import { SpecializationsModel } from 'src/app/models/specializations.model';
import { environment } from '../../../enviroments/enviroment';

@Component({
  selector: 'app-specializations',
  templateUrl: './specializations.component.html',
  styleUrls: ['./specializations.component.css']
})
export class SpecializationsComponent {
  private httpClient: HttpClient;
  public specializationStat: SpecializationsModel = {
    Name: '',
    AverageSalary: 0,
    AverageExperience: 0,
    StartYearAverage: 0,
    ResumeCount: 0
  };
  public listModel: ListModel[] = []; 
  public salary2experience: Salary2ExperienceModel[] = [];
  private specialization: string = '';
  public grath: string = 'faculties';

  myControl = new FormControl('');
  options: string[] = [];
  filteredOptions: Observable<string[]>;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
    this.httpClient.get<ListModel[]>(environment.baseUrl + 'Common/specializations').subscribe(listInput => {
      listInput.forEach( (value) => {
        this.options.push(value.Name);
      });
    });
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }

  ChangeChart(name: string) {
    this.grath = name;
    if (name == 'salary2experience') {
      this.httpClient.get<Salary2ExperienceModel[]>(environment.baseUrl + 'Specializations/' + this.specialization + '/' + name).subscribe(listInput => {
        this.salary2experience = listInput;
      });
    } else {
      this.httpClient.get<ListModel[]>(environment.baseUrl + 'Specializations/' + this.specialization + '/' + name).subscribe(listInput => {
        this.listModel = listInput;
      });
    }
  }
  
  ChangeSpecialization(name: string) {
    this.specialization = name;
    this.httpClient.get<SpecializationsModel>(environment.baseUrl + 'Specializations/' + name).subscribe(specializationStat => {
      this.specializationStat = specializationStat;
    });
    this.ChangeChart(this.grath);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
}
