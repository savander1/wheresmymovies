var SearchController = (function () {
    function SearchController(address) {
        this.address = address;
    }
    return SearchController;
})();

var MovieController = (function () {
    function MovieController(address) {
        this.address = address;
    }
    return MovieController;
})();

var AuthController = (function () {
    function AuthController(address) {
        this.address = address;
    }
    return AuthController;
})();

var WheresMyMovies = (function () {
    function WheresMyMovies(searchControllerAddress, movieControllerAdderess, authControllerAddress) {
    }
    WheresMyMovies.prototype.init = function () {
    };
    return WheresMyMovies;
})();
//# sourceMappingURL=wheresmymovies.js.map
