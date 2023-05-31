import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {FormControl} from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { FacultyModel } from 'src/app/models/faculty.model';
import { ListModel } from 'src/app/models/list.model';
import { Salary2ExperienceModel } from 'src/app/models/salary2experience.model';
import { environment } from '../../../enviroments/enviroment';

@Component({
  selector: 'app-faculties',
  templateUrl: './faculties.component.html',
  styleUrls: ['./faculties.component.css']
})
export class FacultiesComponent {
  private httpClient: HttpClient;
  public facultyStat: FacultyModel = {
    Name: '',
    AverageSalary: 0,
    AverageGraduationYear: 0,
    SpecializationMatchPercent: 0,
    AverageExperience: 0,
    StartYearAverage: 0,
    ResumeCount: 0
  };
  public listModel: ListModel[] = []; 
  public salary2experience: Salary2ExperienceModel[] = [];
  private faculty: string = '';
  public grath: string = 'specializations';

  myControl = new FormControl('');
  options: string[] = [];
  filteredOptions: Observable<string[]>;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
    this.httpClient.get<ListModel[]>(environment.baseUrl + 'Common/faculties').subscribe(listInput => {
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
      this.httpClient.get<Salary2ExperienceModel[]>(environment.baseUrl + 'Faculties/' + this.faculty + '/' + name).subscribe(listInput => {
        this.salary2experience = listInput;
      });
    } else {
      this.httpClient.get<ListModel[]>(environment.baseUrl + 'Faculties/' + this.faculty + '/' + name).subscribe(listInput => {
        this.listModel = listInput;
      });
    }
  }
  
  ChangeFaculty(name: string) {
    this.faculty = name;
    this.httpClient.get<FacultyModel>(environment.baseUrl + 'Faculties/' + name).subscribe(facultyStat => {
      this.facultyStat = facultyStat;
    });
    this.ChangeChart(this.grath);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
}
