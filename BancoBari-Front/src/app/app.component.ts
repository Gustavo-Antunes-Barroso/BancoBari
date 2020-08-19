import { Component } from '@angular/core';
import { OnInit } from "@angular/core";
import { QueueService } from "src/app/services/queue.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BancoBari-Front';
  retorno;
  ngOnInit(): void {
  this.selecionarQuantidadeMensagens();    
  }
  constructor(public service: QueueService){

  }

  selecionarQuantidadeMensagens(){
    this.service.SelecionarQuantidadeMensagens().subscribe(
      response =>{
        this.retorno = response;
        setInterval(()=> {
          this.selecionarQuantidadeMensagens(); 
        }, 5000);
      },
      error =>{

      }
    )
  }

}
