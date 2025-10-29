import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialModule } from '../material.module';
import { FormsModule } from '@angular/forms';
import { LoginDTO } from '../DTOs/LoginDTO';
import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RegisterDTO } from '../DTOs/RegisterDTO';

const domain : string = "https://localhost:7278/";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MaterialModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  hide = true;
  registerUsername : string = "";
  registerEmail : string = "";
  registerPassword : string = "";
  registerPasswordConfirm : string = "";

  loginUsername : string = "";
  loginPassword : string = "";

  constructor(public route : Router, private http: HttpClient) { }

  ngOnInit() {
  }

  async login(user : string, pass : string){

    let logUser = new LoginDTO(user, pass);

    logUser.username = user;
    logUser.password = pass;
  
    // Appel au service d'authentification ici
    const x = await lastValueFrom(this.http.post<any>(domain + "api/Users/login", logUser));
    // Redirection si la connexion a réussi :
  
    localStorage.setItem("token", x.token);
    console.log(x.token);
    this.route.navigate(["/play"]);
    
  }

  async register(username : string, email : string, password : string, passwordConfirm : string){

    let regUser = new RegisterDTO(username, email, password, passwordConfirm);

    this.registerUsername = username;
    this.registerEmail = email;
    this.registerPassword = password;
    this.registerPasswordConfirm = passwordConfirm;

    const x = await lastValueFrom(this.http.post<any>(domain + "api/Users/Register", regUser));

    return x;
  }
}
