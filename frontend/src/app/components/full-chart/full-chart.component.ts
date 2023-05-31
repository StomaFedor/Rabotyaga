import { Component, Input } from '@angular/core';
import Chart, { ChartDataset, ChartTypeRegistry } from 'chart.js/auto';
import { ListModel } from 'src/app/models/list.model';

@Component({
  selector: 'app-full-chart',
  templateUrl: './full-chart.component.html',
  styleUrls: ['./full-chart.component.css']
})
export class FullChartComponent {
  public chart: any;

  private type: keyof ChartTypeRegistry = 'bar';

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
    var labels = new Array();
    var dataset = new Array();
    this.data.forEach( (value) => {
      labels.push(value.Name);
      dataset.push(value.Count);
    });
    this.chart = new Chart("MyChart", {
      type: type,
      data: {
        labels: labels,
        datasets: [{
          label: "",
          data: dataset,
          backgroundColor: [
            'rgba(255, 99, 132, 0.6)',
            'rgba(54, 162, 235, 0.6)',
            'rgba(255, 206, 86, 0.6)',
            'rgba(75, 192, 192, 0.6)',
            'rgba(153, 102, 255, 0.6)'
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
