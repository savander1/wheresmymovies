var Ajax;!function(e){var t=function(){function e(e,t,n){this.endPoint=e,this.onSuccess=t,this.onFailure=n}return e.prototype.get=function(e){this.makeRequest(e,"GET")},e.prototype.post=function(e){this.makeRequest(e,"POST")},e.prototype.makeRequest=function(e,t){var n=this,o=new XMLHttpRequest;o.open(t,this.endPoint,!0),o.onreadystatechange=function(){(4!=o.readyState||200!=o.status)&&n.onFailure(o),n.onSuccess(o)},o.send(JSON.stringify(e))},e}();e.Request=t}(Ajax||(Ajax={}));var Common;!function(e){var t=function(){function e(e,t){this.onclick=t,this.buttonText=e}return e.prototype.render=function(){var e=document.createElement("button");e.id=this.buttonText.toLowerCase(),e.className="button";var t=document.createTextNode(this.buttonText);return e.appendChild(t),e.addEventListener("click",this.onclick),e},e}();e.Button=t}(Common||(Common={}));var __extends=this&&this.__extends||function(e,t){function n(){this.constructor=e}for(var o in t)t.hasOwnProperty(o)&&(e[o]=t[o]);e.prototype=null===t?Object.create(t):(n.prototype=t.prototype,new n)},Form;!function(e){!function(e){e[e.text=0]="text",e[e.number=1]="number",e[e.textbox=2]="textbox",e[e.none=3]="none"}(e.FieldValidationType||(e.FieldValidationType={}));var t=e.FieldValidationType,n=function(){function e(e,t,n,o,r){void 0===o&&(o=""),void 0===r&&(r=""),this.invalidClass=" invalid",this.fieldType=e,this.label=t,this.cls=n,this.name=o,""===r&&(r=e+"_"+Math.floor(10*Math.random()+1)),this.id=r}return e.prototype.render=function(){return void 0},e.prototype.isValid=function(){if(this.fieldType===t.none)return!0;var e=document.getElementById(this.id),n=e.value;if(this.fieldType===t.text||this.fieldType===t.textbox)return this.validateText(n);if(this.fieldType===t.number)return this.validateNumber(n);throw"Invalid FieldValidationType"},e.prototype.isPopulated=function(e){return void 0!==e&&""!==e},e.prototype.createElement=function(e){return document.createElement(e)},e.prototype.validateText=function(e){return this.isPopulated(e)},e.prototype.validateNumber=function(e){return!isNaN(e)},e}(),o=function(e){function n(){e.apply(this,arguments)}return __extends(n,e),n.prototype.render=function(){var n=this,o=e.prototype.createElement.call(this,"div"),r=e.prototype.createElement.call(this,"label");r.textContent=this.label,o.appendChild(r);var i=e.prototype.createElement.call(this,"input");return this.isPopulated(this.name)&&(i.name=this.name),this.isPopulated(this.id)&&(i.id=this.id),this.isPopulated(this.cls)&&(i.className=this.cls),this.fieldType!==t.none&&i.addEventListener("blur",function(){n.isValid()||-1!==i.className.indexOf(" invalid")?i.className=i.className.replace(" invalid",""):i.className+=" invalid"}),o.appendChild(i),o},n}(n);e.TextField=o;var r=function(e){function n(){e.apply(this,arguments)}return __extends(n,e),n.prototype.render=function(){var n=this,o=e.prototype.createElement.call(this,"div"),r=e.prototype.createElement.call(this,"label");r.textContent=this.label,o.appendChild(r);var i=e.prototype.createElement.call(this,"textarea");return this.isPopulated(this.name)&&(i.name=this.name),this.isPopulated(this.id)&&(i.id=this.id),this.isPopulated(this.cls)&&(i.className=this.cls),this.fieldType!==t.none&&i.addEventListener("blur",function(){n.isValid()||-1!==i.className.indexOf(" invalid")?i.className=i.className.replace(" invalid",""):i.className+=" invalid"}),o.appendChild(i),o},n}(n);e.TextAreaField=r;var i=function(){function e(e,t){this.fields=e,this.buttons=t}return e.prototype.render=function(e,t,n,o){void 0===e&&(e=null),void 0===t&&(t=null),void 0===n&&(n=null),void 0===o&&(o=null);var r=document.createElement("form");return r.className="show",r.method=n,r.action=o,r.name=e,r.id=t,this.fields.forEach(function(e){r.appendChild(e.render())}),this.buttons.forEach(function(e){r.appendChild(e.render())}),r},e.prototype.isValid=function(){return this.fields.forEach(function(e){return e.isValid()!==!0?!1:void 0}),!0},e}();e.Form=i}(Form||(Form={}));var Alert;!function(e){var t=function(){function e(e){this.headerText=e}return e.prototype.render=function(){var e=document.createElement("div");e.className="toolbar";var t=document.createElement("span"),n=document.createTextNode(this.headerText);t.appendChild(n),e.appendChild(t);var o=new Common.Button("X",function(){document.getElementsByClassName("messageBox")[0].remove()}),r=o.render();return r.id="close-button",r.className="button close",e.appendChild(r),e},e}(),n=function(){function e(e){this.buttons=e}return e.prototype.render=function(){var e=document.createElement("div");return e.className="footer",this.buttons.forEach(function(t){e.appendChild(t.render())}),e},e}(),o=function(){function e(e,o,r){this.headerElement=new t(e),this.content=o,this.footerElement=new n(r)}return e.prototype.render=function(){var e=document.createElement("div");return e.className="messageBox",e.appendChild(this.headerElement.render()),e.appendChild(this.content),e.appendChild(this.footerElement.render()),e},e}();e.MessageBox=o}(Alert||(Alert={}));var Application;!function(e){var t=[new Form.TextField(Form.FieldValidationType.none,"IMDB Id","id","","id"),new Form.TextField(Form.FieldValidationType.none,"Title","title","","title"),new Form.TextField(Form.FieldValidationType.none,"Year","year","","year"),new Form.TextField(Form.FieldValidationType.none,"Released","released","","released"),new Form.TextField(Form.FieldValidationType.none,"Runtime","runtime","","runtime"),new Form.TextField(Form.FieldValidationType.none,"Genre","genre","","genre"),new Form.TextField(Form.FieldValidationType.none,"Rated","rated","","rated"),new Form.TextField(Form.FieldValidationType.none,"Director","director","","director"),new Form.TextField(Form.FieldValidationType.none,"Writer","writer","","writer"),new Form.TextField(Form.FieldValidationType.none,"Language","writer","","writer"),new Form.TextField(Form.FieldValidationType.none,"Country","country","","country"),new Form.TextField(Form.FieldValidationType.none,"Location","location","","location"),new Form.TextAreaField(Form.FieldValidationType.none,"Plot","plot","","plot")],n=[new Common.Button("Check",null),new Common.Button("Clear",null),new Common.Button("Submit",null)];window.onload=function(){var e=new Form.Form(t,[]),o=new Alert.MessageBox("Add Movie",e.render(),n),r=document.getElementById("poster");r.appendChild(o.render())}}(Application||(Application={}));var Movie=function(){function e(){}return e}(),Display;!function(e){e[e.Show=0]="Show",e[e.Hide=1]="Hide"}(Display||(Display={}));var Controller=function(){function e(e){this.address=e}return e.prototype.error=function(e){console.log(e)},e}(),SearchController=function(e){function t(t){e.call(this,t)}return __extends(t,e),t}(Controller),MovieController=function(e){function t(t){e.call(this,t)}return __extends(t,e),t.prototype.get=function(e,t){var n=$("#id").val(),o=encodeURIComponent($("#title").val()),r=new Ajax.Request(this.address,e,t);r.get({name:o,id:n})},t.prototype.post=function(e,t,n){console.log(e);var o=new Ajax.Request(this.address,t,n);o.post(e)},t}(Controller),AuthController=function(e){function t(t){e.call(this,t)}return __extends(t,e),t}(Controller),App=function(){function e(e,t,n){searchController=e,authController=n,movieController=t}return e.setImage=function(e){var t=$('<img id="posterImg">');return t.attr("src",e),t},e.canPopulateForm=function(){return""!==$("#id").val()||""!==$("#title").val()},e.populateForm=function(){movieController.get(function(t){$("#id").val(t.Id),$("#title").val(t.Title),$("#year").val(TimeFormatter.formatYear(t.Year)),$("#released").val(TimeFormatter.formatReleaseDate(t.Released)),$("#runtime").val(TimeFormatter.formatRuntime(t.Runtime)),$("#genre").val(t.Genre),$("#rated").val(t.Rated),$("#director").val(t.Director),$("#writer").val(t.Writer),$("#language").val(t.Language),$("#country").val(t.Country),$("#location").val(t.Location),$("#plot").text(t.Plot);var n=e.setImage(t.FullImgUrl),o=$("#poster");o.html(""),n.appendTo(o),$("form img").addClass("hide")},function(e,t,n){movieController.error(e.responseText),movieController.error(t),movieController.error(n),$("form img").addClass("hide")})},e.clearForm=function(){$("#id").val(""),$("#title").val(""),$("#year").val(""),$("#released").val(""),$("#runtime").val(""),$("#genre").val(""),$("#rated").val(""),$("#director").val(""),$("#writer").val(""),$("#language").val(""),$("#country").val(""),$("#location").val(""),$("#plot").text(""),$("#poster").html("")},e.killEvent=function(e){e.stopPropagation(),e.preventDefault()},e.getMovie=function(){var e=new Movie;return e.Id=$("#id").val(),e.Title=$("#title").val(),e.Year=$("#year").val(),e.Released=$("#released").val(),e.Runtime=$("#runtime").val(),e.Genre=$("#genre").val(),e.Rated=$("#rated").val(),e.Director=$("#director").val(),e.Writer=$("#writer").val(),e.Language=$("#language").val(),e.Country=$("#country").val(),e.Location=$("#location").val(),e.Plot=$("#plot").text(),e.FullImgUrl=$("#posterImg").attr("src"),e},e.showForm=function(t,n){e.killEvent(t);var o=$("body > div form"),r="hide";n===Display.Show&&(r="show"),o.removeAttr("class"),o.addClass(r)},e.clear=function(t){$("form img").addClass("hide"),e.killEvent(t),e.clearForm()},e.check=function(t){e.killEvent(t),e.canPopulateForm()?($("form img").addClass("show"),e.populateForm()):alert("Enter an ID or Title")},e.close=function(t){e.clear(t),e.showForm(t,Display.Hide)},e.submit=function(t){e.killEvent(t),$("form img").addClass("show");var n=e.getMovie();movieController.post(n,function(){e.clear(t),e.showForm(t,Display.Hide)},function(e,t,n){movieController.error(e.responseText),movieController.error(t),movieController.error(n),alert("There was a problem with your request: "+n),$("form img").addClass("hide")})},e.prototype.init=function(){$("#add").click(function(t){e.showForm(t,Display.Show)}),$("#check").click(e.check),$("#clear").click(e.clear),$("#close").click(e.close),$("#submit").click(e.submit)},e}(),TimeFormatter=function(){function e(){}return e.formatYear=function(e){if(void 0===e||0===e.length)return"";if(1===e.length)return e[0].toString();var t=e[0].toString(),n=e[e.length-1].toString();return t+"-"+n},e.formatReleaseDate=function(e){if(void 0===e)return"";var t={weekday:"long",year:"numeric",month:"long",day:"numeric"};return new Date(e).toLocaleDateString("en-US",t)},e.formatRuntime=function(e){if(void 0===e)return"";var t=e.split(":");if(3!==t.length)throw"Invalid Runtime: "+e;var n=60*+t[0],o=+t[1],r=+t[2];return r=r>30?0:1,n+o+r+" minutes"},e}(),searchController=new SearchController("/api/search/"),movieController=new MovieController("/api/movies/"),authController=new AuthController("/api/auth/"),movieApp=new App(searchController,movieController,authController);