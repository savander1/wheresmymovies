﻿///<reference path="jquery.d.ts" />
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
        $('#location').val('');
        $('#plot').text('');
        $('#poster').html('');
    }

    private static killEvent(event: JQueryEventObject): void {
        event.stopPropagation();
        event.preventDefault();
    }

    private static submitForm(): void {
        var body = $('form').serialize();
        alert(body);
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
    }

    private static close(event: JQueryEventObject): void {
        App.clear(event);
        App.showForm(event, Display.Hide);
    }

    private static submit(event: JQueryEventObject): void {
        App.killEvent(event);
        App.submitForm();
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
    public static formatYear(year: string): string {
        return '1999';
    }

    public static formatRuntime(runtime: string): string {
        return '89 minutes';
    }
}

var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');

var movieApp = new App(searchController, movieController, authController);

$('document').ready(movieApp.init);
