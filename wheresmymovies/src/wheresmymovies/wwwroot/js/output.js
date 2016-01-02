var Ajax;
(function (Ajax) {
    var Request = (function () {
        function Request(endPoint, onSuccess, onFailure) {
            this.endPoint = endPoint;
            this.onSuccess = onSuccess;
            this.onFailure = onFailure;
        }
        Request.prototype.get = function (data) {
            this.makeRequest(data, 'GET');
        };
        Request.prototype.post = function (data) {
            this.makeRequest(data, 'POST');
        };
        Request.prototype.makeRequest = function (data, type) {
            var me = this;
            var request = new XMLHttpRequest();
            request.open(type, this.endPoint, true);
            request.onreadystatechange = function () {
                if (request.readyState != 4 || request.status != 200) {
                    me.onFailure(request);
                }
                me.onSuccess(request);
            };
            request.send(JSON.stringify(data));
        };
        return Request;
    })();
    Ajax.Request = Request;
})(Ajax || (Ajax = {}));
var Common;
(function (Common) {
    var Button = (function () {
        function Button(buttonText, onclick) {
            this.onclick = onclick;
            this.buttonText = buttonText;
        }
        Button.prototype.render = function () {
            var me = this;
            var inputElement = document.createElement('button');
            inputElement.id = this.buttonText.toLowerCase();
            inputElement.className = 'button';
            var textElement = document.createTextNode(this.buttonText);
            inputElement.appendChild(textElement);
            if (this.onclick !== void 0) {
                inputElement.addEventListener('click', this.onclick);
            }
            return inputElement;
        };
        Button.prototype.addOnClickEvent = function (onclick) {
            this.onclick = onclick;
            var elm = document.getElementById(this.buttonText.toLowerCase());
            elm.addEventListener('click', onclick);
        };
        return Button;
    })();
    Common.Button = Button;
})(Common || (Common = {}));
///<reference path="common.ts" />
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Form;
(function (Form_1) {
    (function (FieldValidationType) {
        FieldValidationType[FieldValidationType['text'] = 0] = 'text';
        FieldValidationType[FieldValidationType['number'] = 1] = 'number';
        FieldValidationType[FieldValidationType['textbox'] = 2] = 'textbox';
        FieldValidationType[FieldValidationType['none'] = 3] = 'none';
    })(Form_1.FieldValidationType || (Form_1.FieldValidationType = {}));
    var FieldValidationType = Form_1.FieldValidationType;
    var Field = (function () {
        function Field(type, label, cls, name, id) {
            if (name === void 0) { name = ''; }
            if (id === void 0) { id = ''; }
            this.invalidClass = ' invalid';
            this.fieldType = type;
            this.label = label;
            this.cls = cls;
            this.name = name;
            if (id === '') {
                id = type + '_' + Math.floor((Math.random() * 10) + 1);
            }
            this.id = id;
        }
        Field.prototype.render = function () {
            return void 0;
        };
        Field.prototype.isValid = function () {
            if (this.fieldType === FieldValidationType.none) {
                return true;
            }
            var elem = document.getElementById(this.id);
            var value = elem.value;
            if (this.fieldType === FieldValidationType.text || this.fieldType === FieldValidationType.textbox) {
                return this.validateText(value);
            }
            if (this.fieldType === FieldValidationType.number) {
                return this.validateNumber(value);
            }
            throw 'Invalid FieldValidationType';
        };
        Field.prototype.isPopulated = function (value) {
            return value !== void 0 && value !== '';
        };
        Field.prototype.createElement = function (tagName) {
            return document.createElement(tagName);
        };
        Field.prototype.validateText = function (text) {
            return this.isPopulated(text);
        };
        Field.prototype.validateNumber = function (text) {
            return !isNaN(text);
        };
        return Field;
    })();
    var TextField = (function (_super) {
        __extends(TextField, _super);
        function TextField() {
            _super.apply(this, arguments);
        }
        TextField.prototype.render = function () {
            var me = this;
            var containerElement = _super.prototype.createElement.call(this, 'div');
            var labelElement = _super.prototype.createElement.call(this, 'label');
            labelElement.textContent = this.label;
            containerElement.appendChild(labelElement);
            var inputElement = _super.prototype.createElement.call(this, 'input');
            if (this.isPopulated(this.name)) {
                inputElement.name = this.name;
            }
            if (this.isPopulated(this.id)) {
                inputElement.id = this.id;
            }
            if (this.isPopulated(this.cls)) {
                inputElement.className = this.cls;
            }
            if (this.fieldType !== FieldValidationType.none) {
                inputElement.addEventListener('blur', function () {
                    if (!me.isValid() && inputElement.className.indexOf(' invalid') === -1) {
                        inputElement.className += ' invalid';
                    }
                    else {
                        inputElement.className = inputElement.className.replace(' invalid', '');
                    }
                });
            }
            containerElement.appendChild(inputElement);
            return containerElement;
        };
        return TextField;
    })(Field);
    Form_1.TextField = TextField;
    var TextAreaField = (function (_super) {
        __extends(TextAreaField, _super);
        function TextAreaField() {
            _super.apply(this, arguments);
        }
        TextAreaField.prototype.render = function () {
            var me = this;
            var containerElement = _super.prototype.createElement.call(this, 'div');
            var labelElement = _super.prototype.createElement.call(this, 'label');
            labelElement.textContent = this.label;
            containerElement.appendChild(labelElement);
            var inputElement = _super.prototype.createElement.call(this, 'textarea');
            if (this.isPopulated(this.name)) {
                inputElement.name = this.name;
            }
            if (this.isPopulated(this.id)) {
                inputElement.id = this.id;
            }
            if (this.isPopulated(this.cls)) {
                inputElement.className = this.cls;
            }
            if (this.fieldType !== FieldValidationType.none) {
                inputElement.addEventListener('blur', function () {
                    if (!me.isValid() && inputElement.className.indexOf(' invalid') === -1) {
                        inputElement.className += ' invalid';
                    }
                    else {
                        inputElement.className = inputElement.className.replace(' invalid', '');
                    }
                });
            }
            containerElement.appendChild(inputElement);
            return containerElement;
        };
        return TextAreaField;
    })(Field);
    Form_1.TextAreaField = TextAreaField;
    var Form = (function () {
        function Form(fields, buttons) {
            this.fields = fields;
            this.buttons = buttons;
        }
        Form.prototype.render = function (name, id, method, action) {
            if (name === void 0) { name = null; }
            if (id === void 0) { id = null; }
            if (method === void 0) { method = null; }
            if (action === void 0) { action = null; }
            var formElement = document.createElement('form');
            formElement.className = 'show';
            formElement.method = method;
            formElement.action = action;
            formElement.name = name;
            formElement.id = id;
            this.fields.forEach(function (element) {
                formElement.appendChild(element.render());
            });
            this.buttons.forEach(function (button) {
                formElement.appendChild(button.render());
            });
            return formElement;
        };
        Form.prototype.isValid = function () {
            this.fields.forEach(function (element) {
                if (element.isValid() !== true) {
                    return false;
                }
            });
            return true;
        };
        Form.prototype.getValues = function () {
            var values = ',';
            this.fields.forEach(function (field) {
                var key = field.id;
                var elm = document.getElementById(field.id);
                var value = elm.value;
                var pair = '' + key + ':' + encodeURIComponent(value) + ',';
                value += pair;
            });
            values = values.substr(0, values.length - 1);
            return JSON.parse('{' + values + '}');
        };
        return Form;
    })();
    Form_1.Form = Form;
})(Form || (Form = {}));
var Models;
(function (Models) {
    var Movie = (function () {
        function Movie() {
        }
        return Movie;
    })();
    Models.Movie = Movie;
    var MovieSearchCriteria = (function () {
        function MovieSearchCriteria() {
        }
        return MovieSearchCriteria;
    })();
    Models.MovieSearchCriteria = MovieSearchCriteria;
})(Models || (Models = {}));
///<reference path="common.ts" />
///<reference path="model.ts" />
///<reference path="ajax.ts" />
var Controllers;
(function (Controllers) {
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
        MovieController.prototype.get = function (idName, success, failure) {
            var movieRequest = new Ajax.Request(this.address, success, failure);
            movieRequest.get(idName);
        };
        MovieController.prototype.post = function (movie, success, failure) {
            console.log(movie);
            var movieRequests = new Ajax.Request(this.address, success, failure);
            movieRequests.post(movie);
        };
        return MovieController;
    })(Controller);
    Controllers.MovieController = MovieController;
    var AuthController = (function (_super) {
        __extends(AuthController, _super);
        function AuthController(address) {
            _super.call(this, address);
        }
        return AuthController;
    })(Controller);
})(Controllers || (Controllers = {}));
///<reference path="common.ts" />
var Alert;
(function (Alert) {
    var MessageBoxHeader = (function () {
        function MessageBoxHeader(headerText) {
            this.headerText = headerText;
        }
        MessageBoxHeader.prototype.render = function () {
            var headerContainer = document.createElement('div');
            headerContainer.className = 'toolbar';
            var textContainer = document.createElement('span');
            var headerText = document.createTextNode(this.headerText);
            textContainer.appendChild(headerText);
            headerContainer.appendChild(textContainer);
            var closeButton = new Common.Button('X', function () {
                document.getElementsByClassName('messageBox')[0].remove();
            });
            var button = closeButton.render();
            button.id = 'close-button';
            button.className = 'button close';
            headerContainer.appendChild(button);
            return headerContainer;
        };
        return MessageBoxHeader;
    })();
    var MessageBoxContent = (function () {
        function MessageBoxContent(content) {
            this.content = content;
        }
        MessageBoxContent.prototype.render = function () {
            var contentContaier = document.createElement('div');
            contentContaier.className = 'middle';
            contentContaier.appendChild(this.content);
            return contentContaier;
        };
        return MessageBoxContent;
    })();
    var MessageBoxFooter = (function () {
        function MessageBoxFooter(buttons) {
            this.buttons = buttons;
        }
        MessageBoxFooter.prototype.render = function () {
            var footerContent = document.getElementsByClassName('footer')[0];
            if (footerContent === void 0) {
                footerContent = document.createElement('div');
            }
            footerContent.className = 'footer';
            this.buttons.forEach(function (button) {
                footerContent.appendChild(button.render());
            });
            return footerContent;
        };
        return MessageBoxFooter;
    })();
    var MessageBox = (function () {
        function MessageBox(header, content, buttons) {
            this.headerElement = new MessageBoxHeader(header);
            this.contentElement = new MessageBoxContent(content);
            this.footerElement = new MessageBoxFooter(buttons);
        }
        MessageBox.prototype.render = function () {
            var container = document.createElement('div');
            container.className = 'messageBox';
            container.appendChild(this.headerElement.render());
            container.appendChild(this.contentElement.render());
            container.appendChild(this.footerElement.render());
            return container;
        };
        MessageBox.prototype.replaceContent = function (replacement) {
            this.contentElement = new MessageBoxContent(replacement);
            var container = document.getElementsByClassName('messageBox')[0];
            var oldContent = container.getElementsByClassName['middle'][0];
            container.replaceChild(this.contentElement.render(), oldContent);
        };
        MessageBox.prototype.replaceButtons = function (buttons) {
            this.footerElement = new MessageBoxFooter(buttons);
            this.render();
        };
        MessageBox.prototype.close = function () {
            document.getElementsByClassName('messageBox')[0].remove();
        };
        return MessageBox;
    })();
    Alert.MessageBox = MessageBox;
})(Alert || (Alert = {}));
///<reference path="common.ts" />
///<reference path="form.ts" />
///<reference path="controller.ts" />
///<reference path="messageBox.ts" />
var Application;
(function (Application) {
    var fields = [
        new Form.TextField(Form.FieldValidationType.none, 'IMDB Id', 'id', '', 'id'),
        new Form.TextField(Form.FieldValidationType.none, 'Title', 'title', '', 'title'),
        new Form.TextField(Form.FieldValidationType.none, 'Year', 'year', '', 'year'),
        new Form.TextField(Form.FieldValidationType.none, 'Released', 'released', '', 'released'),
        new Form.TextField(Form.FieldValidationType.none, 'Runtime', 'runtime', '', 'runtime'),
        new Form.TextField(Form.FieldValidationType.none, 'Genre', 'genre', '', 'genre'),
        new Form.TextField(Form.FieldValidationType.none, 'Rated', 'rated', '', 'rated'),
        new Form.TextField(Form.FieldValidationType.none, 'Director', 'director', '', 'director'),
        new Form.TextField(Form.FieldValidationType.none, 'Writer', 'writer', '', 'writer'),
        new Form.TextField(Form.FieldValidationType.none, 'Language', 'writer', '', 'writer'),
        new Form.TextField(Form.FieldValidationType.none, 'Country', 'country', '', 'country'),
        new Form.TextField(Form.FieldValidationType.none, 'Location', 'location', '', 'location'),
        new Form.TextAreaField(Form.FieldValidationType.none, 'Plot', 'plot', '', 'plot')
    ];
    var addButtons = [
        new Common.Button('Clear', null),
        new Common.Button('Submit', null)
    ];
    var movieForm = new Form.Form(fields, []);
    var checkForm = new Form.Form(fields.slice(0, 2), []);
    var movieController = new Controllers.MovieController('/api/movies/');
    var messageBox = new Alert.MessageBox('Add Movie', checkForm.render(), []);
    var searchButtonClick = function (event) {
        movieController.get(checkForm.getValues(), messageBox.close, function (resp) {
            movieController.error(resp);
            messageBox.replaceButtons(addButtons);
            messageBox.replaceContent(movieForm.render());
        });
    };
    var searchButtons = [
        new Common.Button('Clear', null),
        new Common.Button('Search', searchButtonClick)
    ];
    window.onload = function () {
        messageBox.replaceButtons(searchButtons);
        var container = document.getElementById('poster');
        container.appendChild(messageBox.render());
    };
})(Application || (Application = {}));
///<reference path="common.ts"/>
///<reference path="ajax.ts"/>
///<reference path="form.ts" />
///<reference path="jquery.d.ts"/>
var Movie = (function () {
    function Movie() {
    }
    return Movie;
})();
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
        var name = encodeURIComponent($('#title').val());
        var movieRequest = new Ajax.Request(this.address, success, failure);
        movieRequest.get({ name: name, id: id });
    };
    MovieController.prototype.post = function (movie, success, failure) {
        console.log(movie);
        var movieRequests = new Ajax.Request(this.address, success, failure);
        movieRequests.post(movie);
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
var App = (function () {
    function App(searchControllerAddress, movieControllerAdderess, authControllerAddress) {
        searchController = searchControllerAddress;
        authController = authControllerAddress;
        movieController = movieControllerAdderess;
    }
    App.setImage = function (src) {
        var img = $('<img id="posterImg">');
        img.attr('src', src);
        return img;
    };
    App.canPopulateForm = function () {
        return $('#id').val() !== '' || $('#title').val() !== '';
    };
    App.populateForm = function () {
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
            var thumb = App.setImage(data.FullImgUrl);
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
    App.clearForm = function () {
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
    App.killEvent = function (event) {
        event.stopPropagation();
        event.preventDefault();
    };
    App.getMovie = function () {
        var data = new Movie();
        data.Id = $('#id').val();
        data.Title = $('#title').val();
        data.Year = $('#year').val();
        data.Released = $('#released').val();
        data.Runtime = $('#runtime').val();
        data.Genre = $('#genre').val();
        data.Rated = $('#rated').val();
        data.Director = $('#director').val();
        data.Writer = $('#writer').val();
        data.Language = $('#language').val();
        data.Country = $('#country').val();
        data.Location = $('#location').val();
        data.Plot = $('#plot').text();
        data.FullImgUrl = $('#posterImg').attr('src');
        return data;
    };
    App.showForm = function (event, display) {
        App.killEvent(event);
        var form = $('body > div form');
        var disp = 'hide';
        if (display === Display.Show) {
            disp = 'show';
        }
        form.removeAttr('class');
        form.addClass(disp);
    };
    App.clear = function (event) {
        $('form img').addClass('hide');
        App.killEvent(event);
        App.clearForm();
    };
    App.check = function (event) {
        App.killEvent(event);
        if (App.canPopulateForm()) {
            $('form img').addClass('show');
            App.populateForm();
        }
        else {
            alert('Enter an ID or Title');
        }
    };
    App.close = function (event) {
        App.clear(event);
        App.showForm(event, Display.Hide);
    };
    App.submit = function (event) {
        App.killEvent(event);
        $('form img').addClass('show');
        var movie = App.getMovie();
        movieController.post(movie, function () {
            App.clear(event);
            App.showForm(event, Display.Hide);
        }, function (jqXHr, textStatus, errorThrown) {
            movieController.error(jqXHr.responseText);
            movieController.error(textStatus);
            movieController.error(errorThrown);
            alert('There was a problem with your request: ' + errorThrown);
            $('form img').addClass('hide');
        });
    };
    App.prototype.init = function () {
        $('#add').click(function (event) {
            App.showForm(event, Display.Show);
        });
        $('#check').click(App.check);
        $('#clear').click(App.clear);
        $('#close').click(App.close);
        $('#submit').click(App.submit);
    };
    return App;
})();
var TimeFormatter = (function () {
    function TimeFormatter() {
    }
    TimeFormatter.formatYear = function (year) {
        if (year === void 0 || year.length === 0) {
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
var movieApp = new App(searchController, movieController, authController);
//$('document').ready(movieApp.init);
