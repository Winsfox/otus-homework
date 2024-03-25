1. Развернуть СУБД - docker-compose.yml
2. Накатить миграцию - dotnet run --launch-profile "Migration" --project src/Otus.Highload.Homework.Hosting
3. Запустить приложение -  dotnet run --launch-profile "Local" --project src/Otus.Highload.Homework.Hosting .

Swagger доступен по порту - http://localhost:5000/swagger/index.html .


