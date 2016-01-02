///<reference path="common.ts" />
///<reference path="form.ts" />
///<reference path="controller.ts" />
///<reference path="messageBox.ts" />

module Application {
    var fields = [
        new Form.TextField(Form.FieldValidationType.none, 'IMDB Id', 'id', '' , 'id' ),
        new Form.TextField(Form.FieldValidationType.none, 'Title', 'title', '' , 'title' ),
        new Form.TextField(Form.FieldValidationType.none, 'Year', 'year', '' , 'year' ),
        new Form.TextField(Form.FieldValidationType.none, 'Released', 'released', '' , 'released' ),
        new Form.TextField(Form.FieldValidationType.none, 'Runtime', 'runtime', '' , 'runtime' ),
        new Form.TextField(Form.FieldValidationType.none, 'Genre', 'genre', '' , 'genre' ),
        
        new Form.TextField(Form.FieldValidationType.none, 'Rated', 'rated', '' , 'rated' ),
        new Form.TextField(Form.FieldValidationType.none, 'Director', 'director', '' , 'director' ),
        new Form.TextField(Form.FieldValidationType.none, 'Writer', 'writer', '' , 'writer' ),
        
        new Form.TextField(Form.FieldValidationType.none, 'Language', 'writer', '' , 'writer' ),
        new Form.TextField(Form.FieldValidationType.none, 'Country', 'country', '' , 'country' ),
        
        new Form.TextField(Form.FieldValidationType.none, 'Location', 'location', '' , 'location' ),
        
        new Form.TextAreaField(Form.FieldValidationType.none, 'Plot', 'plot', '' , 'plot' )
    ];
    
    var addButtons = [
        new Common.Button('Clear', null),
        new Common.Button('Submit', null)
    ];
    
    
    var movieForm = new Form.Form(fields, []);
    var checkForm = new Form.Form(fields.slice(0,2), []);
    
    var movieController = new Controllers.MovieController('/api/movies/');
    var messageBox = new Alert.MessageBox('Add Movie', checkForm.render(), [])
    
 
    var searchButtonClick:EventListener = function(event) {
        movieController.get(checkForm.getValues(), messageBox.close, function(resp){
            movieController.error(resp);
            messageBox.replaceButtons(addButtons);
            messageBox.replaceContent(movieForm.render());
        });
    };

    var searchButtons = [
        new Common.Button('Clear', null), 
        new Common.Button('Search', searchButtonClick)
    ];
    
    
    window.onload = function(){
        
        messageBox.replaceButtons(searchButtons);
        
        var container = document.getElementById('poster');
        container.appendChild(messageBox.render());
    }
    
    
}