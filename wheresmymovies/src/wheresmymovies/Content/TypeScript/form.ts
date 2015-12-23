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
    
    abstract class Field implements Renderable{
        
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
        
        isPopulated(value: any): boolean{
            return value !== void 0 && value !== ''
        }
        
        createElement(tagName:string): HTMLElement {
            return document.createElement(tagName);
        }
    }
    
    class TextField  extends Field implements Renderable, Validatable{
        
        
        public render(): Element{
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
        
        public isValid():boolean {
            var elem = document.getElementById(this.id) as HTMLInputElement;
            var value = elem.value;
            
            return this.isPopulated(value);
            
        }
    }
    
    export class AddModvieForm {
        fields: Field[];
        rootElement: HTMLElement;
        
        constructor(root:string){
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
    var form = new Form.AddModvieForm('boo');
    form.render();
};