import { BehaviorSubject, Observable, interval, Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { switchMap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Alarm, AlarmPriority, AlarmType } from '../models/alarm';

export interface AlarmTriggerDTO {
  id: number;
  dateTime: Date;
  priority: AlarmPriority;
  type: AlarmType;
  limit: number;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class AlarmServiceService {
  private wsResult = new BehaviorSubject<AlarmTriggerDTO[] | null>(null);
  wsResultObs = this.wsResult.asObservable();
  private subscription: Subscription | undefined;

  constructor(private http: HttpClient) {
  }
  getResult() {
    console.log("get result");
    this.subscription = interval(1000) // Emit value every 1 second
      .pipe(
        switchMap(() => this.http.get<AlarmTriggerDTO[]>('api/alarms/triggers'))
      )
      .subscribe(
        response => {
          this.wsResult.next(response);
          console.log(response);
        },
        error => {
          console.error('An error occurred during the request:', error);
        }
      );
  }
  disconnect() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
