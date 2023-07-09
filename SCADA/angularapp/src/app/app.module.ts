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

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'trending', component: TrendingComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'tags', component: TagManagementComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TrendingComponent,
    AdminComponent,
    RtuPopupComponent,
    TagManagementComponent
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
