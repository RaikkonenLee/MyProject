import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-validationform',
  templateUrl: './validationform.component.html'
  //styleUrls: ['./validationform.component.css']
})
export class ValidationformComponent{
  // implements OnInit 
  // ngOnInit() {
     
  // }
  complexForm: FormGroup;

  constructor(fb: FormBuilder) {
    this.complexForm = fb.group({
      //表示一定要輸入
      'firstName': [null, Validators.required],
      //表示一定要輸入，而且最短為5個字元，最多為10個字元。有多個規則時用陣列包住。
      'lastName': [null, 
        Validators.compose([
          Validators.required, 
          Validators.minLength(5), Validators.maxLength(10)])],
      'gender': [null, Validators.required],
      'hiking': [false],
      'running': [false],
      'swimming': [false]
    });

    //用來觀察表格元素的變化
    console.log(this.complexForm);
    this.complexForm.valueChanges.subscribe((form: any) => {
      console.log('form change to:', form);
    });
   }

   //提交執行的程式
   submitForm(value: any) {
     console.log(value);
   }

}
