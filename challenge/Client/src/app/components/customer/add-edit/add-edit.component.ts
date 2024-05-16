import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'cust-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css'],
})
export class AddEditComponent implements OnInit {
  @Output() closeModalEvent = new EventEmitter();
  @Input() customer: any;
  customerForm: FormGroup;
  btnTitle: string = '';

  constructor(
    private service: ApiService,
    private readonly formBuilder: FormBuilder
  ) {
    this.customerForm = this.formBuilder.group({
      customerId: [0],
      firstName: [''],
      lastName: [''],
      email: [''],
    });
  }

  ngOnInit() {
    this.btnTitle = this.customer.customerId == 0 ? 'Add' : 'Update';
    this.customerForm.patchValue(this.customer);
  }

  onSubmit() {
    this.customerForm.markAllAsTouched();
    if (this.customerForm.invalid) {
      alert("Can't add the Customer some fields are required");
      return;
    }
    const customer = this.customerForm.getRawValue();
    if (this.customer.customerId == 0) {
      // Add
      this.service.addCustomer(customer).subscribe((res) => {
        if (res) {
          alert('The Customer has been added');
          this.closeModalEvent.emit();
        } else alert('Can not add Customer');
      });
    } else {
      // Update
      this.service.updateCustomer(customer).subscribe((res) => {
        if (res) {
          alert('The Customer has been updated');
          this.closeModalEvent.emit();
        } else alert('Can not update Customer');
      });
    }
  }
}
