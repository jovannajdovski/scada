import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, interval, Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { switchMap } from 'rxjs/operators';
interface TrendingResponse {
  id: number;
  description: string;
  address: number;
  value: string;
  limit: string;
  unit: string;
  scanTime: number;
  alarmPriority: string;
}

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  private wsResult = new BehaviorSubject<TrendingResponse[] | null>(null);
  wsResultObs = this.wsResult.asObservable();
  private subscription: Subscription | undefined;

  constructor(private http: HttpClient) {
  }
  getResult() {
    console.log("get result");
    this.subscription = interval(1000) // Emit value every 1 second
      .pipe(
        switchMap(() => this.http.get<TrendingResponse[]>('/scada/trending'))
      )
      .subscribe(
        response => {
          this.handleResult(response);
        },
        error => {
          console.error('An error occurred during the request:', error);
        }
      );
  }

  handleResult(message: TrendingResponse[]) {
    this.wsResult.next(message)
    const minScanTime = Math.min(...message.map(item => item.scanTime));
    return minScanTime * 1000;
  }
  disconnect() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
