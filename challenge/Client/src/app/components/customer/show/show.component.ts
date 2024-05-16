import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css'],
})
export class ShowComponent implements OnInit {
  CustomerList: any = [];
  ModalTitle = '';
  ActivateAddEdit: boolean = false;
  customer: any;

  constructor(private service: ApiService) {}

  ngOnInit(): void {
    this.refreshList();
  }

  addClick() {
    this.customer = {
      customerId: 0,
      firstName: '',
      lastName: '',
      email: '',
    };
    this.ModalTitle = 'Add Customer';
    this.ActivateAddEdit = true;
  }

  editClick(item: any) {
    this.customer = item;
    this.ModalTitle = 'Edit Customer';
    this.ActivateAddEdit = true;
  }

  deleteClick(item: any) {
    if (confirm('Are you sure??')) {
      this.service.deleteCustomer(item.CustomerId).subscribe((data) => {
        alert(data.toString());
        this.refreshList();
      });
    }
  }

  closeClick() {
    this.ActivateAddEdit = false;
    this.refreshList();
  }

  refreshList() {
    this.service.getCustomerList().subscribe((data) => {
      this.CustomerList = data;
    });
  }
}
