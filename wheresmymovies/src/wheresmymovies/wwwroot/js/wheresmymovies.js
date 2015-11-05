var __extends=this&&this.__extends||function(e,t){function r(){this.constructor=e}for(var o in t)t.hasOwnProperty(o)&&(e[o]=t[o]);e.prototype=null===t?Object.create(t):(r.prototype=t.prototype,new r)},Movie=function(){function e(){}return e}(),Display;!function(e){e[e.Show=0]="Show",e[e.Hide=1]="Hide"}(Display||(Display={}));var Controller=function(){function e(e){this.address=e}return e.prototype.error=function(e){console.log(e)},e}(),SearchController=function(e){function t(t){e.call(this,t)}return __extends(t,e),t}(Controller),MovieController=function(e){function t(t){e.call(this,t)}return __extends(t,e),t.prototype.get=function(e,t){var r=$("#id").val(),o=$("#title").val();$.ajax({type:"GET",url:this.address,data:{name:o,id:r},success:function(t){e(t)},error:function(e,r,o){t(e,r,o)}})},t.prototype.post=function(e,t,r){console.log(e),$.ajax({type:"POST",url:this.address,data:e,success:function(e){t(e)},error:function(e,t,o){r(e,t,o)}})},t}(Controller),AuthController=function(e){function t(t){e.call(this,t)}return __extends(t,e),t}(Controller),App=function(){function e(e,t,r){searchController=e,authController=r,movieController=t}return e.setImage=function(e){var t=$('<img id="posterImg">');return t.attr("src",e),t},e.canPopulateForm=function(){return""!==$("#id").val()||""!==$("#title").val()},e.populateForm=function(){movieController.get(function(t){$("#id").val(t.Id),$("#title").val(t.Title),$("#year").val(TimeFormatter.formatYear(t.Year)),$("#released").val(TimeFormatter.formatReleaseDate(t.Released)),$("#runtime").val(TimeFormatter.formatRuntime(t.Runtime)),$("#genre").val(t.Genre),$("#rated").val(t.Rated),$("#director").val(t.Director),$("#writer").val(t.Writer),$("#language").val(t.Language),$("#country").val(t.Country),$("#location").val(t.Location),$("#plot").text(t.Plot);var r=e.setImage(t.FullImgUrl),o=$("#poster");o.html(""),r.appendTo(o),$("form img").addClass("hide")},function(e,t,r){movieController.error(e.responseText),movieController.error(t),movieController.error(r),$("form img").addClass("hide")})},e.clearForm=function(){$("#id").val(""),$("#title").val(""),$("#year").val(""),$("#released").val(""),$("#runtime").val(""),$("#genre").val(""),$("#rated").val(""),$("#director").val(""),$("#writer").val(""),$("#language").val(""),$("#country").val(""),$("#location").val(""),$("#plot").text(""),$("#poster").html("")},e.killEvent=function(e){e.stopPropagation(),e.preventDefault()},e.getMovie=function(){var e=new Movie;return e.Id=$("#id").val(),e.Title=$("#title").val(),e.Year=$("#year").val(),e.Released=$("#released").val(),e.Runtime=$("#runtime").val(),e.Genre=$("#genre").val(),e.Rated=$("#rated").val(),e.Director=$("#director").val(),e.Writer=$("#writer").val(),e.Language=$("#language").val(),e.Country=$("#country").val(),e.Location=$("#location").val(),e.Plot=$("#plot").text(),e.FullImgUrl=$("#posterImg").attr("src"),e},e.showForm=function(t,r){e.killEvent(t);var o=$("body > div form"),n="hide";r===Display.Show&&(n="show"),o.removeAttr("class"),o.addClass(n)},e.clear=function(t){$("form img").addClass("hide"),e.killEvent(t),e.clearForm()},e.check=function(t){e.killEvent(t),e.canPopulateForm()?($("form img").addClass("show"),e.populateForm()):alert("Enter an ID or Title")},e.close=function(t){e.clear(t),e.showForm(t,Display.Hide)},e.submit=function(t){e.killEvent(t);var r=e.getMovie();movieController.post(r,function(){e.clear(t),e.showForm(t,Display.Hide)},function(e,t,r){movieController.error(e.responseText),movieController.error(t),movieController.error(r),$("form img").addClass("hide")})},e.prototype.init=function(){$("#add").click(function(t){e.showForm(t,Display.Show)}),$("#check").click(e.check),$("#clear").click(e.clear),$("#close").click(e.close),$("#submit").click(e.submit)},e}(),TimeFormatter=function(){function e(){}return e.formatYear=function(e){if(0===e.length)return"";if(1===e.length)return e[0].toString();var t=e[0].toString(),r=e[e.length-1].toString();return t+"-"+r},e.formatReleaseDate=function(e){if(void 0===e)return"";var t={weekday:"long",year:"numeric",month:"long",day:"numeric"};return new Date(e).toLocaleDateString("en-US",t)},e.formatRuntime=function(e){if(void 0===e)return"";var t=e.split(":");if(3!==t.length)throw"Invalid Runtime: "+e;var r=60*+t[0],o=+t[1],n=+t[2];return n=n>30?0:1,r+o+n+" minutes"},e}(),searchController=new SearchController("/api/search/"),movieController=new MovieController("/api/movies/"),authController=new AuthController("/api/auth/"),movieApp=new App(searchController,movieController,authController);$("document").ready(movieApp.init);