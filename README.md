# MinhaApi
Projeto simples de Web API desenvolvida em ASP.NET Core 5.0, aplicando conceitos da estrutura DDD e testes unitários.

<h4 align="center"> 
  <a href="#Tecnologias-e-ferramentas">Tecnologias e ferramentas</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; 
  <a href="#Sobre-o-projeto">Sobre o projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#Demonstração">Demonstração</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  </br>
  <a href="#Como-usar">Como usar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#Licença">Licença</a>
</h4>

<br/>

<p align="center">
  <a href="https://opensource.org/licenses/MIT">
    <img src="https://img.shields.io/badge/License-MIT-blue.svg" alt="License MIT">
  </a>
</p>

# Tecnologias e ferramentas

O projeto foi desenvolvido com as seguintes tecnologias e ferramentas:

- [Visual Studio 2022 Community](#Pré-requisitos) - IDE utilizada para desenvolver a solução.
- [ASP.NET Core 5.0](https://docs.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-5.0) - plataforma utilizada para o desenvolvimento da aplicação.
- [Swagger](https://swagger.io/) - framework que disponibiliza ferramentas para gerar a documentação da API.
- [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/) - mapeador de banco de dados de objeto para .NET.
- [SQLite](https://www.sqlite.org/docs.html) - banco de dados relacional.
- [AutoMapper](https://automapper.org/) - biblioteca que resolve mapeamento de um objeto para outro.
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/) -  biblioteca .NET para construir regras de validação.
- [xUnit](https://xunit.net/) - framework utilizado para teste de unidade.
- [Bogus](https://github.com/bchavez/bogus) - framework que gera dados aleatórios, reais e em diversos idiomas, por exemplo, cpf, nome...
- [MOQ](https://github.com/Moq/moq4/wiki/Quickstart) - framework que simula o comportamento de objetos reais na plataforma .NET.
- [AutoMock](https://github.com/moq/Moq.AutoMocker) - implementa os mocks de forma automática para teste de unidade.
- [FluentAssertions](https://fluentassertions.com/introduction) - conjunto de métodos de extensão .NET que ajuda a escrever teste de unidade com mais produtividade e legibilidade.

# Sobre o projeto

Este é um projeto desenvolvido em ASP.NET Core 5.0 para realizar a gestão de produtos e fornecedores.                                                     
A API disponibiliza uma documentação criada com Swagger. Os testes unitários desse projeto foram executados utilizando o xUnit.                                                    
A interface do Swagger, deste projeto, permite as seguintes funcionalidades para a gestão de produtos:

- Obter um produto a partir do seu ID.
- Obter os produtos baseados nos parâmetros de consulta, com a resposta paginada.
- Salvar um produto na base de dados.
- Alterar um produto na base de dados.
- Exclui um produto na base de dados. A exclusão é lógica, atualizando o campo Ativo para false.

A gestão de fornecedores possui as mesmas funcionalidades descritas acima em suas rotas.

# Demonstração
                                                                                        
A figura abaixo ilustra a interface da API documentada com Swagger.

<p align="center">
  <img src="https://github.com/renanegobbi/MinhaApi/blob/main/Github/ImgApi.PNG"/>
</p>

A figura abaixo mostra um print dos testes unitários executados no Visual Studio.

<p align="center">
  <img src="https://github.com/renanegobbi/MinhaApi/blob/main/Github/ImgTests.PNG"/>
</p>

# Como usar

Após clonar esta solução, executar o seguinte comando no projeto MinhaApi.Data para criar o banco de dados.                                   
<br><b>update-database</b>

# Licença                                     
Este projeto está sob a licença do MIT. Consulte a [LICENÇA](https://github.com/TesteReteste/lim/blob/master/LICENSE) para obter mais informações.

