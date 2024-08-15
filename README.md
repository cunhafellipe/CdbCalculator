# CDB Calculator

Este projeto é uma calculadora de CDB que inclui uma Web API em .NET e um frontend em Angular.

## Pré-requisitos

- .NET 6.0 SDK ou superior
- Node.js e npm
- Angular CLI

## Configuração do Projeto

1. Clone o repositório
2. Backend (.NET):
- Abra a solução `CdbCalculator.sln` no Visual Studio.
- Restaure os pacotes NuGet.
- Execute o projeto `CdbCalculator.Api`.

3. Frontend (Angular):
- Navegue até a pasta do projeto Angular:
  ```
  cd CdbCalculator.Web
  ```
- Instale as dependências:
  ```
  npm install
  ```
- Execute o aplicativo Angular:
  ```
  ng serve
  ```

4. Acesse o aplicativo em `http://localhost:4200`.

## Executando os Testes

- No Visual Studio, use o Test Explorer para executar os testes do backend.

## Estrutura do Projeto

- `CdbCalculator.Core`: Contém os modelos de domínio e interfaces.
- `CdbCalculator.Application`: Contém a lógica de negócios.
- `CdbCalculator.Api`: Projeto da Web API.
- `CdbCalculator.Web`: Projeto Angular para o frontend.
- `*.Tests`: Projetos de teste correspondentes.
