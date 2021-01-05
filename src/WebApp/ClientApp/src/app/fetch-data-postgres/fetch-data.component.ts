import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data-postgres',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataPostgresComponent {
  public result: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    console.log('weather: get=', baseUrl + 'api/v1/weatherforecasts/GetDogs');
    http.get<any[]>(baseUrl + 'api/v1/weatherforecasts/GetDogs').subscribe(result => { // TODO: use generated API client
      console.log('weather: data=', result);
      this.result = result;
    }, error => console.error(error));
  }
}

