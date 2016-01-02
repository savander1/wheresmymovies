///<reference path="common.ts" />

module Form {
    export enum FieldValidationType {
        'text',
        'number',
        'textbox',
        'none'
    }
    
    abstract class Field implements Common.Renderable, Common.Validatable{
        
        protected invalidClass : string = ' invalid';
        
        fieldType: FieldValidationType;
        label: string;
        cls : string;
        name : string;
        id : string;
        value: string;
        
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
    
    
    
    export class Form implements Common.Validatable, Common.Renderable{
        fields: Field[];
        buttons: Common.Button[];
        rootElement: HTMLElement;
        
        constructor(fields: Field[], buttons: Common.Button[]){
            this.fields = fields;
            this.buttons = buttons;
        }
        
        render(name: string = null, id:string = null, method:string = null, action:string = null): HTMLElement{
            var formElement = document.createElement('form')
            formElement.className = 'show';
            formElement.method = method;
            formElement.action = action;
            formElement.name = name;
            formElement.id = id;
            
            this.fields.forEach(element => {
                formElement.appendChild(element.render());
            });
            
            this.buttons.forEach(button => {
                formElement.appendChild(button.render());
            });
            
            return formElement;
        }
        
        isValid() :boolean{
            this.fields.forEach(element => {
                if (element.isValid() !== true){
                    return false;
                }
            });
            return true;
        }
        
        getValues():any{
            
            var values = '';
            
            this.fields.forEach(field => {
                var key = field.id;
                var elm = document.getElementById(field.id) as HTMLInputElement;
                var value = elm.value;
                
                var pair = '"' + key + '":"' + encodeURIComponent(value) + '",';
                values += pair
            });
            
            values = values.substr(0, values.length - 1);
            
            return JSON.parse('{' + values + '}');
        }
    }
}

