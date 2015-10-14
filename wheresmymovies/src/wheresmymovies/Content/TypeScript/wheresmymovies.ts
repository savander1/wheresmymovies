///<reference path="jquery.d.ts" />
class Controller{
    constructor(protected address: string) {
    }
        
    error(err:Object): void {
        console.log(err);
    }
}

class SearchController extends Controller{

    constructor(address: string) { super(address);  }
}

class MovieController extends Controller{
    
    constructor(address: string) { super(address); }

    get(): void {
        var id = $('#id').val();
        var name = $('#title').val();
        var query = '?id=' + encodeURIComponent(id) + '&name=' + encodeURIComponent(name);
        
        var url  = this.address + query;
        
        $.ajax({
            type: "GET",
            url: url,
            dataType: 'application/json'
        }).done(function(data) {
            alert(data);
        }).fail(function(error){
            error(error);
        });
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
