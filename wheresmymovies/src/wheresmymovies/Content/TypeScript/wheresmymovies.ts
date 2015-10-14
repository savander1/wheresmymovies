///<reference path="jquery.d.ts" />
class Controller{
    constructor(protected address: string) {
    }
}

class SearchController extends Controller{

    constructor(address: string) { super(address);  }
}

class MovieController extends Controller{
    
    constructor(address: string) { super(address); }

    get(): void {

    }
}

class AuthController extends Controller{
    
    constructor(address: string) { super(address); }
}

class WheresMyMovies {
    
    private searchController:SearchController;
    private movieController:MovieController;
    private authController:AuthController;
    
    constructor(searchControllerAddress: SearchController, movieControllerAdderess: MovieController, authControllerAddress: AuthController) {
        this.searchController = searchController;
        this.authController = authController;
        this.movieController = movieController;
    }
    
    init():void {
        $('#add').click(function (event) {
            $('.form').css('display', 'block');
            event.stopPropagation();
            event.preventDefault();
        });
    }
}

var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');

var movieApp = new WheresMyMovies(searchController, movieController, authController);

$('document').ready(movieApp.init);
