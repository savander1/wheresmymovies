///<reference path="jquery.d.ts" />
///<reference path="typeahead.d.ts"/>
///<reference path="form.ts" />
interface IMovie {
    Id: string;
    Title: string;
    Year: number[]; 
    Rated: string;
    Released: string;
    Runtime: string; 
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

class Movie implements IMovie{
    public Id: string;
    public Title: string;
    public Year: number[]; 
    public Rated: string;
    public Released: string;
    public Runtime: string; 
    public Genre: string[]; 
    public Director: string[];
    public Writer: string[];
    public Actors: string[];
    public Plot: string;
    public Language: string[]; 
    public Country: string;
    public ThumbImgUrl: string;
    public FullImgUrl: string;
    public Location: string;
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
        var name = encodeURIComponent( $('#title').val() );
        
        $.ajax({
            type: "GET",
            url: this.address,
            data: { name: name, id: id },
            success: jqXHr => { success(jqXHr); },
            error: (jqXHr, textStatus, errorThrown) => { failure(jqXHr, textStatus, errorThrown) }
        });
    }
    
    post(movie:IMovie, success:Function, failure:Function):void {
        console.log(movie);
        
        $.ajax({
            type: "POST",
            url: this.address,
            data: movie,
            success: jqXHr => { success(jqXHr); },
            error: (jqXHr, textStatus, errorThrown) => { failure(jqXHr, textStatus, errorThrown) }
        });
    }
}

class AuthController extends Controller{
    
    constructor(address: string) { super(address); }
}

class App {
    
    private searchController:SearchController;
    private movieController:MovieController;
    private authController: AuthController;
    
    constructor(searchControllerAddress: SearchController, movieControllerAdderess: MovieController, authControllerAddress: AuthController) {
        searchController = searchControllerAddress;
        authController = authControllerAddress;
        movieController = movieControllerAdderess;
    } 

    private static setImage(src: string): JQuery {
        var img = $('<img id="posterImg">');
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
            $('#country').val(data.Country);
            $('#location').val(data.Location);
            $('#plot').text(data.Plot);
            var thumb = App.setImage(data.FullImgUrl);
            var poster = $('#poster');
            poster.html('');
            thumb.appendTo(poster);
            $('form img').addClass('hide');
        }, (jqXHr, textStatus, errorThrown) => {
            movieController.error(jqXHr.responseText);
            movieController.error(textStatus);
            movieController.error(errorThrown);
            $('form img').addClass('hide');
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
        $('#country').val('');
        $('#location').val('');
        $('#plot').text('');
        $('#poster').html('');
    }

    private static killEvent(event: JQueryEventObject): void {
        event.stopPropagation();
        event.preventDefault();
    }

    private static getMovie(): IMovie {
        var data = new Movie();
        data.Id = $('#id').val();
        data.Title = $('#title').val();
        data.Year = $('#year').val();
        data.Released = $('#released').val();
        data.Runtime =  $('#runtime').val();
        data.Genre = $('#genre').val();
        data.Rated = $('#rated').val();
        data.Director = $('#director').val();
        data.Writer = $('#writer').val();
        data.Language = $('#language').val();
        data.Country = $('#country').val();
        data.Location = $('#location').val();
        data.Plot = $('#plot').text();
        data.FullImgUrl =  $('#posterImg').attr('src');
        return data;
    }

    private static showForm(event: JQueryEventObject, display: Display): void {
        App.killEvent(event);
        var form = $('body > div form');
        var disp = 'hide';
        if (display === Display.Show) {
            disp = 'show';
        }
        form.removeAttr('class');
        form.addClass(disp);  
    }

    private static clear(event:JQueryEventObject): void {
        $('form img').addClass('hide');
        App.killEvent(event);
        App.clearForm();
    }

    private static check(event: JQueryEventObject): void {
        App.killEvent(event);
        if (App.canPopulateForm()) {
            $('form img').addClass('show');
            App.populateForm();
        }
        else {
            alert('Enter an ID or Title');
        }
    }

    private static close(event: JQueryEventObject): void {
        App.clear(event);
        App.showForm(event, Display.Hide);
    }

    private static submit(event: JQueryEventObject): void {
        App.killEvent(event);
        $('form img').addClass('show');
        var movie = App.getMovie();
        movieController.post(movie, () => {
            App.clear(event);
            App.showForm(event, Display.Hide);
        },(jqXHr, textStatus, errorThrown) => {
            movieController.error(jqXHr.responseText);
            movieController.error(textStatus);
            movieController.error(errorThrown);
            alert('There was a problem with your request: ' + errorThrown);
            $('form img').addClass('hide');
        });
    }
    
    public init(): void {
        $('#add').click(event => {
            App.showForm(event, Display.Show);
        });

        $('#check').click(App.check);
        $('#clear').click(App.clear);
        $('#close').click(App.close);
        $('#submit').click(App.submit);
    }
}

class TimeFormatter {
    public static formatYear(year: Number[]): string {
        if (year === void 0 || year.length === 0) {
            return '';
        }
        if (year.length === 1) {
            return year[0].toString();
        }
        var from = year[0].toString();
        var to = year[year.length - 1].toString();
        return from + '-' + to;
    }

    public static formatReleaseDate(year: string): string {
        if (year === void 0) {
            return '';
        }
        var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        return new Date(year).toLocaleDateString('en-US', options);
    }

    public static formatRuntime(runtime: string): string {
        if (runtime === void 0) {'en-US'
            return '';
        }
        var rt = runtime.split(':');
        if (rt.length !== 3) {
            throw 'Invalid Runtime: ' + runtime;
        }
        var hours = +rt[0] * 60;
        var mins = +rt[1];
        var secs = +rt[2];

        if (secs > 30) {
            secs = 0;
        } else {
            secs = 1;
        }

        return hours + mins + secs + ' minutes'
    }
}

var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');

var movieApp = new App(searchController, movieController, authController);

//$('document').ready(movieApp.init);
