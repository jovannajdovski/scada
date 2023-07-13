import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AlarmServiceService, AlarmTriggerDTO } from '../../services/alarm-service.service';

@Component({
  selector: 'app-alarm-triggers',
  templateUrl: './alarm-triggers.component.html',
  styleUrls: ['./alarm-triggers.component.css']
})
export class AlarmTriggersComponent {
  triggers: AlarmTriggerDTO[] = [];
  constructor(private webSocketService: AlarmServiceService, private router: Router) { }

  ngOnInit(): void {
    this.webSocketService.getResult();
    this.webSocketService.wsResultObs.subscribe((value) => {
      if (value != null)
        this.triggers = value;
      this.triggers.forEach(function (v) {
        console.log(v.priority);
      })
    })
  }
  logout(): void {
    this.webSocketService.disconnect();
    this.router.navigate(['/login']);
  }
}
