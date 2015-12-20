module Form {
    enum FieldType {
        'text',
        'number',
        'textbox'
    }
    
    abstract class Field{
        
        fieldType: FieldType;
        label: string;
        cls : string;
        
        constructor(type: FieldType, label: string, cls: string){ 
            this.fieldType = type;
            this.label = label;
            this.cls = cls;
        }
        
        abstract isValid(value: any) : boolean;
    }
    
    class TextFile : extends Field {
        
        isValid(value: any){
            
        }
    }
    
    export class AddModvieForm  {
        
        
    } 
}