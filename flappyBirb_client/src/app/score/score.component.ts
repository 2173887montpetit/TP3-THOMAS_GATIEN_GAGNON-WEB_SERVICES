import { Component } from '@angular/core';
import { Score } from '../models/score';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { Round00Pipe } from '../pipes/round-00.pipe';
import { firstValueFrom } from 'rxjs';
import { FlappyService } from '../services/FlappyService.service';
import { PublicScoreDTO } from '../DTOs/PublicScoreDTO';
import { MyScoreDTO } from '../DTOs/MyScoreDTO';

@Component({
  selector: 'app-score',
  standalone: true,
  imports: [MaterialModule, CommonModule, Round00Pipe],
  templateUrl: './score.component.html',
  styleUrl: './score.component.css'
})
export class ScoreComponent {

  myScores : MyScoreDTO[] = [];
  publicScores : PublicScoreDTO[] = [];
  
  constructor(private flappyService: FlappyService) {}

  async ngOnInit() {
    try {
      const response = await this.flappyService.getPublicScores();
      const myResponse = await this.flappyService.getUserScores();
      console.log('Public scores response:', response);
      console.log('My scores response:', myResponse);

      // Support both direct array and wrapper { value: [...] }
      const publicArr = Array.isArray(response) ? response : (Array.isArray((response as any)?.value) ? (response as any).value : []);
      this.publicScores = publicArr;

      const myArr = Array.isArray(myResponse) ? myResponse : (Array.isArray((myResponse as any)?.value) ? (myResponse as any).value : []);
  
      this.myScores = myArr.map((item: any) => new MyScoreDTO(
        item.scoreValue ?? item.value ?? 0,
        item.timeInSeconds ?? item.tempsEnSecondes ?? item.time ?? 0,
        item.date ?? '',
        item.isPublic ?? false
      ));

      console.log('Public scores array (final):', this.publicScores);
      console.log('My scores array (mapped):', this.myScores);
    } catch (error) {
      console.error('Error loading public scores:', error);
      this.publicScores = [];
    }

  }

  async changeScoreVisibility(score: MyScoreDTO){
    
    
  }



}
