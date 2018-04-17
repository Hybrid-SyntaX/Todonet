import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map'
import { TodoTask } from '../models/task';

@Injectable()
export class TasksService {
    API_ENDPOINT = '/api/tasks';
    constructor(private http: Http) {

    }
    getTasks() {
        return this.http.get(this.API_ENDPOINT)
            .map(res => res.json());
    }
    create(task: TodoTask) {
        return this.http.post(this.API_ENDPOINT, task)
            .map(res => res.json());
    }
    delete(id: string) {
        return this.http.delete(this.API_ENDPOINT+'/' + id)
            .map(res => res.json());
    }
    update(task: TodoTask): any {
        
        return this.http.put(this.API_ENDPOINT + '/' + task.id, task)
            .map(res => res.json());
    }
}