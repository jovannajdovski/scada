import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

interface LoginResponse {
  userType: string;
}
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private http: HttpClient, private router:Router) { }

  login() {
    // Send the username and password to the backend API
    const requestBody = {
      username: this.username,
      password: this.password
    };

    this.http.post<LoginResponse>('/scada/login', requestBody).subscribe(
      response => {
        const userType = response.userType;
        if (userType == 'user') {
          this.router.navigate(['/trending']);
        } else {
          this.router.navigate(['/admin']);
        }
      },
      error => {
        console.error('An error occurred during login:', error);
      }
    );

  }
}
