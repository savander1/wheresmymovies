module Form {
    
    export interface Renderable {
        render(): Element;
    }
    export interface Validatable {
        isValid():boolean;
    }
    
    enum FieldValidationType {
        'text',
        'number',
        'textbox',
        'none'
    }
    
    abstract class Field implements Renderable, Validatable{
        
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
                   if (!me.isValid()){
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
                   if (!me.isValid()){
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
    
    export class AddMovieForm {
        fields: Field[];
        rootElement: HTMLElement;
        
        constructor(root:string, fields: Field[]){
            this.fields = [
                new TextField(FieldValidationType.text, 'Title', 'form', 'title')
            ];
            this.rootElement = document.getElementById(root);
        }
        
        render(): void{
            var formElement = document.createElement('form')
            
            formElement.method = 'POST';
            
            this.fields.forEach(element => {
                formElement.appendChild(element.render());
            });
            
            this.rootElement.appendChild(formElement);
        }
    } 
}

window.onload = function(){
    var form = new Form.AddMovieForm('boo', null);
    form.render();
};