import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class FileService {
    constructor(private http: HttpClient){}

    fetchFile(): Observable<string> {
        return this.http.get('assets/MazeValant.txt', {responseType: 'text'});
    }
}