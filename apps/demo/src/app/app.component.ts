import { Component, OnInit } from '@angular/core';
import { LoggingService } from './logging/logging.service';
import { StuffService } from './stuff/stuff.service';
import { ValantDemoApiClient } from './api-client/api-client';

@Component({
  selector: 'valant-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent implements OnInit {
  public title = 'Valant demo';
  public data: ValantDemoApiClient.MazeDto[];
  

  constructor(private logger: LoggingService, private stuffService: StuffService) {}

  ngOnInit() {
    this.logger.log('Welcome to the AppComponent');
    this.stuffService.saveStuff().subscribe(
      response => {
        let mazeNumberJustSaved = response;
        console.log('Maze id number just saved', mazeNumberJustSaved)
        this.getStuff()
      }
    );
    
  }

  private getStuff(): void {
    this.stuffService.getStuff().subscribe({
      next: (response: ValantDemoApiClient.MazeDto[]) => {
        this.data = response;
        console.log(this.data);
      },
      error: (error) => {
        this.logger.error('Error getting stuff: ', error);
      },
    });
  }
}
