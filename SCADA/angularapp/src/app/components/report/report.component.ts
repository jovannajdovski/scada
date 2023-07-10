import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { Alarm, AlarmPriority, AlarmType } from 'src/app/models/alarm';
import { AlarmReportDTO, AnalogInputReportDTO, DigitalInputReportDTO } from 'src/app/models/report';
import { AnalogInput, DigitalInput, TagValue } from 'src/app/models/tags';
import { ReportService } from 'src/app/services/report.service';
import { Chart, ChartData, ChartOptions } from 'chart.js/auto';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent {

  chart: any;

  AlarmPriority = AlarmPriority;

  alarms: AlarmReportDTO[] = [];
  tagValues: TagValue[] = [];
  analogInputs: AnalogInputReportDTO[] = [];
  digitalInputs: DigitalInputReportDTO[] = [];
  reportType: string = '';

  constructor(private reportService: ReportService) {
   }


  initializeCharts() {
    const chartData: ChartData<'bar', any, string> = {
      labels: this.alarms.map(alarm => alarm.id.toString()), // Use alarm ID as labels
      datasets: [
        {
          label: 'Type',
          data: this.alarms.map(alarm => alarm.type), // Use alarm type as dataset values
          backgroundColor: 'rgba(75, 192, 192, 0.2)',
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 1,
         },
        {
          label: 'Priority',
          data: this.alarms.map(alarm => alarm.priority), // Use alarm priority as dataset values
          backgroundColor: 'rgba(192, 75, 75, 0.2)',
          borderColor: 'rgba(192, 75, 75, 1)',
          borderWidth: 1,
        },
        {
          label: 'Limit',
          data: this.alarms.map(alarm => alarm.limit), // Use alarm limit as dataset values
          backgroundColor: 'rgba(192, 192, 75, 0.2)',
          borderColor: 'rgba(192, 192, 75, 1)',
          borderWidth: 1,
        },
        // {
        //   label: 'Date',
        //   data: this.alarms.map(alarm => alarm.date), // Use alarm date as dataset values
        //   backgroundColor: 'rgba(75, 75, 192, 0.2)',
        //   borderColor: 'rgba(75, 75, 192, 1)',
        //   borderWidth: 1,
        // },
      ],
    };
    
    const chartOptions: ChartOptions<'bar'> = {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        y: {
          beginAtZero: true,
        },
      },
      plugins: {
        title: {
          display: true,
          text: 'Alarms Chart',
        },
      },
    };
    
    if(this.chart){
      this.chart.destroy();
    }
    
    this.chart = new Chart('canvas', {
      type: 'bar',
      data: chartData,
      options: chartOptions
    });

  }

  showPopup: boolean = false;
  reportPopupType: string = '';

  closePopup() {
    this.showPopup = false;
  }

  openDialog(tagType: string) {
    this.reportPopupType = tagType;
    this.showPopup = true;
  }

  getAllAlarmsForm: any = {
    startTime: new Date(),
    endTime: new Date(),
    isAscending: false
  };
  getAlarmsByPriorityForm: any = {
    priority: 0,
    isAscending: false
  };
  getAllTagValuesForm: any = {
    startTime: new Date(),
    endTime: new Date(),
    isAscending: false
  };
  getLastAnalogInputsForm: any = {
    isAscending: false
  };
  getLastDigitalInputsForm: any = {
    isAscending: false
  };
  getTagValuesByIdForm: any = {
    id: 0,
    isAscending: false
  };
  


  getAllAlarms() {
    let startTime = this.getAllAlarmsForm.startTime;
    let endTime = this.getAllAlarmsForm.endTime;
    let isAscending =  this.getAllAlarmsForm.isAscending;

    this.reportService.getAllAlarms(startTime, endTime, isAscending).subscribe(
      (data) => {  
        this.alarms = [];  
        this.alarms = data;
        console.log(this.alarms);
        this.reportType = 'alarms';
        this.showPopup = false;
        this.initializeCharts();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getAlarmsByPriority() {

    let priority = this.getAlarmsByPriorityForm.priority;
    let isAscending =  this.getAlarmsByPriorityForm.isAscending;
    this.reportService.getAlarmsByPriority(priority, isAscending).subscribe(
      (data) => {
        this.alarms = [];
        this.alarms = data;
        console.log(this.alarms);
        this.reportType = 'alarmsByPriority';
        this.showPopup = false;
        this.initializeCharts();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getAllTagValues() {

    let startTime = this.getAllTagValuesForm.startTime;
    let endTime = this.getAllTagValuesForm.endTime;
    let isAscending =  this.getAllTagValuesForm.isAscending;

    this.reportService.getAllTagValues(startTime, endTime, isAscending).subscribe(
      (data) => {
        this.tagValues = [];
        this.tagValues = data;
        console.log(this.tagValues);
        this.reportType = 'tagValues';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getLastAnalogInputs() {

    let isAscending =  this.getLastAnalogInputsForm.isAscending;

    this.reportService.getLastAnalogInputs(isAscending).subscribe(
      (data) => {
        this.analogInputs = [];
        this.analogInputs = data;
        console.log(this.analogInputs);
        this.reportType = 'lastAnalogInputs';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getLastDigitalInputs() {

    let isAscending =  this.getLastDigitalInputsForm.isAscending;

    this.reportService.getLastDigitalInputs(isAscending).subscribe(
      (data) => {
        this.digitalInputs = [];
        this.digitalInputs = data;
        console.log(this.digitalInputs);
        this.reportType = 'lastDigitalInputs';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getTagValuesById() {

    let id = this.getTagValuesByIdForm.id;
    let isAscending =  this.getTagValuesByIdForm.isAscending;

    this.reportService.getTagValuesById(id, isAscending).subscribe(
      (data) => {
        this.tagValues = [];
        this.tagValues = data;
        console.log(this.tagValues);
        this.reportType = 'tagValuesById';
        this.showPopup = false;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
