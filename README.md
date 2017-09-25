Перед запуском необходимо установить node.js, .net core 2.0
Для работы нужно запустить NotesApi и NotesWeb


Запуск NotesWeb из консоли:
 - npm install
 - dotnet publish
 - cd bin\Debug\netcoreapp2.0\publish\
 - dotnet NotesWeb.dll

Запуск NotesApi из консоли:
 - dotnet publish
 - cd bin\Debug\netcoreapp2.0\publish\
 - dotnet NotesApi.dll

При запуске из VisualStudio все должно происходить автоматически