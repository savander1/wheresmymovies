///<reference path="jquery.d.ts" />
interface IMovie {
    Id: string;
    Title: string;
    Year: number[]; 
    Rated: string;
    Released: Date;
    Runtime: TimeRanges; 
    Genre: string[]; 
    Director: string[];
    Writer: string[];
    Actors: string[];
    Plot: string;
    Language: string[]; 
    Country: string;
    ThumbImgUrl: string;
    FullImgUrl: string;
    Location: string;
}

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

class MovieController extends Controller{
    
    constructor(address: string) { super(address); }

    get(success:Function, failure:Function): void {
        var id = $('#id').val();
        var name = $('#title').val();
        
        $.ajax({
            type: "GET",
            url: this.address,
            data: { name: name, id: id },
            success: function (jqXHr) { success(jqXHr); },
            error: function (jqXHr, textStatus, errorThrown) { failure(jqXHr, textStatus, errorThrown) }
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
        searchController = searchControllerAddress;
        authController = authControllerAddress;
        movieController = movieControllerAdderess;
    } 

    private static setImage(src: string): JQuery {
        var img = $('<img id="dynamic">');
        img.attr('src', src);
        
        return img;
    }

    private static populateForm(): void {
        movieController.get(function(data:IMovie){
            $('#id').val(data.Id);
            $('#title').val(data.Title);
            $('#year').val(data.Year[0].toString());
            $('#released').val();
            $('#runtime').val();
            $('#genre').val(data.Genre);
            $('#rated').val(data.Rated);
            $('#director').val(data.Director);
            $('#writer').val(data.Writer);
            $('#language').val(data.Language);
            $('#location').val(data.Location);
            $('#plot').text(data.Plot);
            var thumb = WheresMyMovies.setImage(data.FullImgUrl);
            var poster = $('#poster');
            thumb.appendTo(poster);
        }, function(jqXHr, textStatus, errorThrown) {
            movieController.error(jqXHr.responseText);
            movieController.error(textStatus);
            movieController.error(errorThrown);
        });
    }
    
    public init(): void {
        $('#add').click(event => {
            $('body > div form').css('display', 'block');
            event.stopPropagation();
            event.preventDefault();
        });

        $('#check').click(event => {
            event.stopPropagation();
            event.preventDefault();
            WheresMyMovies.populateForm();
        });
    }
}

var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');

var movieApp = new WheresMyMovies(searchController, movieController, authController);

$('document').ready(movieApp.init);
