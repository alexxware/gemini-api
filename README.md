# Gemini API

1. Clonar Repositorio
2. En tu archivo `appsettings.json` asegurate que el archivo tenga la siguiente estructura:
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
3. Ve a https://cloud.google.com y crea un nuevo proyecto.
4. Después dirigete a https://aistudio.google.com/welcome e inicia sesión. Ahí **crea tu Key** para la API.
3. En tu entorno local, inicializa el gestor de secretos: `dotnet user-secrets init`
4. Despues guarda tu API Key `dotnet user-secrets set "ApiKeys:GoogleMaps" "MI_API_KEY_SECRETA"`