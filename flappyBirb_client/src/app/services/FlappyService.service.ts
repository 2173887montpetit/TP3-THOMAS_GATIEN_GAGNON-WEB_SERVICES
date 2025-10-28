import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ScoreDTO } from '../DTOs/ScoreDTO';
import { Score } from '../models/score';
import { lastValueFrom } from 'rxjs';

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


}
