import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateTimeService {

  constructor() { }

  formatDate(model: any) {
    return `${model.year}-${model.month}-${model.day}`;
  }
}
