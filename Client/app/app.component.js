"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var AppComponent = /** @class */ (function () {
    function AppComponent() {
        this.decimalChanged = function (oldValue, newValue) {
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
        };
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
        this.items = ["Angular 4", "React", "Underscore"];
        this.newitem = "";
        this.pushitem = function () {
            if (this.newitem != "") {
                this.items.push(this.newitem);
                this.newitem = "";
            }
        };
        this.removeitem = function (index) {
            this.items.splice(index, 1);
        };
    }
    AppComponent.prototype.ngOnInit = function () {
        this.form = new forms_1.FormGroup({
            decimal: new forms_1.FormControl(""),
            binary: new forms_1.FormControl(""),
            octal: new forms_1.FormControl(""),
            hexa: new forms_1.FormControl("")
        });
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'app-root',
            templateUrl: "./app.component.html",
            styleUrls: ["./app.component.css"]
        })
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
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
//# sourceMappingURL=app.component.js.map