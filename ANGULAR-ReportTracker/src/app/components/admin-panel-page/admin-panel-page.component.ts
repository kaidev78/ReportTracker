import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-panel-page',
  templateUrl: './admin-panel-page.component.html',
  styleUrls: ['./admin-panel-page.component.css']
})
export class AdminPanelPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  option: Number = 1;

  changeMenu(opt:number){
    this.option = opt
  }
}
