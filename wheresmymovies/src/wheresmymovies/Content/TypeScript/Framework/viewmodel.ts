///<reference path="exceptions.ts" />

namespace ViewModel {
    
    export interface PropertyObserver {
        updateValue (value:any);
    }

    export class ObservableProperty<T> { 
        private observers : PropertyObserver [];
        private _propertyValue: T;

        constructor(value: T) {
            this.observers = [];
            this.propertyValue = value;
        }
        
        public get propertyValue(): T{
            return this._propertyValue;
        }
        
        public set propertyValue(value: T){
            this._propertyValue = value;
            this.notifyObservers(value);
        }

        registerObserver (observer : PropertyObserver) : void {
            this.observers.push(observer);
        }

        removeObserver (observer : PropertyObserver) : void {
            this.observers.splice(this.observers.indexOf(observer), 1);
        }

        notifyObservers (arg : any) : void {

            this.observers.forEach((observer : PropertyObserver)=> {
                observer.updateValue(arg);
            });
        }
    }
}
