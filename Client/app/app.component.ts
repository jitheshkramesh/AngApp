import { Component, Input, OnChanges, SimpleChange} from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-root',
    templateUrl: `./app.component.html`,
    styleUrls: [`./app.component.css`]
})
export class AppComponent {
    form;
    ngOnInit() {
        this.form = new FormGroup({
            decimal: new FormControl(""),
            binary: new FormControl(""),
            octal: new FormControl(""),
            hexa: new FormControl("")
        });
    }

    decimalChanged = function (oldValue, newValue) {
        if (newValue != "") {
            this.form.patchValue({ binary: parseInt(newValue, 10).toString(2) });
            this.form.patchValue({ octal: parseInt(newValue, 10).toString(8) });
            this.form.patchValue({ hexa: parseInt(newValue, 10).toString(16).toUpperCase() });
        }
        else {
            this.form.patchValue({ binary: "" });
            this.form.patchValue({ octal: "" });
            this.form.patchValue({ hexa: "" });
        }
    }

    //b = 0;
    //o = 0;
    ////h = 0;

    //binaryChanged = function (oldValue, newValue) {
    //    //this.b = this.b + 1;
    //    //if (this.b == 1) {
    //        if (newValue != "") {
    //            this.form.patchValue({ decimal: parseInt(newValue, 2).toString(10) });
    //            this.form.patchValue({ octal: parseInt(newValue, 2).toString(8) });
    //            this.form.patchValue({ hexa: parseInt(newValue, 2).toString(16) });
    //        }
    //        else {
    //            this.form.patchValue({ decimal: "" });
    //            this.form.patchValue({ octal: "" });
    //            this.form.patchValue({ hexa: "" });
    //        }
    //    //    this.b = 0;
    //    //}
    //}

    //octalChanged = function (oldValue, newValue) {
    //    this.o = this.o + 1;
    //    if (this.o == 1) {
    //        if (newValue != "") {
    //            this.form.patchValue({ decimal: parseInt(newValue, 2).toString(10) });
    //                        }
    //        else {
    //            this.form.patchValue({ decimal: "" });
    //        }
    //        this.o = 0;
    //    }
    //}

    //name = 'Dot Net Tricks MVC5 With Angular';
    //title = "App Starts";
    items = ["Angular 4", "React", "Underscore"];
    newitem = "";
    pushitem = function () {
        if (this.newitem != "") {
            this.items.push(this.newitem);
            this.newitem = "";
        }
    }
    removeitem = function (index) {
        this.items.splice(index, 1);
    }
}


//@Component({
//  selector: 'my-app',
////    template: `<h1>Hello {{name}}</h1>
////<div>
////<input type="text">
////</div>`,
//    templateUrl:`./app.component.html`
//})
//export class AppComponent  {
//    name = 'Dot Net Tricks MVC5 With Angular';
//}
