var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
///<reference path="jquery.d.ts" />
var Controller = (function () {
    function Controller(address) {
        this.address = address;
    }
    Controller.prototype.error = function (err) {
        if (err.responseCode) {
            console.log(err.responseCode);
        }
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
    MovieController.prototype.error = function (err) {
        _super.prototype.error.call(this, err);
    };
    ;
    MovieController.prototype.get = function () {
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
        this.searchController = searchController;
        this.authController = authController;
        this.movieController = movieController;
    }
    WheresMyMovies.setImage = function (src) {
        var img = $('<img id="dynamic">');
        img.attr('src', src);
        return img;
    };
    WheresMyMovies.populateForm = function () {
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
        });
    };
    WheresMyMovies.prototype.init = function () {
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
    };
    return WheresMyMovies;
})();
var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');
var movieApp = new WheresMyMovies(searchController, movieController, authController);
$('document').ready(movieApp.init);
