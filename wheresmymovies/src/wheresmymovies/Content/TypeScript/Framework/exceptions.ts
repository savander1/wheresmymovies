module Exceptions {
    export class PropertyNotFoundException{
        
        public message: string;
        public name: string;
        
        public constructor(propertyName: string){
            this.message = 'Property ' + propertyName + ' not found';
            this.name = 'PropertyNotFound';
        }
    }
}