# Gemini API

1. Clone Repository
2. In your `appsettings.json` file, make sure it has the following structure:
~~~
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ApiKeys": {
    "GoogleGemini": ""
  }
}
~~~
3. Go to https://cloud.google.com and create a new project.
4. Then go to https://aistudio.google.com/welcome and sign in. There, **create your API Key**.
5. In your local environment, initialize the secret manager: `dotnet user-secrets init`
6. Then save your API Key: `dotnet user-secrets set "ApiKeys:GoogleMaps" "MY_SECRET_API_KEY"`
