# CDB Calculator

Este projeto � uma calculadora de CDB que inclui uma Web API em .NET e um frontend em Angular.

## Pr�-requisitos

- .NET 6.0 SDK ou superior
- Node.js e npm
- Angular CLI

## Configura��o do Projeto

1. Clone o reposit�rio
2. Backend (.NET):
- Abra a solu��o `CdbCalculator.sln` no Visual Studio.
- Restaure os pacotes NuGet.
- Execute o projeto `CdbCalculator.Api`.

3. Frontend (Angular):
- Navegue at� a pasta do projeto Angular:
  ```
  cd CdbCalculator.Web
  ```
- Instale as depend�ncias:
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

- `CdbCalculator.Core`: Cont�m os modelos de dom�nio e interfaces.
- `CdbCalculator.Application`: Cont�m a l�gica de neg�cios.
- `CdbCalculator.Api`: Projeto da Web API.
- `CdbCalculator.Web`: Projeto Angular para o frontend.
- `*.Tests`: Projetos de teste correspondentes.