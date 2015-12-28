///<reference path="common.ts" />

module Alert {
    
    class MessageBoxHeader implements Common.Renderable{
 
        private headerText: string;
        
        constructor(headerText:string){
            this.headerText = headerText;
        }
        
        
        render(): Element {
            var headerContainer = document.createElement('div');
            headerContainer.className = 'toolbar';
            var textContainer = document.createElement('span');
            var headerText = document.createTextNode(this.headerText);
            textContainer.appendChild(headerText);
            headerContainer.appendChild(textContainer);
            
            var closeButton = new Common.Button('X', function(){
                document.getElementsByClassName('messageBox')[0].remove();
            });        
            var button = closeButton.render();
            button.id = 'close-button';
            button.className = 'button close';
            headerContainer.appendChild(button);
        
            return headerContainer;
        }
    }
    
    class MessageBoxFooter implements Common.Renderable{
        private buttons: Common.Button[];
        
        constructor(buttons:Common.Button[]){
            this.buttons = buttons;
        }
        
        render():HTMLElement{
            var footerContent = document.createElement('div');
            footerContent.className = 'footer';
            
            this.buttons.forEach(button => {
                footerContent.appendChild(button.render());
            });
            
            return footerContent;
        }
    }
    
    export class MessageBox implements Common.Renderable{
        
        headerElement: MessageBoxHeader;
        content: HTMLElement; 
        footerElement: MessageBoxFooter;
        
        constructor(header: string, content: HTMLElement, buttons:Common.Button[]){
            this.headerElement = new MessageBoxHeader(header);
            this.content = content;
            this.footerElement = new MessageBoxFooter(buttons);
        }
        
        render():Element{
            var container = document.createElement('div');
            container.className = 'messageBox';
            
            container.appendChild(this.headerElement.render());
            container.appendChild(this.content);
            container.appendChild(this.footerElement.render());
            
            return container;
        }
    }
}