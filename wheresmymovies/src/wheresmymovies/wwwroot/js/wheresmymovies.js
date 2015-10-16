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
    MovieController.prototype.get = function () {
        var id = $('#id').val();
        var name = $('#title').val();
        var query = '?id=' + encodeURIComponent(id) + '&name=' + encodeURIComponent(name);
        var url = this.address + query;
        $.ajax({
            type: "GET",
            url: url,
            dataType: 'application/json'
        }).done(function (data) {
            alert(data);
        }).fail(function (error) {
            error(error);
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
    WheresMyMovies.prototype.init = function () {
        $('#add').click(function (event) {
            $('.form').css('display', 'block');
            event.stopPropagation();
            event.preventDefault();
        });
    };
    return WheresMyMovies;
})();
var searchController = new SearchController('/api/search/');
var movieController = new MovieController('/api/movies/');
var authController = new AuthController('/api/auth/');
var movieApp = new WheresMyMovies(searchController, movieController, authController);
$('document').ready(movieApp.init);
