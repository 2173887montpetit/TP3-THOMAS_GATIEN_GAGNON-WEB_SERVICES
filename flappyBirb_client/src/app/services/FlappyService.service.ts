import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ScoreDTO } from '../DTOs/ScoreDTO';
import { Score } from '../models/score';
import { Observable, lastValueFrom } from 'rxjs';
import { PublicScoreDTO } from '../DTOs/PublicScoreDTO';

const domain : string = "https://localhost:7278/";

@Injectable({
  providedIn: 'root'
})

export class FlappyService {

constructor( private http: HttpClient) { }

GetScore(){
  let score = sessionStorage.getItem("score");
  let time = sessionStorage.getItem("time");
  let scoreDTO = new ScoreDTO(Number(score) || 0, Number(time) || 0);
  let x = lastValueFrom(this.http.post<any>(domain + "api/Scores/PostScore", scoreDTO));
  console.log(x);
  return x;
}

// Récupère les scores publics
async getPublicScores(): Promise<any> {
  let x = await lastValueFrom(this.http.get<any>(domain + 'api/Scores/GetPublicScores'));
  console.log(x);
    return x ?? [];

}

// Récupère les scores de l'utilisateur connecté (nécessite Authorization)

}