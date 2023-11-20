import { Component, Input } from '@angular/core';
import Limoncello from '../models/limoncello';
import { registerLocaleData } from '@angular/common';
import localeNl from '@angular/common/locales/nl';
registerLocaleData(localeNl, 'nl');

@Component({
  selector: 'app-limoncello-info',
  templateUrl: './limoncello-info.component.html',
  styleUrls: ['./limoncello-info.component.scss']
})
export class LimoncelloInfoComponent {

  @Input() limoncello!: Limoncello;

}
