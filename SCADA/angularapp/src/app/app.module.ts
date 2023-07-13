import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { TrendingComponent } from './components/trending/trending.component';
import { AdminComponent } from './components/admin/admin.component';
import { RtuPopupComponent } from './components/rtu-popup/rtu-popup.component';
import { TagManagementComponent } from './components/tag-management/tag-management.component';
import { ReportComponent } from './components/report/report.component';
import { AlarmTriggersComponent } from './components/alarm-triggers/alarm-triggers.component';
import { AlarmsComponent } from './components/alarms/alarms.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'tags', component: TagManagementComponent },
  { path: 'reports', component: ReportComponent },
  { path: 'trending', component: TrendingComponent },
  { path: 'alarms', component: AlarmsComponent },
  { path: 'alarm-triggers', component: AlarmTriggersComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TagManagementComponent,
    ReportComponent,
    TrendingComponent,
    AdminComponent,
    RtuPopupComponent,
    AlarmTriggersComponent,
    AlarmsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule,
    CommonModule
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
