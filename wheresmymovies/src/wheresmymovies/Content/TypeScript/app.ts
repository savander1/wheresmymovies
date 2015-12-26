///<reference path="common.ts" />
///<reference path="form.ts" />
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
    
    var buttons = [
        new Common.Button('Check', null),
        new Common.Button('Clear', null),
        new Common.Button('Submit', null),
        
    ];
    
    
 
    window.onload = function(){
        
        var form = new Form.Form(fields, []);
        
        var messageBox = new Alert.MessageBox('Add Movie', new Common.Button('Close', null), form.render(), buttons)
        
        var container = document.getElementById('poster');
        container.appendChild(messageBox.render())
    }
}