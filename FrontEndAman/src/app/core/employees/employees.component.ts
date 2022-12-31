import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Employee } from 'src/app/models/employee';
import { Department } from 'src/app/models/department';
import { CoreService } from '../core.service';
import { DialogComponent } from '../dialog/dialog.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { DetailsCardComponent } from '../details-card/details-card.component';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-employees',

  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss'],
})
export class EmployeesComponent implements OnInit {
  parentMessage = 'Hello from ParentComponent';
  displayedColumns: string[] = [
    'id',
    'name',
    'age',
    'salary',
    'departmentName',
    'Controls',
  ];
  dataSource!: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @Input() empDetails: any;
  constructor(
    private dialog: MatDialog,
    private service: CoreService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getAllEmployees();
  }
  openDialog() {
    this.dialog.open(DialogComponent, {
      width: '30%',
    });
  }
  openPopup(id: number) {
    this.dialog.open(DetailsCardComponent, {
      data: { id },
    });
  }
  openDetails() {
    this.dialog.open(DetailsCardComponent, {
      width: '30%',
    });
  }
  getEmployeeDetails(id: number) {
    this.service.getEmployeeById(id).subscribe(
      (res) => {
        console.log(res);
      },
      (error) => console.log(error)
    );
  }

  getAllEmployees() {
    this.service.getEmployees().subscribe(
      (data: any) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      (error) => {
        console.log(error);
      }
    );
  }
  editEmployee(row: any) {
    this.dialog.open(DialogComponent, {
      width: '30%',
      data: row,
    });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  deleteEmployee(id: number) {
    this.service.deleteEmployee(id).subscribe(
      (res) => {
        alert('Deleted!');
        location.reload();
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
