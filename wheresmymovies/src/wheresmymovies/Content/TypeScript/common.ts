module Common{
    export interface Renderable {
        render(): Element;
    }
    export interface Validatable {
        isValid():boolean;
    }
    
    export class Button implements Renderable{
        
        onclick:EventListener;
        buttonText:string;
        
        constructor(buttonText:string, onclick: EventListener){
            this.onclick = onclick;
            this.buttonText = buttonText;
        }
        
        render(): Element{
            var me = this;
            
            var inputElement = document.createElement('button') as HTMLButtonElement
            inputElement.id = this.buttonText.toLowerCase();
            var textElement = document.createTextNode(this.buttonText);
            inputElement.appendChild(textElement);

            inputElement.addEventListener('click', this.onclick)
            
            return inputElement;
        }
    }
}