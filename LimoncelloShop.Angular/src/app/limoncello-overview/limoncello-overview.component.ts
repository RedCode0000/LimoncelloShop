import { Component, OnInit } from '@angular/core';
import Limoncello from '../models/limoncello';
import { LimoncelloService } from '../services/limoncello.service';

@Component({
  selector: 'app-limoncello-overview',
  templateUrl: './limoncello-overview.component.html',
  styleUrls: ['./limoncello-overview.component.scss']
})
export class LimoncelloOverviewComponent implements OnInit {

  allLimoncello: Limoncello[] = [];

  constructor(private limoncelloService: LimoncelloService) { }

  ngOnInit(): void {
    this.limoncelloService.getAllLimoncello().subscribe(x => {
      this.allLimoncello = x;
      //console.log(this.allLimoncello);
    })
  }

}
