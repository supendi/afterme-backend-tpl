The mindset of how we should treat controllers is:
Controller is the highest level layer, the outer layer from the business layer.
Contollers are transport layer, the Http Transport layer. 

So some of controller's responsibilities are:
- Provides the required arguments by the business layer methods/constructors(the SomeService/SomeManager class)
- Handles anything related to the HttpRequest/Response
- Convertion (like from http request body to object class)


Examples:

The controllers should not have logic code like the following:

if(order.total < 0){
    return something;
}

The above code is a business logic code. The above code should be in the service classes, the domain business classes.



The following code is accepted:

if(!httpContext.Request.Headers.Contains('Authorization')){
    return unauthorize();
}

The above code is allowed in controller because that is a transport layer logic code.
=========


Most of controllers will only call the methods of the business class/services, Thats why our existing controllers are tiny and clean.


