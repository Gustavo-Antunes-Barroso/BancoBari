import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class QueueService {

  constructor(public http: HttpClient) { }

  SelecionarQuantidadeMensagens(): Observable<any>{
    return this.http.get("Queue") as Observable<any>;
  }
}
