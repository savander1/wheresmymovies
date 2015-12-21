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
    
    abstract class Field{
        
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
        
        
        
        isPopulated(value: any): boolean{
            return value !== void 0 && value !== ''
        }
        
    }
    
    class TextField  extends Field implements Renderable, Validatable{
        
        
        render(): Element{
            var containerElement = new HTMLDivElement();
            
            var labelElement = new HTMLLabelElement();
            labelElement.textContent = this.label;
            
            containerElement.appendChild(labelElement);
            
            var inputElement = new HTMLInputElement();
            
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
               inputElement.onblur = function(){
                   if (!this.isValid(inputElement.value)){
                       inputElement.className += ' nvalid';
                   } else {
                       inputElement.className = inputElement.className.replace(' invalid', '');
                   }
               };
            }
            
            containerElement.appendChild(inputElement);
            
            return containerElement;
        }
        
        isValid():boolean {
            var elem = document.getElementById(this.id) as HTMLInputElement;
            var value = elem.value;
            
            return this.isPopulated(value);
            
        }
    }
    
    export class AddModvieForm  {
        
        
    } 
}