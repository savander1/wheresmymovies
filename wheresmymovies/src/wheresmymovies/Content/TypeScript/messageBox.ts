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
    
    class MessageBoxContent implements Common.Renderable{
        private content: HTMLElement;
        
        constructor(content: HTMLElement){
            this.content = content;
        }
        
        render():HTMLElement{
            var contentContaier = document.createElement('div');
            contentContaier.className = 'middle';
            contentContaier.appendChild(this.content);
            return contentContaier;
        }
    }
    
    class MessageBoxFooter implements Common.Renderable{
        private buttons: Common.Button[];
        
        constructor(buttons:Common.Button[]){
            this.buttons = buttons;
        }
        
        render():Element{
            var footerContent = document.getElementsByClassName('footer')[0];
            if (footerContent === void 0)
            { 
                footerContent = document.createElement('div'); 
            }
            footerContent.className = 'footer';
            
            this.buttons.forEach(button => {
                footerContent.appendChild(button.render());
            });
            
            return footerContent;
        }
    }
    
    
    
    export class MessageBox implements Common.Renderable{
        
        headerElement: MessageBoxHeader;
        contentElement: MessageBoxContent;
        footerElement: MessageBoxFooter;
        
        constructor(header: string, content: HTMLElement, buttons:Common.Button[]){
            this.headerElement = new MessageBoxHeader(header);
            this.contentElement = new MessageBoxContent(content);
            this.footerElement = new MessageBoxFooter(buttons);
        }
        
        render():Element{
            var container = document.createElement('div');
            container.className = 'messageBox';
            
            container.appendChild(this.headerElement.render());
            container.appendChild(this.contentElement.render());
            container.appendChild(this.footerElement.render());
            
            return container;
        }
        
        replaceContent(replacement:HTMLElement):void{
            this.contentElement = new MessageBoxContent(replacement);
            var container = document.getElementsByClassName('messageBox')[0];
            
            var oldContent = container.getElementsByClassName['middle'][0];
            container.replaceChild(this.contentElement.render(), oldContent);
        }
        
        replaceButtons(buttons:Common.Button[]):void{
            this.footerElement = new MessageBoxFooter(buttons);
            this.render();
        }
        
        close():void{
            document.getElementsByClassName('messageBox')[0].remove();
        }
    }
}