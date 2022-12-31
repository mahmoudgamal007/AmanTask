import { Component, Inject, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Employee } from 'src/app/models/employee';
import { CoreService } from '../core.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Department } from 'src/app/models/department';
import { Router } from '@angular/router';

@Component({
  selector: 'app-department-dialog',
  templateUrl: './department-dialog.component.html',
  styleUrls: ['./department-dialog.component.scss'],
})
export class DepartmentDialogComponent implements OnInit {
  departmentForm!: FormGroup;
  newDepartment: Department = new Department();
  departments: Department[] = [];
  actionBtn: string = 'Save'; // deafult is save button but if form isnt empty it becomes update insted
  actionTitle: string = 'Add Department';

  constructor(
    private formbuilder: FormBuilder,
    private service: CoreService,
    private router: Router,
    private dialogRef: MatDialogRef<DepartmentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public editData: any
  ) {}

  ngOnInit(): void {
    this.getDepartments();
    //using reactive forms to post new employee
    this.departmentForm = this.formbuilder.group({
      departmentName: ['', Validators.required],
      departmentSpecialization: ['', [Validators.required]],
    });
    if (this.editData) {
      this.actionBtn = 'Update';
      this.actionTitle = 'Update Department';
      this.departmentForm.controls['departmentName'].setValue(
        this.editData.name
      );
      this.departmentForm.controls['departmentSpecialization'].setValue(
        this.editData.specialization
      );
    }
  }
  //maaping the values from the form to the employee object
  mapDeoartment(newDepartment: Department, values: any) {
    newDepartment.name = values.departmentName;
    newDepartment.specialization = values.departmentSpecialization;
    return newDepartment;
  }
  // posting the employee object using the servicec
  //in case form opens on empty we add otherwise we subscribe on the put
  addDepartment() {
    this.newDepartment = this.mapDeoartment(
      this.newDepartment,
      this.departmentForm.value
    );
    if (!this.editData) {
      if (this.departmentForm.valid) {
        this.service.addDepartment(this.newDepartment).subscribe(
          (data) => {
            alert('Department Added.');

            this.departmentForm.reset();
            this.dialogRef.close();
            location.reload();
          },
          (error) => {
            console.log(error);
            alert('Error Adding New Department!');
          }
        );
      } else {
        alert('Invalid Inputs please recheck');
      }
    } else {
      this.newDepartment.id = this.editData.id;
      if (this.departmentForm.valid) {
        this.service
          .putDepartment(this.editData.id, this.newDepartment)
          .subscribe(
            (data) => {
              alert('Dpepartment Updated.');
              this.router.navigate([this.router.url]);
              this.departmentForm.reset();
              this.dialogRef.close();
              location.reload();
            },
            (error) => {
              console.log(error);
              alert('Error Adding New Department!');
            }
          );
      } else {
        alert('Invalid Inputs please recheck');
      }
    }
  }
  //get all departments
  getDepartments() {
    this.service.getDepartments().subscribe(
      (res: any) => {
        console.log(res);
        this.departments = res;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
