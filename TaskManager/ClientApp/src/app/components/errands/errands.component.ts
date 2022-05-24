import { Component, Input, OnInit } from '@angular/core';
import {errands} from "../mock-data/errands";

@Component({
  selector: 'app-errands',
  templateUrl: './errands.component.html',
  styleUrls: ['./errands.component.css'],
})
export class ErrandsComponent implements OnInit {
  errands = errands;
  constructor() {}

  ngOnInit(): void {}
}
