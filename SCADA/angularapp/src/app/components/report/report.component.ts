import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { Alarm, AlarmPriority, AlarmType } from 'src/app/models/alarm';
import { AlarmReportDTO, AnalogInputReportDTO, DigitalInputReportDTO, TagValueReportDTO } from 'src/app/models/report';
import { AnalogInput, DigitalInput, TagValue } from 'src/app/models/tags';
import { ReportService } from 'src/app/services/report.service';
import { Chart, ChartData, ChartOptions } from 'chart.js/auto';
import 'chartjs-adapter-date-fns';
import { enUS } from 'date-fns/locale';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent {

  chart: any;
  chartDisplay: boolean = false;
  AlarmPriority = AlarmPriority;

  alarms: AlarmReportDTO[] = [];
  tagValues: TagValueReportDTO[] = [];
  analogInputs: AnalogInputReportDTO[] = [];
  digitalInputs: DigitalInputReportDTO[] = [];
  reportType: string = '';

  constructor(private reportService: ReportService) {
   }


  initializeCharts() {
    this.chartDisplay = true;
    let titleTxt;
    let dates: Date[] = [];
    let values;
    if (this.reportType === "tagValues"){
      titleTxt = "Tag Values Chart";
      dates = this.tagValues.map(tagValue => new Date(tagValue.date));
      values = this.tagValues.map(tagValue => tagValue.value);

    } else if (this.reportType === "lastAnalogInputs"){
      titleTxt = "Last Values Analog Input Chart";
      dates = this.tagValues.map(analogInputs => new Date(analogInputs.date));
      values = this.tagValues.map(analogInputs => analogInputs.value);

    } else if (this.reportType === "lastDigitalInputs"){
      titleTxt = "Last Values Digital Input Chart";
      dates = this.digitalInputs.map(digitalInputs => new Date(digitalInputs.date));
      values = this.digitalInputs.map(digitalInputs => digitalInputs.value);

    } else if (this.reportType === "tagValuesById"){
      dates = this.tagValues.map(tagValue => new Date(tagValue.date));
      values = this.tagValues.map(tagValue => tagValue.value);
      titleTxt = "Tag Values By ID Chart";
    }

    const formattedDates = dates.map(date => date.toISOString());
    // dates.map((date, index) => ({ t: date.toISOString(), y: values[index] }))
    const chartData: ChartData<'line', any, string> = {
      labels: formattedDates, 
      datasets: [
        {
          label: 'Tag Value',
          data: values,
          backgroundColor: 'rgba(20, 0, 255, 0.2)',
          borderColor: 'rgba(20, 0, 255, 1)',
          borderWidth: 1,
        }
      ],
    };
    
    const chartOptions: ChartOptions<'line'> = {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        y: {
          beginAtZero: true,
        },
        x: {
          type: 'time',
          adapters: { 
            date: {
              locale: enUS, 
            },
          }, 
        }
      },
      plugins: {
        title: {
          display: true,
          text: titleTxt,
        },
      },
    };
    
    if(this.chart){
      this.chart.destroy();
    }
    
    this.chart = new Chart('canvas', {
      type: 'line',
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
        this.initializeCharts();
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
        this.initializeCharts();
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
        this.initializeCharts();
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
        this.initializeCharts();
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
