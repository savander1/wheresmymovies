/// <reference path="jquery.d.ts" />

interface IController{
    address:string;
}

class SearchController implements IController{

    constructor(public address:string) {
    }
}

class MovieController implements IController{
    
    constructor(public address:string) {
        
    }
}

class AuthController implements IController{
    
    constructor(public address:string) {
        
    }
}

class WheresMyMovies {
    
    private searchController:SearchController;
    private movieController:MovieController;
    private authController:AuthController;
    
    constructor(searchControllerAddress:SearchController, movieControllerAdderess:MovieController, authControllerAddress:AuthController){
        
    }
    
    init():void {
      
    }
}