///<reference path="common.ts" />

module Alert {
    
    class MessageBoxHeader implements Common.Renderable{
        
        private closeButton: Common.Button;
        private headerText: string;
        
        constructor(headerText:string, closeButton: Common.Button){
            this.closeButton = closeButton;
            this.headerText = headerText;
        }
        
        
        render(): Element {
            var headerContainer = document.createElement('div');
            var headerText = document.createTextNode(this.headerText);
            headerContainer.appendChild(headerText);
            if (this.closeButton !== void 0){
                var button = this.closeButton.render();
                headerContainer.appendChild(button);
            }
            
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
        
        constructor(header: string, headerCloseButton:Common.Button, content: HTMLElement, buttons:Common.Button[]){
            this.headerElement = new MessageBoxHeader(header, headerCloseButton);
            this.content = content;
            this.footerElement = new MessageBoxFooter(buttons);
        }
        
        render():Element{
            var container = document.createElement('div');
            
            container.appendChild(this.headerElement.render());
            container.appendChild(this.content);
            container.appendChild(this.footerElement.render());
            
            return container;
        }
    }
}