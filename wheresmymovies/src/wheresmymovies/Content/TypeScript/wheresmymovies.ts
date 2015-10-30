///<reference path="jquery.d.ts" />
///<reference path="typeahead.d.ts"/>
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

enum Display {
    Show,
    Hide
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
            success: jqXHr => { success(jqXHr); },
            error: (jqXHr, textStatus, errorThrown) => { failure(jqXHr, textStatus, errorThrown) }
        });
    }
}

class AuthController extends Controller{
    
    constructor(address: string) { super(address); }
}

class WheresMyMovies {
    
    private searchController:SearchController;
    private movieController:MovieController;
    private authController: AuthController;
    
    constructor(searchControllerAddress: SearchController, movieControllerAdderess: MovieController, authControllerAddress: AuthController) {
        searchController = searchControllerAddress;
        authController = authControllerAddress;
        movieController = movieControllerAdderess;
    } 

    private static setImage(src: string): JQuery {
        var img = $('<img>');
        img.attr('src', src);
        
        return img;
    }

    private static canPopulateForm(): boolean {
        return $('#id').val() !== '' || $('#title').val() !== '';
    }

    private static populateForm(): void {
        movieController.get((data:IMovie) => {
            $('#id').val(data.Id);
            $('#title').val(data.Title);
            $('#year').val(TimeFormatter.formatYear(data.Year));
            $('#released').val(TimeFormatter.formatReleaseDate(data.Released));
            $('#runtime').val(TimeFormatter.formatRuntime(data.Runtime));
            $('#genre').val(data.Genre);
            $('#rated').val(data.Rated);
            $('#director').val(data.Director);
            $('#writer').val(data.Writer);
            $('#language').val(data.Language);
            $('#location').val(data.Location);
            $('#plot').text(data.Plot);
            var thumb = WheresMyMovies.setImage(data.FullImgUrl);
            var poster = $('#poster');
            poster.html('');
            thumb.appendTo(poster);
            $('form img').removeAttr('style');
        }, (jqXHr, textStatus, errorThrown) => {
            movieController.error(jqXHr.responseText);
            movieController.error(textStatus);
            movieController.error(errorThrown);
            $('form img').removeAttr('style');
        });
    }

    private static clearForm(): void {
        $('#id').val('');
        $('#title').val('');
        $('#year').val('');
        $('#released').val('');
        $('#runtime').val('');
        $('#genre').val('');
        $('#rated').val('');
        $('#director').val('');
        $('#writer').val('');
        $('#language').val('');
        $('#location').val('');
        $('#plot').text('');
        $('#poster').html('');
    }

    private static killEvent(event: JQueryEventObject): void {
        event.stopPropagation();
        event.preventDefault();
    }

    private static submitForm(): void {
        var form = $('form');
        form.submit();
        var body = form.serialize();
        alert(body);
    }

    private static showForm(event: JQueryEventObject, display: Display): void {
        WheresMyMovies.killEvent(event);
        var disp = 'hide';
        if (display === Display.Show) {
            disp = 'show';
        }
        $('body > div form').addClass(disp);  
    }

    private static clear(event:JQueryEventObject): void {
        $('form img').addClass('hide');
        WheresMyMovies.killEvent(event);
        WheresMyMovies.clearForm();
    }

    private static check(event: JQueryEventObject): void {
        WheresMyMovies.killEvent(event);
        if (WheresMyMovies.canPopulateForm()) {
            $('form img').addClass('show');
            WheresMyMovies.populateForm();
        }
        else {
            alert('Enter an ID or Title');
        }
    }

    private static close(event: JQueryEventObject): void {
        WheresMyMovies.clear(event);
        WheresMyMovies.showForm(event, Display.Hide);
    }

    private static submit(event: JQueryEventObject): void {
        WheresMyMovies.killEvent(event);
        WheresMyMovies.submitForm();
    }
    
    public init(): void {
        $('#add').click(event => {
            WheresMyMovies.showForm(event, Display.Show);
        });

        $('#check').click(WheresMyMovies.check);
        $('#clear').click(WheresMyMovies.clear);
        $('#close').click(WheresMyMovies.close);
        $('#submit').click(WheresMyMovies.submit);

    }
}

class TimeFormatter {
    public static formatYear(year: Number[]): string {
        return '1999';
    }

    public static formatReleaseDate(year: Date): string {
        return '1998';
    }

    public static formatRuntime(runtime: TimeRanges): string {
        return '89 minutes';
    }
}

var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');

var movieApp = new WheresMyMovies(searchController, movieController, authController);

$('document').ready(movieApp.init);
