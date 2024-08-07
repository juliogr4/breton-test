# 1. Introdução

Esse projeto constitui em registro e login de usuário.

Somente usuários devidamente autenticados terão a permissão de criar, editar e deletar clientes

# 2. Versões

| Stack | Version |
| ----------- | ------- |
| API | .Net 8.0 |
| Angular | 16.0 |
| Sql Server Developer | 2022 |

# 3. Tecnologias
  | API |
  | ----------- |
  | Clean Architecture |
  | Mediator |
  | CQRS |
  | FluentValidation |
  | Mapster |
  | DDD |
  | Dapper ORM |
  | Json Web Token - Jwt |

---

  | FRONTEND |
  | ----------- |
  | jwt-decoder |
  | reactive forms |
  | interceptor |
  | guards |

---

  | DATABASE |
  | ----------- |
  | Stored Procedures |

# 4. Configurações

## 4.1 API
1. Acessem `appsettings.json`
2. Mudem o SqlConnection e o EmailSettings (Outlook) de acordo com seus dados pessoais.

```c#
    - "SqlConnection": "Data Source="YOUR_SERVER";Initial Catalog=breton_db;Integrated Security=True;Trust Server Certificate=True"
    - "EmailSettings": { "Host": "outlook.office365.com", "Port": 587, "From": "YOUR_EMAIL", "Password": "YOUR_EMAIL_PASSWORD" }
```

# 5. Startup Project

## 5.1 API

- **visual studio code:** `dotnet run --project ./Breton.Api/`
- **visual studio:** Basta abrir o arquivo `Breton Sln` e pressionar `F5` para abrir o swagger
 
*OBS: para testar no vscode, a pasta Request utiliza a extensão "Request Client". Caso venha à utilizar, não esqueçam de inserir o Bearer Token na solicitação*
  
## 5.2 SQL Server
Abram o arquivo `queries.sql` e executem os procedimentos pressionando "F5"

## 5.3 Angular
ng serve --o

# 6. ENDPOINTS

Account Endpoint: Accesso geral.
Customer Endpoint: Para cada requisição, é necessário enviar no HEADER o Bearer Token.

![Endpoints](https://github.com/juliogr4/breton-test/assets/102883494/61bd21d7-83a0-49d0-9f7b-ff40857b174c)

# 7. TELAS

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










