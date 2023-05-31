import { Component, Input } from '@angular/core';
import Chart, { ChartDataset, ChartTypeRegistry } from 'chart.js/auto';
import { Salary2ExperienceModel } from 'src/app/models/salary2experience.model';

@Component({
  selector: 'app-scatter-chart',
  templateUrl: './scatter-chart.component.html',
  styleUrls: ['./scatter-chart.component.css']
})
export class ScatterChartComponent {
  public chart: any;

  private type: keyof ChartTypeRegistry = 'scatter';

  @Input() data: Salary2ExperienceModel[] = [];

  ngOnChanges(): void {
    if (this.chart != null) {
      this.chart.destroy();
    }
    this.CreateChart(this.type);
  }

  ChangeType(newType:keyof ChartTypeRegistry) {
    this.type = newType;
    if (this.chart != null) {
      this.chart.destroy();
    }
    this.CreateChart(newType);
  }

  CreateChart(type:keyof ChartTypeRegistry) {
    var xyValues = new Array();
    this.data.forEach( (value) => {
      var x = value.Experience;
      value.Salary.forEach( (y) => {
        var tmp = {x, y};
        xyValues.push(tmp);
      });
    });
    this.chart = new Chart("MyChart", {
      type: this.type,
      data: {
        datasets: [{
          data: xyValues,
          backgroundColor: [
            'blue'
          ]
        }]
      },
      options: {
        plugins: {
          legend: {
            display: false,
            position: "right"
          }
        }
      }
    });
  }
}
