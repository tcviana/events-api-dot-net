# AwesomeDevEvents - Curso Criando REST APIs com ASP.NET Core

Está sendo desenvolvido um projeto de eventos de programação, utilizando ASP.NET Core 8.

## Tecnologias e ferramentas utilizadas
- Visual Studio 2022
- ASP.NET Core 8
- EF Core
- Swagger
- AutoMapper


## Funcionalidades
- Cadastro, Listagem, Detalhes, Atualização, e Remoção de Evento
- Cadastro de palestrantes
- Migration
* Create: dotnet ef migrations add <name> - Persistence/Migrations
* Rollback: ef migrations remove
* Execute: dotnet ef database update