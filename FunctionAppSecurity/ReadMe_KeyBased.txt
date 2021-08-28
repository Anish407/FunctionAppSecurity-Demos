Different ways to Authorize (These are called Authorization Levels) -- ENUM Values
Anonymous
Function
Admin
System
User

------------ Anonymous ---------------------
Anonymous means no authentication is required.  Any valid HTTP request passes. Function, 
Admin & System authorization level are key based.


------------ Function ---------------------
Basically, there are two types of keys:  host and function keys.  
The former is scoped at the function app level while the latter is scoped at the function level (i.e. within a function app).

There is a special host key called the master key (aptly named _master).  
A master key is always present and can’t be revoked although it can be renewed, 
i.e. its value can be changed and its older value won’t be accepted anymore.

A key can be passed to an Azure Function HTTP request in the URL as the code query string.  
Alternatively, it can be included in the x-functions-key HTTP header. 
Only the key value, not its name, is passed.

Function authorization level requires a key for authorization.
Both function and host key will work.
In that sense it is the less restrictive of key-based authorization level.


------------ Admin ---------------------
Admin authorization level requires a host key for authorization.

Passing a function key will fail authorization and return an HTTP 401 – Unauthorized error code.

------------ System ---------------------
System authorization level requires the master key of a function app for authorization.

Passing a function key or a host key (except the master key) will fail authorization and return an HTTP 401 – Unauthorized error code.

------------ User ---------------------
User authorization level isn’t key based.  Instead it does mandate a valid authentication token.