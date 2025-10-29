import { Component } from '@angular/core';
import { Score } from '../models/score';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { Round00Pipe } from '../pipes/round-00.pipe';
import { firstValueFrom } from 'rxjs';
import { FlappyService } from '../services/FlappyService.service';
import { PublicScoreDTO } from '../DTOs/PublicScoreDTO';

@Component({
  selector: 'app-score',
  standalone: true,
  imports: [MaterialModule, CommonModule, Round00Pipe],
  templateUrl: './score.component.html',
  styleUrl: './score.component.css'
})
export class ScoreComponent {

  myScores : Score[] = [];
  publicScores : PublicScoreDTO[] = [];
  
  constructor(private flappyService: FlappyService) {}

  async ngOnInit() {
    try {
      const response = await this.flappyService.getPublicScores();
      console.log('Full API response:', response);

      // ✅ Extract the array inside "value" (your API returns { result, value })
      this.publicScores = response?.value ?? [];

      console.log('Public scores array:', this.publicScores);
    } catch (error) {
      console.error('Error loading public scores:', error);
      this.publicScores = [];
    }

  }

  async changeScoreVisibility(score : Score){

    
  }



}
