abstract class Controller{
    constructor(protected address: string) {
    }
}

class SearchController extends Controller{

    constructor(address: string) { super(address);  }
}

class MovieController extends Controller{
    
    constructor(address: string) { super(address); }
}

class AuthController extends Controller{
    
    constructor(address: string) { super(address); }
}

class WheresMyMovies {
    
    private searchController:SearchController;
    private movieController:MovieController;
    private authController:AuthController;
    
    constructor(searchControllerAddress:SearchController, movieControllerAdderess:MovieController, authControllerAddress:AuthController){
        
    }
    
    show(selector: string): void {
        $(selector).css('display', 'show');
        
        
    }

        
    init():void {
        $('a[data-command="add"]').click(function (event) {
            this.show('.form');
            event.preventDefault();
        });
    }
}

var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');

var movieApp = new WheresMyMovies(searchController, movieController, authController);

$('document').ready(movieApp.init);