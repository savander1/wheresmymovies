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
            
            return inputElement;
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