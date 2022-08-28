# refit_log_calls

Refit Request Log Curl is a Nuget package that expose a way of log Refit request as a curl

## Installation

Install it from nuget.org

```bash
dotnet add package RefitLogCalls --version 1.0.0
```

## Usage

Considering that you have a existing RefitClient in you app

```c#
builder.Services
    .AddRefitClient<INationalizeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.nationalize.io"));   
```

Import the namespace, that provides .Log() and RegisterLogCurl() methods

```c#
builder.Services
    .AddRefitClient<INationalizeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.nationalize.io"))
    .LogCurl();

builder.Services.RegisterLogCurl();       
```

And that's it, your requests and responses will be logged always your are executing it in debug mode

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License
[MIT](https://choosealicense.com/licenses/mit/)