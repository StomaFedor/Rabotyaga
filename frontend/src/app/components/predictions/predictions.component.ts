import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { PredictionModel } from 'src/app/models/prediction.model';
import { PredictModel } from 'src/app/models/predict.model';
import { environment } from '../../../enviroments/enviroment';

@Component({
  selector: 'app-predictions',
  templateUrl: './predictions.component.html',
  styleUrls: ['./predictions.component.css']
})
export class PredictionsComponent {
  private httpClient: HttpClient;
  public predict: PredictModel = {
    Gender: "",
    Age: 0,
    Vacation: "",
    ExpectedSalary: 0,
    YearGraduation: 0,
    Experience: 0
  }
  public prediction: PredictionModel = {
    prediction: []
  }

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  GetPredict(gender: string, age: string, vacation: string, salary: string, year: string, experience: string) {
    this.predict.Age = Number(age);
    this.predict.ExpectedSalary = Number(salary);
    this.predict.Experience = Number(experience);
    this.predict.Gender = gender;
    this.predict.Vacation = vacation;
    this.predict.YearGraduation = Number(year);
    var tmp: {graduates: PredictModel[]} = {graduates: [this.predict]};
    this.httpClient.post<PredictionModel>(environment.aiUrl + 'predict', tmp).subscribe(data => {
      this.prediction = data;
    });
  }
}