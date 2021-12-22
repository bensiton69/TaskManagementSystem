import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
baseUrl = "https://localhost:5001/api/Statistics";
  constructor(private httpClient:HttpClient) { }

  getStats(){
    return this.httpClient.get(this.baseUrl)
    .pipe();
  }
}
