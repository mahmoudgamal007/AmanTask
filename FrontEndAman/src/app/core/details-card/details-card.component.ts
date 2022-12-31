import { Component, OnInit, Input, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CoreService } from '../core.service';

@Component({
  selector: 'app-details-card',
  templateUrl: './details-card.component.html',
  styleUrls: ['./details-card.component.scss'],
})
export class DetailsCardComponent implements OnInit {
  constructor(
    private service: CoreService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}
  empDetails: any;
  @Input() empId: any;
  ngOnInit(): void {
    this.getAdresses();
  }
  empAddresses: any;
  getAdresses() {
    this.service.getAddressByEmpId(this.data.id).subscribe(
      (res) => {
        this.empAddresses = res;
      },
      (error) => console.log(error)
    );
  }
}
