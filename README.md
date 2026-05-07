# Genesis CDB Challenge

## Sobre o Projeto

Este projeto foi desenvolvido como parte do desafio técnico para a Genesis.

A solução consiste em:

* Uma Web API desenvolvida em ASP.NET Core
* Uma aplicação Web desenvolvida em Angular CLI
* Implementação utilizando Clean Architecture
* Uso de CQRS com MediatR
* Validações com FluentValidation
* Testes unitários com cobertura superior a 90%

---

# Tecnologias Utilizadas

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
* Angular Forms
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

O projeto backend foi desenvolvido utilizando:

* Clean Architecture
* CQRS
* Dependency Injection
* SOLID Principles

## Camadas

### API

Responsável por:

* Controllers
* Swagger
* Configuração de DI
* Middlewares

### Application

Responsável por:

* Commands
* Handlers
* Validators
* DTOs

### Domain

Responsável por:

* Regras de negócio
* Cálculo do CDB
* Regras tributárias

---

# Regra de Negócio

A aplicação calcula o rendimento de um investimento em CDB utilizando:

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

# Como Executar o Projeto

## Pré-requisitos

Instalar:

* .NET 8 SDK
* Node.js
* Angular CLI
* Visual Studio 2022 ou superior

---

# Executando a API

## 1. Restaurar dependências

```bash
dotnet restore
```

## 2. Executar a API

```bash
dotnet run --project src/Genesis.Cdb.Challenge.Api
```

## 3. Swagger

Acesse:

```txt
https://localhost:xxxx/swagger
```

---

# Executando o Frontend Angular

## 1. Entrar na pasta do projeto

```bash
cd src/genesis-cdb-web
```

## 2. Instalar dependências

```bash
npm install
```

## 3. Executar aplicação

```bash
ng serve
```

## 4. Acessar aplicação

```txt
http://localhost:4200
```

---

# Endpoint da API

## Calcular investimento

### Request

```http
POST /api/financial/calculate
```

### Body

```json
{
  "initialAmount": 1000,
  "months": 12
}
```

### Response

```json
{
  "grossAmount": 1122.51,
  "netAmount": 1098.01
}
```

---

# Validações

A API valida:

* Valor inicial maior que zero
* Prazo maior que um mês

Em caso de erro, a API retorna:

```http
400 Bad Request
```

---

# Testes Unitários

Os testes unitários cobrem:

* Cálculo bruto
* Cálculo líquido
* Regras de imposto
* Regras de validação
* Handlers
* Crescimento composto
* Arredondamento

## Executar testes

```bash
dotnet test
```

---

# Cobertura de Código

## Executar cobertura

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Gerar relatório HTML

```bash
reportgenerator \
-reports:**/coverage.cobertura.xml \
-targetdir:coveragereport
```

## Abrir relatório

```txt
coveragereport/index.html
```

A cobertura da camada lógica é superior a 90%.

---

# Qualidade de Código

O projeto foi desenvolvido seguindo:

* Padrões SOLID
* Clean Code
* Sem warnings do Visual Studio
* Compatível com regras padrão do SonarLint

---

# Pacotes Utilizados

## Backend

```bash
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection

dotnet add package FluentValidation.AspNetCore

dotnet add package Swashbuckle.AspNetCore

dotnet add package xunit

dotnet add package xunit.runner.visualstudio

dotnet add package FluentAssertions

dotnet add package Moq

dotnet add package coverlet.collector
```

---

# Melhorias Futuras

* Dockerização
* CI/CD
* Testes de integração
* Logging estruturado
* Health Checks
* Observabilidade

---

# Autor

Desenvolvido para o desafio técnico Genesis.
