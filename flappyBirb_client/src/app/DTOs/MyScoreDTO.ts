export class MyScoreDTO {
 constructor(
  public scoreValue: number,
  public timeInSeconds: number,
  public date: string,
  public isPublic: boolean){}
}
