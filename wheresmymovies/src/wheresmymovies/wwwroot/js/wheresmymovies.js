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
    WheresMyMovies.canPopulateForm = function () {
        return $('#id').val() !== '' || $('#title').val() !== '';
    };
    WheresMyMovies.populateForm = function () {
        movieController.get(function (data) {
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
            var thumb = WheresMyMovies.setImage(data.FullImgUrl);
            var poster = $('#poster');
            poster.html('');
            thumb.appendTo(poster);
            $('form img').addClass('hide');
        }, function (jqXHr, textStatus, errorThrown) {
            movieController.error(jqXHr.responseText);
            movieController.error(textStatus);
            movieController.error(errorThrown);
            $('form img').addClass('hide');
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
        $('#country').val('');
        $('#location').val('');
        $('#plot').text('');
        $('#poster').html('');
    };
    WheresMyMovies.killEvent = function (event) {
        event.stopPropagation();
        event.preventDefault();
    };
    WheresMyMovies.submitForm = function () {
        var form = $('form');
        form.submit();
        var body = form.serialize();
        alert(body);
    };
    WheresMyMovies.showForm = function (event, display) {
        WheresMyMovies.killEvent(event);
        var form = $('body > div form');
        var disp = 'hide';
        if (display === Display.Show) {
            disp = 'show';
        }
        form.removeAttr('class');
        form.addClass(disp);
    };
    WheresMyMovies.clear = function (event) {
        $('form img').addClass('hide');
        WheresMyMovies.killEvent(event);
        WheresMyMovies.clearForm();
    };
    WheresMyMovies.check = function (event) {
        WheresMyMovies.killEvent(event);
        if (WheresMyMovies.canPopulateForm()) {
            $('form img').addClass('show');
            WheresMyMovies.populateForm();
        }
        else {
            alert('Enter an ID or Title');
        }
    };
    WheresMyMovies.close = function (event) {
        WheresMyMovies.clear(event);
        WheresMyMovies.showForm(event, Display.Hide);
    };
    WheresMyMovies.submit = function (event) {
        WheresMyMovies.killEvent(event);
        WheresMyMovies.submitForm();
    };
    WheresMyMovies.prototype.init = function () {
        $('#add').click(function (event) {
            WheresMyMovies.showForm(event, Display.Show);
        });
        $('#check').click(WheresMyMovies.check);
        $('#clear').click(WheresMyMovies.clear);
        $('#close').click(WheresMyMovies.close);
        $('#submit').click(WheresMyMovies.submit);
    };
    return WheresMyMovies;
})();
var TimeFormatter = (function () {
    function TimeFormatter() {
    }
    TimeFormatter.formatYear = function (year) {
        if (year.length === 0) {
            return '';
        }
        if (year.length === 1) {
            return year[0].toString();
        }
        var from = year[0].toString();
        var to = year[year.length - 1].toString();
        return from + '-' + to;
    };
    TimeFormatter.formatReleaseDate = function (year) {
        if (year === void 0) {
            return '';
        }
        var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        return new Date(year).toLocaleDateString('en-US', options);
    };
    TimeFormatter.formatRuntime = function (runtime) {
        if (runtime === void 0) {
            'en-US';
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
        }
        else {
            secs = 1;
        }
        return hours + mins + secs + ' minutes';
    };
    return TimeFormatter;
})();
var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');
var movieApp = new WheresMyMovies(searchController, movieController, authController);
$('document').ready(movieApp.init);
