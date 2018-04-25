import { TodoTask } from './../../models/task';
import { isDevMode, NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { Component, OnInit } from '@angular/core';
import { TasksService } from '../../services/tasks.service';
import { Observable } from 'rxjs/Observable';
import { SharedService } from '../../services/shared.service';



@Component({
    selector: 'tasks-list',
    templateUrl: './taskslist.component.html',
    styleUrls: ['./taskslist.component.css']
})
//export namespace Tasks {
export class TasksListComponent implements OnInit {
    EMPTY_GUID = '00000000-0000-0000-0000-000000000000'
    tasks: any[];
    task: TodoTask=new TodoTask();
    devMode =isDevMode()
    
    deleteMode=false;
    editMode=false;
    newMode=false;

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
    newTask():void{
        this.task=new TodoTask();
    }
    remove(): void {
        this.tasksService.delete(this.task.id).subscribe(()=>this.populate());
        console.log("Remove " + this.task.id)
    }
    edit(): void {
        //this.task = currentTask;
        console.log("Edit " + this.task.id)
    }
    select(currentTask: TodoTask): void {
        console.log("Select " + currentTask.id)
        this.task = currentTask;
        this.cancelModes();
        
    }
    cancel():void{
        //method 1: very safe; costly
        if(this.task.id != this.EMPTY_GUID)
        {
            this.tasksService.getTask(this.task.id).subscribe((t)=>{
                this.task=t;
                this.populate();
            });
        }
        else
        {
            this.newTask();
        }
        //method 2: replace task with original task
        //this.task=this.originalTask;

    }
    cancelModes(){
        this.deleteMode=false;
        this.editMode=false;
        this.newMode=false;
    }
    toggleCompletion():void{
        if(this.task.id != this.EMPTY_GUID)
        {
            console.log('BEFORE complete:' +this.task.completionDate)
            if(this.task.completionDate==null)
                this.tasksService.do(this.task).subscribe(()=>this.populate());
            else
                this.tasksService.undo(this.task).subscribe(()=>this.populate());
                
            
            console.log('AFTER complete:' +this.task.completionDate)
        }
    }
}
//}
