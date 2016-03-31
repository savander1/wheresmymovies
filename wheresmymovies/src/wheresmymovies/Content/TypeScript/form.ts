///<reference path="common.ts" />
///<reference path="Models/model.ts" />

module Form {
    export enum FieldValidationType {
        'text',
        'number',
        'textbox',
        'none'
    }
    
    export abstract class Field<T> implements Common.Renderable, Common.Validatable, ViewModel.PropertyObserver<T>{
        
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
        
        updateValue(value: any){
            var elem = document.getElementById(this.id) as HTMLInputElement;
            elem.value = value;
        }
        
        onChange(){
            
        }
        
        private validateText(text:string):boolean{
            return this.isPopulated(text);
        }
        
        private validateNumber(text:any) : boolean{
            return !isNaN(text);
        }
    }
    
    export class TextField<T>  extends Field<T>{
        
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
    
    export class TextAreaField<T> extends Field<T>{
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
        private _model: Models.ViewModel;
        private _buttons: Common.Button[];
        private _rootElement: HTMLElement;
        
        constructor(model: Models.ViewModel, buttons: Common.Button[]){
            this._model = model;
            this._buttons = buttons;
        }
        
        render(name: string = null, id:string = null, method:string = null, action:string = null): HTMLElement{
            var formElement = document.createElement('form')
            formElement.className = 'show';
            formElement.method = method;
            formElement.action = action;
            formElement.name = name;
            formElement.id = id;
            
            this._model.getHtmlElements().forEach(element => {
                formElement.appendChild(element);
            });
            
            this._buttons.forEach(button => {
                formElement.appendChild(button.render());
            });
            
            return formElement;
        }
        
        isValid() :boolean{
            return this._model.isValid();
        }
        
        getValues(): Models.ViewModel{
            return this._model;
        }
    }
}

