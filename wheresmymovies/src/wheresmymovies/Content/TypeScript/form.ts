module Form {
    
    export interface Renderable {
        render(): Element;
    }
    export interface Validatable {
        isValid():boolean;
    }
    
    export enum FieldValidationType {
        'text',
        'number',
        'textbox',
        'none'
    }
    
    abstract class Field implements Renderable, Validatable{
        
        protected invalidClass : string = ' invalid';
        
        fieldType: FieldValidationType;
        label: string;
        cls : string;
        name : string;
        id : string;
        
        constructor(type: FieldValidationType, label: string, cls: string, name: string = '', id : string = ''){ 
            this.fieldType = type;
            this.label = label;
            this.cls = cls;
            this.name = name;
            
            if(id === ''){
                id = type + '_' + Math.floor((Math.random() * 10) + 1);
            }
            this.id = id;
        }
        
        render(): Element{
            return void 0;
        }
        
        isValid():boolean {
            if (this.fieldType === FieldValidationType.none){
                return true;
            }
            var elem = document.getElementById(this.id) as HTMLInputElement;
            var value = elem.value;
            
            if (this.fieldType === FieldValidationType.text || this.fieldType === FieldValidationType.textbox){
                return this.validateText(value)
            }
            
            if (this.fieldType === FieldValidationType.number){
                return this.validateNumber(value);
            }
            
            throw 'Invalid FieldValidationType';
        }
        
        isPopulated(value: any): boolean{
            return value !== void 0 && value !== ''
        }
        
        createElement(tagName:string): HTMLElement {
            return document.createElement(tagName);
        }
        
        private validateText(text:string):boolean{
            return this.isPopulated(text);
        }
        
        private validateNumber(text:any) : boolean{
            return !isNaN(text);
        }
    }
    
    export class TextField  extends Field{
        
        render(): Element{
            var me = this;
            
            var containerElement = super.createElement('div');
            
            var labelElement = super.createElement('label')
            labelElement.textContent = this.label;
            
            containerElement.appendChild(labelElement);
            
            var inputElement = super.createElement('input') as HTMLInputElement
            
            if (this.isPopulated(this.name)){
                inputElement.name = this.name;
            }
            
            if (this.isPopulated(this.id)){
                inputElement.id = this.id;
            }
            
            if (this.isPopulated(this.cls)){
                inputElement.className = this.cls;
            }
            
            if (this.fieldType !== FieldValidationType.none){
               inputElement.addEventListener('blur',  function(){
                   if (!me.isValid() && inputElement.className.indexOf(' invalid') === -1){
                       inputElement.className += ' invalid';
                   } else {
                       inputElement.className = inputElement.className.replace(' invalid', '');
                   }
               });
            }
            
            containerElement.appendChild(inputElement);
            
            return containerElement;
        }
    }
    
    export class TextAreaField extends Field{
        render(): Element{
            var me = this;
            
            var containerElement = super.createElement('div');
            
            var labelElement = super.createElement('label')
            labelElement.textContent = this.label;
            
            containerElement.appendChild(labelElement);
            
            var inputElement = super.createElement('textarea') as HTMLTextAreaElement
            
            if (this.isPopulated(this.name)){
                inputElement.name = this.name;
            }
            
            if (this.isPopulated(this.id)){
                inputElement.id = this.id;
            }
            
            if (this.isPopulated(this.cls)){
                inputElement.className = this.cls;
            }
            
            if (this.fieldType !== FieldValidationType.none){
               inputElement.addEventListener('blur',  function(){
                   if (!me.isValid() && inputElement.className.indexOf(' invalid') === -1){
                       inputElement.className += ' invalid';
                   } else {
                       inputElement.className = inputElement.className.replace(' invalid', '');
                   }
               });
            }
            
            containerElement.appendChild(inputElement);
            
            return containerElement;
        }
    }
    
    export class Button implements Renderable{
        
        onclick:EventListenerObject;
        buttonText:string;
        
        constructor(buttonText:string, onclick: EventListenerObject){
            this.onclick = onclick;
            this.buttonText = buttonText;
        }
        
        render(): Element{
            var me = this;
            
            var inputElement = document.createElement('button') as HTMLButtonElement
            inputElement.addEventListener('click', onclick)
            
            return inputElement;
        }
    }
    
    export class RenderableForm implements Validatable{
        fields: Field[];
        buttons: Button[];
        rootElement: HTMLElement;
        
        constructor(root:string, fields: Field[], buttons: Button[]){
            this.fields = fields;
            this.buttons = buttons;
            this.rootElement = document.getElementById(root);
        }
        
        render(name: string = null, id:string = null, method:string = null, action:string = null): void{
            var formElement = document.createElement('form')
            
            formElement.method = method;
            formElement.action = action;
            formElement.name = name;
            formElement.id = id;
            
            this.fields.forEach(element => {
                formElement.appendChild(element.render());
            });
            
            this.rootElement.appendChild(formElement);
        }
        
        isValid() :boolean{
            this.fields.forEach(element => {
                if (element.isValid() !== true){
                    return false;
                }
            });
            return true;
        }
    }
}

module Test {
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
        new Form.Button('Check', null),
        new Form.Button('Clear', null),
        new Form.Button('Submit', null),
        new Form.Button('Close', null)
    ];
    
    window.onload = function (){
        var form = new Form.RenderableForm('poster', fields, buttons);
    
    form.render();
    }
}