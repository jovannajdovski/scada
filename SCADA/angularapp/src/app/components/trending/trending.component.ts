import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WebSocketService } from '../../services/web-socket.service';

interface TrendingResponse {
  id: number;
  description: string;
  address: number;
  value: string;
  limit: string;
  unit: string;
  scanTime: number;
}

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css']
})
export class TrendingComponent implements OnInit {
  trendingData: TrendingResponse[] = [];

  constructor(private webSocketService: WebSocketService, private router: Router) { }

  ngOnInit(): void {
    this.webSocketService.getResult();
    this.webSocketService.wsResultObs.subscribe((value) => {
      if(value!=null)
        this.trendingData = value;
    })
  }

  logout(): void {
    this.webSocketService.disconnect();
    this.router.navigate(['/login']);
  }

  toAlarms() {
    this.webSocketService.disconnect();
    this.router.navigate(['/alarm-triggers']);
  }
}

