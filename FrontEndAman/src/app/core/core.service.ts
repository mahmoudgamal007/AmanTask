import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Department } from '../models/department';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root',
})
export class CoreService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}
  addEmployee(employee: Employee) {
    return this.http.post(this.baseUrl + 'Employee', employee);
  }
  addDepartment(department: Department) {
    return this.http.post(this.baseUrl + 'Department', department);
  }
  getEmployees() {
    return this.http.get(this.baseUrl + 'Employee');
  }
  getAddresses() {
    return this.http.get(this.baseUrl + 'Address');
  }
  getAddressByEmpId(id: number) {
    return this.http.get(
      this.baseUrl + 'Address/GetAddressByUserId?empId=' + id
    );
  }
  getEmployeeById(id: number) {
    return this.http.get(this.baseUrl + 'Employee/GetEmployeeById?id=' + id);
  }
  getDepartmentById(id: number) {
    return this.http.get(
      this.baseUrl + 'Department/GetDepartmentById?id=' + id
    );
  }
  getDepartments() {
    return this.http.get(this.baseUrl + 'Department');
  }
  deleteEmployee(id: number) {
    return this.http.delete(this.baseUrl + 'Employee?id=' + id);
  }
  deleteDepartment(id: number) {
    return this.http.delete(this.baseUrl + 'Department?id=' + id);
  }
  putEmployee(id: number, data: Employee) {
    return this.http.put(this.baseUrl + 'Employee?id=' + id, data);
  }
  putDepartment(id: number, data: Department) {
    return this.http.put(this.baseUrl + 'Department?id=' + id, data);
  }
}
