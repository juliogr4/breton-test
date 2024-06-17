# INTRODUÇÃO
  - Esse projeto constitui em registro e login de usuaário.
  - Somente usuários devidamente autenticados terão a permissão de criar, editar e deletar clientes

# VERSÕES
  - API: .Net 8.0
  - Angular: 16.0
  - Sql Server Developer: 2022 

# TECNOLOGIAS
  - API
    - Clean Architure
    - Mediator
    - CQRS
    - FluentValidation
    - DDD
    - Dapper ORM
    - Json Web Token - Jwt

  - FRONTEND
    - jwt-decoder: decode jwt
    - reactive forms
    - interceptor
    - guards

  - DATABASE
    Stored Procedures

# CONFIGURAÇÕES

- API
  Em appsettings.json, mudem o SqlConnection e o EmailSettings (Outlook) de acordo com seus dados pessoais.
    - "SqlConnection": "Data Source="YOUR_SERVER";Initial Catalog=breton_db;Integrated Security=True;Trust Server Certificate=True"
    - "EmailSettings": { "Host": "outlook.office365.com", "Port": 587, "From": "YOUR_EMAIL", "Password": "YOUR_EMAIL_PASSWORD" }

# INICIALIZAÇÃO

- API
    - visual studio code: dotnet run --project ./Breton.Api/
    - visual studio: Basta abrir o arquivo Breton Sln e pressionar "F5" para abrir o swagger
 
    - OBS: para testar no vscode, a pasta Request utiliza a extensão "Request Client". Caso venha à utilizar, não esqueçam de inserir o Bearer Token na solicitação
  
- SQL Server
    -  Abram o arquivo "queries.sql" e executem os procedimentos pressionando "F5"

-  Angular
  - ng serve --o    

# TELAS

REGISTER
![register](https://github.com/juliogr4/breton-test/assets/102883494/d9a2df4c-99fb-44ba-8f7a-87f1bc7703f3)

LOGIN
![login](https://github.com/juliogr4/breton-test/assets/102883494/a24063e9-b143-4eb5-8aa0-fab5dd3585c0)

CUSTOMERS
![customer-list](https://github.com/juliogr4/breton-test/assets/102883494/668d1f21-3c06-4764-8a9d-d8fbdf8fdff2)

ADD EDIT CUSTOMER
![add-edit-customer](https://github.com/juliogr4/breton-test/assets/102883494/8c4e481f-9e0a-41b7-b2d9-8e521c0868b9)

DELETE CUSTOMER
![delete-customer](https://github.com/juliogr4/breton-test/assets/102883494/47392b83-38c1-411c-98f6-1eff1a4e4512)

EMAIL CONFIRMATION
![email-confirmation](https://github.com/juliogr4/breton-test/assets/102883494/417d1c4a-b95e-44d4-a2c8-df20ce629356)










