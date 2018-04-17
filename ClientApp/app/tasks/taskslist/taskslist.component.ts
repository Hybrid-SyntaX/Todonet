import { isDevMode, NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { Observable } from 'rxjs/Observable';
import { SharedService } from '../../services/shared.service';
import { TodoTask } from '../../models/task';


@Component({
    selector: 'tasks-list',
    templateUrl: './taskslist.component.html',
    //styleUrls: ['./taskslist.component.css']
})
//export namespace Tasks {
export class TasksListComponent implements OnInit {
    EMPTY_GUID = '00000000-0000-0000-0000-000000000000'
    tasks: any[];
    task: TodoTask = {
        id: this.EMPTY_GUID,
        name: ''
    };
    constructor(private tasksService: TasksService, private sharedService: SharedService) {


        this.tasks = [];
    }
    ngOnInit(): void {
        this.populate();
    }
    populate() {
        this.tasksService.getTasks()
            .subscribe(tasks => this.tasks = tasks);
    }
    save(): void {
        var result$ = (this.task.id != this.EMPTY_GUID) ? this.tasksService.update(this.task)
            : this.tasksService.create(this.task);

        result$.subscribe((x: any) => {
            this.populate();
        });
    }
    
    remove(currentTask: TodoTask): void {
        console.log("Remove " + currentTask.id)
        this.tasksService.delete(currentTask.id).subscribe(()=>this.populate());
    }
    edit(currentTask: TodoTask): void {
        console.log("Edit " + currentTask.id)
        this.task = currentTask;
    }

}
//}
