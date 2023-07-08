import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, interval } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { switchMap } from 'rxjs/operators';
interface TrendingResponse {
  id: number;
  description: string;
  address: number;
  value: string;
  limit: string;
  unit: string;
}

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  private wsResult = new BehaviorSubject<TrendingResponse[] | null>(null);
  wsResultObs = this.wsResult.asObservable();

  constructor(private http: HttpClient) {
  }
  getResult() {
    console.log("get result");
    interval(3000) // Emit value every 1 second
      .pipe(
        switchMap(() => this.http.get<TrendingResponse[]>('/scada/trending'))
      )
      .subscribe(
        response => {
          console.log(response);
          this.handleResult(response);
        },
        error => {
          console.error('An error occurred during the request:', error);
        }
      );
  }

  handleResult(message: TrendingResponse[]) {
    this.wsResult.next(message)
  }
  disconnect() {
  }
}
