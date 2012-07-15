everytrail4net
==============

everytrail4net is a simple C# wrapper around the existing functionality of the EveryTrail (www.everytrail.com) API. 

The library in its current state is intended for C#.NET developers who would 
like a jumpstart to writing applications specifically against the EveryTrail API.

It is a work in a progress, so PLEASE do not hesitate to contact me with bugs, feature requests, etc...

The best source of documentation and expected behavior should be contained within the app itself. The WebSite project
included in the solution is a test harness created to demonstrate how to consume the available methods of the library.
The Tests project contains unit tests enforcing the expect behavior of these methods.

Set up and Configuration
------------------------

Before using the EveryTrail API, you will need to register your application with EveryTrail in order to get an API key.

Once you have obtained credentials, you must update the appropriate .config (web.config or app.config) file within your 
application to include the following app settings:

```xml
  <appSettings>
    <add key="Key" value="YourKeyHere"/>
    <add key="Secret" value="YourKeySecretHere"/>
    <add key="Version" value="3"/>
  </appSettings>
```

Available Methods
-----------------

#### UserLogin

Attempt to log in a user

Arguments:

* userName (string) - The attempted user name
* password (string) - The attempted password

Returns:

A UserLoginResponse object containing the status of the request and the user's ID if the request was successful.

```csharp
using EveryTrailNET.Core;
using EveryTrailNET.Core.QueryResponse
...
 UserLoginResponse response = Actions.UserLogin(txtUserName.Text, txtPassword.Text);

  if (response.Status)
  {
      ltlResponse.Text = "Successful Login. User id is " + response.UserID;
  }
  else
  {
      ltlResponse.Text = "Login failed";
  }

```

Building the app
----------------

Testing
-------

Contributing
------------

