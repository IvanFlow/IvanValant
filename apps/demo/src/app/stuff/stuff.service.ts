import { Injectable } from '@angular/core';
import { concat, Observable, of } from 'rxjs';
import { ValantDemoApiClient } from '../api-client/api-client';
import { FileService } from '../file-upload/file-service';
import { concatMap, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class StuffService {
  constructor(private httpClient: ValantDemoApiClient.Client, private fileService: FileService) {}

  public getStuff(): Observable<ValantDemoApiClient.MazeDto[]> {
    return this.httpClient.mazeAll();
  }

  public saveStuff(): Observable<number> {
    
    return this.fileService.fetchFile().pipe(
      concatMap(stringFile =>
      {
        console.log('Maze got from file:', stringFile );
        let blob: Blob = new Blob([stringFile], { type: 'text/plain' });

        let uploadFileRequest: any = {
          data: blob,
          fileName: 'MazeValant.txt'
        };

        return this.httpClient.maze2(uploadFileRequest);
      }
      )
    );
  }
}
