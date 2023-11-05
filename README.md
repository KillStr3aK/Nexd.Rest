# C# Sync & Async HttpClient Wrapper

# Installation

Available on [NuGet](https://www.nuget.org/packages/Nexd.Rest/)
[![NuGet version (Nexd.Reflection)](https://img.shields.io/nuget/v/Nexd.Rest.svg?style=flat-square)](https://www.nuget.org/packages/Nexd.Rest/)

```
dotnet add package Nexd.Rest --version 1.0.0
```

### Note
- There is two classes that you can use, depends on your usage.
  - If you don't need to maintain constant connection, just want to make a simple `HttpRequest` you can just use the `HttpRequest` class.
  - If you plan on 'connecting' to an API and actively doing `HttpRequest` you should use the `HttpAPI` class that keeps a `HttpClient` instance alive and is using the same `HttpRequest` class with that instance for its lifetime.

### Initialization
```c#
// import using
using Nexd.Rest;

// random user class that I may send, or get in a result of a `HttpRequest`
class User : IJsonObject // If you have classes that you want to use as return values from a `HttpRequest` (or want to use in a `HttpMethod.Post` request, they should implement this interface)
{
    [JsonProperty("id)]
    public int ID;

    [JsonProperty("username")]
    public string Username;
}
```

### `HttpAPI` method
```c#
// in case of using `HttpAPI`
HttpAPI api = new HttpAPI("http://api.myserver.domain"); // Note: no slash at the end

User? user = api.SendRequest<User>("/user/get/id/1"); // random route, it depends on your API
Console.WriteLine($"{(user as IJsonObject).ToJSON()}"); // IJsonObject implements default ToJSON method which is a wrapper around Newtonsoft.Json serialization method

// you can also make your api wrapper using inheritance like this:
class MyAPI : HttpAPI
{
    public MyAPI(string url) : base(url)
    {
            this.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}"); // maybe set your security tokens here, or anything that is related to the `HttpClient` itself
            this.Client.Timeout = TimeSpan.FromSeconds(5);
    }

    public IEmptyResponse? CreateUser(User user)
    {
        return this.SendRequest<IEmptyResponse>("/user/create", HttpMethod.Post, user);
    }

    public User? GetUserById(int id)
    {
        return this.SendRequest<User>($"/user/get/id/{id}");
    }
}
```

### `HttpRequest` method
```c#
// Sending a GET request to the api
using (HttpRequest request = new HttpRequest("http://api.myserver.domain/user/get/id/15", HttpMethod.Get))
{
    User? user = request.Send<User>();
}
```

### Async
Note: that every method has its `Async` version implemented

Documentation is currently unavailable, but its self explanatory and has the same syntax with the sync version, just with `Async` method names.

### TODO
- CancellationToken
- Async documentation
- More detailed documentation in general
