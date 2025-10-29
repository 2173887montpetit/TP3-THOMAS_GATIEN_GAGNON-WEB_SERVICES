import { Component, OnInit } from '@angular/core';
import { Game } from './gameLogic/game';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { FlappyService } from '../services/FlappyService.service';

@Component({
  selector: 'app-play',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './play.component.html',
  styleUrl: './play.component.css'
})
export class PlayComponent implements OnInit{
  [x: string]: any;

  game : Game | null = null;
  scoreSent : boolean = false;

  constructor(private flappyService: FlappyService){}

  ngOnDestroy(): void {
    // Ceci est crotté mais ne le retirez pas sinon le jeu bug.
    location.reload();
  }

  ngOnInit() {
    this.game = new Game();
  }

  replay(){
    if(this.game == null) return;
    this.game.prepareGame();
    this.scoreSent = false;
  }

  sendScore(){

  if(this.scoreSent) return;

    this.scoreSent = true;

    this.flappyService.GetScore().then((response) => {
      console.log("Score envoyé avec succès :", response);
    }).catch((error) => {
      console.error("Erreur lors de l'envoi du score :", error);
    });

    
    // ██ Appeler une requête pour envoyer le score du joueur ██
    // Le score est dans sessionStorage.getItem("score")
    // Le temps est dans sessionStorage.getItem("time")
    // La date sera choisie par le serveur



  }


}
