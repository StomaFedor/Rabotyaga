import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http'

import { CommonModel } from 'src/app/models/common.model';
import { ListModel } from 'src/app/models/list.model';
import { BarChartComponent } from '../bar-chart/bar-chart.component';
import { environment } from '../../../enviroments/enviroment';

@Component({
  selector: 'app-common',
  templateUrl: './common.component.html',
  styleUrls: ['./common.component.css']
})
export class CommonComponent {
  private httpClient: HttpClient;
  public listModel: ListModel[] = [];

  @ViewChild(BarChartComponent, {static: false})
  public chartComponent: BarChartComponent | undefined;

  public commonStat: CommonModel = {
    AverageSalary: 0,
    StartYearAverage: 0,
    GraduationYearAverage: 0,
    ResumeCount: 0
  };

  ChangeChart(name: string) {
    this.httpClient.get<ListModel[]>(environment.baseUrl + 'Common/' + name).subscribe(listInput => {
      this.listModel = listInput;
    });

  }

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  ngOnInit(): void {
    this.httpClient.get<CommonModel>(environment.baseUrl + 'Common/').subscribe(commonStat => {
      this.commonStat = commonStat;
    });
    this.httpClient.get<ListModel[]>(environment.baseUrl + 'Common/faculties').subscribe(listInput => {
      this.listModel = listInput;
    });
  }
}
