export class TodoTask {

    constructor(){
        this.id='00000000-0000-0000-0000-000000000000';
        this.name="";
        this.completionDate=null;
        this.dueDate=null;
        
        console.log("new task is created")
    }
    id: string="";
    name: string ="";
    completionDate:any;
    dueDate:any;

    isComplete() :boolean {
        return this.completionDate!=null ;
    }

}