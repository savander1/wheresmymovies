///<reference path="exceptions.ts" />

namespace ViewModel {
    
    export interface PropertyObserver<T> {
        updateValue (value:T);
    }
    
    export interface TwoWayBindable<T> {
        updatePropertyValue (value: T);
    }

    export class ObservableProperty<T> implements TwoWayBindable<T> { 
        private observers : PropertyObserver<T> [];
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
        
        updatePropertyValue (value: T): void {
            this._propertyValue = value;
        }

        registerObserver (observer : PropertyObserver<T>) : void {
            this.observers.push(observer);
        }

        removeObserver (observer : PropertyObserver<T>) : void {
            this.observers.splice(this.observers.indexOf(observer), 1);
        }

        notifyObservers (arg : any) : void {

            this.observers.forEach((observer : PropertyObserver<T>)=> {
                observer.updateValue(arg);
            });
        }
    }
}
