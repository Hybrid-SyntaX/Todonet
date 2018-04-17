import { Http } from '@angular/http';
import { Injectable, Output, EventEmitter } from '@angular/core';
import 'rxjs/add/operator/map'


@Injectable()
export class SharedService {
    public newTaskIsAdded: boolean = false;

    constructor() {

    }


    isUpdated = false;

    @Output() change: EventEmitter<boolean> = new EventEmitter();

    toggle() {
        this.isUpdated = !this.isUpdated;
        this.change.emit(this.isUpdated);
    }
   
}