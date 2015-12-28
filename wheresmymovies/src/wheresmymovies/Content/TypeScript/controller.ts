///<reference path="common.ts" />
///<reference path="model.ts" />
///<reference path="ajax.ts" />

module Controller{
    
    abstract class Controller{
    constructor(protected address: string) {
    }
        
    public error(err:any): void {
        console.log(err);
    }
}

    class SearchController extends Controller{

        constructor(address: string) { super(address);  }
    }

    export class MovieController extends Controller{
        
        constructor(address: string) { super(address); }

        get(idName:Models.MovieSearchCriteria, success:Function, failure:Function): void {
            var movieRequest = new Ajax.Request(this.address, success, failure);
            movieRequest.get(idName);
        }
        
        post(movie:Models.Movie, success:Function, failure:Function):void {
            console.log(movie);
            
            var movieRequests = new Ajax.Request(this.address, success, failure);
            movieRequests.post(movie);
        }
    }

    class AuthController extends Controller{
        
        constructor(address: string) { super(address); }
    }
}