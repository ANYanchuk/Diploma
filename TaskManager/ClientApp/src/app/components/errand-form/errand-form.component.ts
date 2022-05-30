import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-errand-form',
  templateUrl: './errand-form.component.html',
  styleUrls: ['./errand-form.component.css']
})
export class ErrandFormComponent implements OnInit {
  @Input() errandId: string;
  mode: 'create' | 'update';

  constructor() { }

  ngOnInit(): void {
    this.mode = this.errandId ? 'update' : 'create';
  }
}
