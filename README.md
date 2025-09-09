# Dev

1. Clonar Repositorio
2. Crea tu archivo `appsettings.json` en la carpeta del proyecto con la siguiente estructura:

>{\
"Logging": {\
"LogLevel": {
"Default": "Information",
"Microsoft.AspNetCore": "Warning"
}\
},\
"AllowedHosts": "*",\
"ApiKeys": {
"GoogleGemini": ""
}\
}
3. En tu entorno local, inicializa el gestor de secretos: `dotnet user-secrets init`
4. Despues guarda tu API Key `dotnet user-secrets set "ApiKeys:GoogleMaps" "MI_API_KEY_SECRETA"`