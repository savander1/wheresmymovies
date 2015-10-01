interface Controller{
    address:string;
}

class SearchController implements Controller{

    constructor(public address:string) {
    }
}

class MovieController implements Controller{
    
    constructor(public address:string) {
        
    }
}

class AuthController implements Controller{
    
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