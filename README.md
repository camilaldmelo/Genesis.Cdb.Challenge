# Genesis CDB Challenge

Aplicação desenvolvida para o desafio técnico Genesis.

A solução contém:

* Web API em .NET
* Frontend em Angular CLI
* Clean Architecture
* CQRS com MediatR
* FluentValidation
* Testes unitários
* Cobertura de testes superior a 90%

---

# Tecnologias

## Backend

* .NET 8
* ASP.NET Core Web API
* MediatR
* FluentValidation
* Swagger
* xUnit
* Moq
* FluentAssertions
* Coverlet

## Frontend

* Angular CLI
* TypeScript
* HTML
* CSS
* HttpClient

---

# Estrutura da Solução

```txt
Genesis.Cdb.Challenge.sln

src/
 ├── Genesis.Cdb.Challenge.Api
 ├── Genesis.Cdb.Challenge.Application
 ├── Genesis.Cdb.Challenge.Domain
 └── genesis-cdb-web

tests/
 └── Genesis.Cdb.Challenge.Tests
```

---

# Arquitetura

O backend foi desenvolvido utilizando:

* Clean Architecture
* CQRS
* Dependency Injection
* SOLID Principles

## Camadas

### API

Responsável por:

* Controllers
* Swagger
* Configuração da aplicação
* Dependency Injection
* Middlewares

### Application

Responsável por:

* Commands
* Handlers
* Validators
* DTOs
* Interfaces

### Domain

Responsável por:

* Regras de negócio
* Cálculo do CDB
* Tributação

---

# Regra de Negócio

O cálculo do CDB utiliza a fórmula:

```txt
VF = VI x [1 + (CDI x TB)]
```

Onde:

* VF = Valor Final
* VI = Valor Inicial
* CDI = 0,9%
* TB = 108%

O cálculo é realizado utilizando juros compostos mês a mês.

## Tributação

| Prazo             | Imposto |
| ----------------- | ------- |
| Até 6 meses       | 22,5%   |
| Até 12 meses      | 20%     |
| Até 24 meses      | 17,5%   |
| Acima de 24 meses | 15%     |

---

# Endpoint da API

```http
POST /api/financial/calculate
```

## Request

```json
{
  "initialAmount": 1000,
  "months": 10
}
```

## Response

```json
{
  "grossAmount": 1101.23,
  "netAmount": 1080.15
}
```

---

# Validações

A API valida:

* Valor inicial maior que zero
* Prazo em meses maior que 1
* Prazo em meses menor que 12

Quando inválido, retorna:

```http
400 Bad Request
```

---

# Frontend Angular

A aplicação frontend foi desenvolvida utilizando Angular CLI.

## Funcionalidades

* Informar valor inicial do investimento
* Informar prazo em meses
* Consumir API REST do backend
* Exibir valor bruto do investimento
* Exibir valor líquido do investimento
* Validação básica de formulário

## Estrutura Frontend

```txt
src/app
 │
 ├── models
 │    ├── cdb-request.ts
 │    └── cdb-response.ts
 │
 ├── services
 │    └── cdb.service.ts
 │
 └── pages
      └── cdb-calculator
```

## Comunicação com Backend

O Angular consome:

```txt
POST /api/financial/calculate
```

URL local utilizada:

```txt
https://localhost:7063/api/financial/calculate
```

O backend possui configuração de CORS habilitada.

---

# Executando a API

Na raiz da solução:

```bash
dotnet restore

dotnet run --project src/Genesis.Cdb.Challenge.Api
```

Swagger:

```txt
https://localhost:7063/swagger
```

---

# Executando o Frontend Angular

Entre na pasta do Angular:

```bash
cd src/genesis-cdb-web
```

Instale as dependências:

```bash
npm install
```

Execute:

```bash
ng serve
```

Acesse:

```txt
http://localhost:4200
```

---

# Executando os Testes

Na raiz da solução:

```bash
dotnet test
```

---

# Cobertura de Testes

Execute:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

# Gerar Relatório HTML

Instale:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Windows CMD:

```bash
reportgenerator ^
-reports:**/coverage.cobertura.xml ^
-targetdir:coveragereport
```

PowerShell:

```powershell
reportgenerator `
-reports:**/coverage.cobertura.xml `
-targetdir:coveragereport
```

Abrir:

```txt
coveragereport/index.html
```

A cobertura da camada lógica é superior a 90%.

---

# SonarLint

Instalação:

```txt
Extensions
→ Manage Extensions
→ SonarLint for Visual Studio
```

Ativar análise completa:

```txt
Tools
→ Options
→ Text Editor
→ C#
→ Advanced
→ Enable full solution analysis
```

Depois:

```txt
Build → Rebuild Solution
```

---

# Requisitos Atendidos

* Web API em .NET
* Frontend Angular CLI
* Clean Architecture
* CQRS
* MediatR
* FluentValidation
* Swagger
* API REST
* Sem banco de dados
* Testes unitários
* Cobertura superior a 90%
* Compatível com SonarLint
* Backend e frontend no mesmo repositório
* README com instruções de execução
