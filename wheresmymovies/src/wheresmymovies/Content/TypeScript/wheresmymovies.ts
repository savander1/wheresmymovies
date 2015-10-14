﻿abstract class Controller{
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
    
    constructor(searchControllerAddress: SearchController, movieControllerAdderess: MovieController, authControllerAddress: AuthController) {
        this.searchController = searchController;
        this.authController = authController;
        this.movieController = movieController;
    }

    show(selector: string, event: JQueryEventObject): void {
        $(selector).css('display', 'show');
        event.preventDefault(); 
    }

        
    init():void {
        $('a[data-command="add"]').click(function (event) { this.show('.form', event); });
    }
}

var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');

var movieApp = new WheresMyMovies(searchController, movieController, authController);

$('document').ready(movieApp.init);
