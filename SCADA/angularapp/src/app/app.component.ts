import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public forecasts?: WeatherForecast[];

  constructor(http: HttpClient) {

    //http.get<WeatherForecast[]>('/WeatherForecast').subscribe(result => {
    //  this.forecasts = result;
    //  //console.log(result)
    //}, error => console.error(error));
  }

  title = 'angularapp';
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
