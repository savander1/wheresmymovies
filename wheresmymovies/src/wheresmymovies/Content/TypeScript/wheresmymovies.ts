///<reference path="jquery.d.ts" />
interface Movie {
    imdbId
}

class Controller{
    constructor(protected address: string) {
    }
        
    protected error(err:any): void {
        if (err.responseCode) {
            console.log(err.responseCode);
        }
    }
}

class SearchController extends Controller{

    constructor(address: string) { super(address);  }
}

class MovieController extends Controller{
    
    constructor(address: string) { super(address); }

    error(err: any): void {
        super.error(err);
    };

    get(): JQueryPromise<any> {
        var id = $('#id').val();
        var name = $('#title').val();
        var query = '?id=' + encodeURIComponent(id) + '&name=' + encodeURIComponent(name);
        
        var url = this.address + query;

        var me = this;
        
        return $.ajax({
            type: "GET",
            url: url,
            dataType: 'application/json'
        }).pipe(function (data) {
            return data.responseCode != 200 ?
                $.Deferred().reject(data) :
                data;
        }).fail(function (err) {
            me.error(err);
            return err;
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

    private static setImage(src: string): JQuery {
        var img = $('<img id="dynamic">');
        img.attr('src', src);
        
        return img;
    }

    private static populateForm(): void {
        movieController.get().done(function (data) {
            $('#id').val();
            $('#title').val();
            $('#year').val();
            $('#released').val();
            $('#runtime').val();
            $('#genre').val();
            $('#rated').val();
            $('#director').val();
            $('#writer').val();
            $('#language').val();
            $('#location').val();
            $('#plot').text();
            var thumb = WheresMyMovies.setImage(data.FullImgUrl);
            $('#poster').appendTo(thumb);
        })
    }
    
    public init(): void {
        $('#add').click(function (event) {
            $('.form').css('display', 'block');
            event.stopPropagation();
            event.preventDefault();
        });

        $('#check').click(function (event) {
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
