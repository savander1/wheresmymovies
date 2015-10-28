var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
///<reference path="jquery.d.ts" />
///<reference path="typeahead.d.ts"/>
var Display;
(function (Display) {
    Display[Display["Show"] = 0] = "Show";
    Display[Display["Hide"] = 1] = "Hide";
})(Display || (Display = {}));
var Controller = (function () {
    function Controller(address) {
        this.address = address;
    }
    Controller.prototype.error = function (err) {
        console.log(err);
    };
    return Controller;
})();
var SearchController = (function (_super) {
    __extends(SearchController, _super);
    function SearchController(address) {
        _super.call(this, address);
    }
    return SearchController;
})(Controller);
var MovieController = (function (_super) {
    __extends(MovieController, _super);
    function MovieController(address) {
        _super.call(this, address);
    }
    MovieController.prototype.get = function (success, failure) {
        var id = $('#id').val();
        var name = $('#title').val();
        $.ajax({
            type: "GET",
            url: this.address,
            data: { name: name, id: id },
            success: function (jqXHr) { success(jqXHr); },
            error: function (jqXHr, textStatus, errorThrown) { failure(jqXHr, textStatus, errorThrown); }
        });
    };
    return MovieController;
})(Controller);
var AuthController = (function (_super) {
    __extends(AuthController, _super);
    function AuthController(address) {
        _super.call(this, address);
    }
    return AuthController;
})(Controller);
var WheresMyMovies = (function () {
    function WheresMyMovies(searchControllerAddress, movieControllerAdderess, authControllerAddress) {
        searchController = searchControllerAddress;
        authController = authControllerAddress;
        movieController = movieControllerAdderess;
    }
    WheresMyMovies.setImage = function (src) {
        var img = $('<img>');
        img.attr('src', src);
        return img;
    };
    WheresMyMovies.populateForm = function () {
        movieController.get(function (data) {
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
            poster.html('');
            thumb.appendTo(poster);
            $('form img').removeAttr('style');
        }, function (jqXHr, textStatus, errorThrown) {
            movieController.error(jqXHr.responseText);
            movieController.error(textStatus);
            movieController.error(errorThrown);
            $('form img').removeAttr('style');
        });
    };
    WheresMyMovies.clearForm = function () {
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
    };
    WheresMyMovies.showForm = function (event, display) {
        var disp = 'hide';
        if (display === Display.Show) {
            disp = 'show';
        }
        $('body > div form').addClass(disp);
        event.stopPropagation();
        event.preventDefault();
    };
    WheresMyMovies.clear = function (event) {
        $('form img').addClass('hide');
        event.stopPropagation();
        event.preventDefault();
        WheresMyMovies.clearForm();
    };
    WheresMyMovies.check = function (event) {
        $('form img').addClass('show');
        event.stopPropagation();
        event.preventDefault();
        WheresMyMovies.populateForm();
    };
    WheresMyMovies.close = function (event) {
        WheresMyMovies.clear(event);
        WheresMyMovies.showForm(event, Display.Hide);
    };
    WheresMyMovies.prototype.init = function () {
        $('#add').click(function (event) {
            WheresMyMovies.showForm(event, Display.Show);
        });
        $('#check').click(WheresMyMovies.check);
        $('#clear').click(WheresMyMovies.clear);
        $('#close').click(WheresMyMovies.close);
    };
    return WheresMyMovies;
})();
var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');
var movieApp = new WheresMyMovies(searchController, movieController, authController);
$('document').ready(movieApp.init);
