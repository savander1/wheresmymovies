module Ajax{
    
    export class Request {
        
        private endPoint:string;
        private onSuccess:Function;
        private onFailure:Function;
        
        constructor(endPoint:string, onSuccess:Function, onFailure:Function){
            this.endPoint = endPoint;
            this.onSuccess = onSuccess;
            this.onFailure = onFailure;
        }
        
        get(data:any):void{
            this.makeRequest(data, 'GET');
        }
        
        post(data:any):void{
            this.makeRequest(data, 'POST');
        }
        
        
        private makeRequest(data:any, type:string, passAsQuery:boolean = false):void{
            var me = this;
            var request = new XMLHttpRequest();
            request.open(type, this.endPoint, true);
            request.onreadystatechange = function () {
                if (request.readyState != 4 || request.status != 200){
                    me.onFailure(request);
                }
                me.onSuccess(request);
            };
            request.send(JSON.stringify(data);
        }
    }
    
}