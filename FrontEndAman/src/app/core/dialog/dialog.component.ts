import { Component, Inject, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Employee } from 'src/app/models/employee';
import { CoreService } from '../core.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Department } from 'src/app/models/department';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements OnInit {
  employeeForm!: FormGroup;
  newEmployee: Employee = new Employee();
  departments: Department[] = [];
  actionBtn: string = 'Save'; // deafult is save button but if form isnt empty it becomes update insted
  actionTitle: string = 'Add Employee';

  constructor(
    private formbuilder: FormBuilder,
    private service: CoreService,
    private dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public editData: any
  ) {}

  ngOnInit(): void {
    this.getDepartments();
    //using reactive forms to post new employee
    this.employeeForm = this.formbuilder.group({
      employeeName: ['', Validators.required],
      employeeAge: [
        '',
        [
          Validators.required,
          Validators.pattern('^[0-9]*$'),
          Validators.maxLength(2),
        ],
      ],
      employeeSalary: [
        '',
        [Validators.required, Validators.pattern('^[0-9]*$')],
      ],
      employeeDepartment: ['', Validators.required],
    });
    if (this.editData) {
      this.actionBtn = 'Update';
      this.actionTitle = 'Update Employee';
      this.employeeForm.controls['employeeName'].setValue(this.editData.name);
      this.employeeForm.controls['employeeAge'].setValue(this.editData.age);
      this.employeeForm.controls['employeeSalary'].setValue(
        this.editData.salary
      );
    }
  }
  //maaping the values from the form to the employee object
  mapEmployee(newEmployee: Employee, values: any) {
    newEmployee.name = values.employeeName;
    newEmployee.age = values.employeeAge;
    newEmployee.salary = values.employeeSalary;
    newEmployee.departmentId = values.employeeDepartment;

    return newEmployee;
  }
  // posting the employee object using the servicec
  //in case form opens on empty we add otherwise we subscribe on the put
  addEmployee() {
    this.newEmployee = this.mapEmployee(
      this.newEmployee,
      this.employeeForm.value
    );
    if (!this.editData) {
      if (this.employeeForm.valid) {
        this.service.addEmployee(this.newEmployee).subscribe(
          (data) => {
            alert('Employee Added.');
            this.employeeForm.reset();
            this.dialogRef.close();
            location.reload();
          },
          (error) => {
            console.log(error);
            alert('Error Adding New Employee!');
          }
        );
      } else {
        alert('Invalid Inputs please recheck');
      }
    } else {
      this.newEmployee.id = this.editData.id;
      this.newEmployee.departmentName = this.editData.departmentName;
      if (this.employeeForm.valid) {
        this.service.putEmployee(this.editData.id, this.newEmployee).subscribe(
          (data) => {
            alert('Employee Updated.');
            this.employeeForm.reset();
            this.dialogRef.close();
            location.reload();
          },
          (error) => {
            console.log(error);
            alert('Error Adding New Employee!');
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
