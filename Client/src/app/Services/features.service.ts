import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FeaturesService {
  baseUrl = "https://localhost:5001/api/Features";
  constructor(private httpClient:HttpClient) { }

  getStatuses(){
    return this.httpClient.get(this.baseUrl + "/Statuses")
    .pipe();
  }

  getUrgentLevel(){
    return this.httpClient.get(this.baseUrl + "/UrgentLevel")
    .pipe();
  }
}
