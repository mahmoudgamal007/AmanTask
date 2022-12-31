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
import { DepartmentDialogComponent } from '../department-dialog/department-dialog.component';

@Component({
  selector: 'app-departments',

  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.scss'],
})
export class DepartmentsComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'specialization', 'Controls'];
  dataSource!: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @Input() deptDetails: any;
  constructor(
    private dialog: MatDialog,
    private service: CoreService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.getAllDepartments();
  }
  openDialog() {
    this.dialog.open(DepartmentDialogComponent, {
      width: '30%',
    });
  }

  getAllDepartments() {
    this.service.getDepartments().subscribe(
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
  editDepartment(row: any) {
    this.dialog.open(DepartmentDialogComponent, {
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
  deleteDepartment(id: number) {
    this.service.deleteDepartment(id).subscribe(
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
