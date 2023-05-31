import { Component, Input } from '@angular/core';
import Chart, { ChartDataset, ChartTypeRegistry } from 'chart.js/auto';
import { ListModel } from 'src/app/models/list.model';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent {
  public chart: any;

  private type: keyof ChartTypeRegistry = 'bar';

  private colors: string[] = [
    'rgba(255, 99, 132, 0.6)',
    'rgba(54, 162, 235, 0.6)',
    'rgba(255, 206, 86, 0.6)',
    'rgba(75, 192, 192, 0.6)',
    'rgba(153, 102, 255, 0.6)',
    'rgba(175, 255, 145, 0.6)',
    'rgba(119, 118, 118, 0.6)'
  ];

  @Input() data: ListModel[] = [];

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
    var length = 7;
    if (this.data.length < length) {
      length = this.data.length;
    }
    type Data = {
      labels: string[], datasets: {label: string, data: number[], backgroundColor: string}[]
    };
    if (type == 'bar') {
      var lineChartData: Data = {
        labels: [''],
        datasets: []
      };
  
      for (let i = 0; i < length; i++) {
        lineChartData.datasets.push({
          label: this.data[i].Name,
          data: [this.data[i].Count],
          backgroundColor: this.colors[i]
        });
      }
      this.chart = new Chart("MyChart", {
        type: type,
        data: lineChartData,
        options: {
          plugins: {
            legend: {
              display: true,
              position: "bottom"
            }
          }
        }
      });
    } else {
      var labels = new Array();
      var dataset = new Array();
      for (let i = 0; i < length; i++) {
        labels.push(this.data[i].Name);
        dataset.push(this.data[i].Count);
      }
      this.chart = new Chart("MyChart", {
        type: type,
        data: {
          labels: labels,
          datasets: [{
            label: "",
            data: dataset,
            backgroundColor: this.colors
          }]
        },
        options: {
          plugins: {
            legend: {
              display: true,
              position: "right"
            }
          }
        }
      });
    }
  }
}
