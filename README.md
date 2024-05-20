# CashFlow API

## Sobre o projeto

**API** desenvolvida durante meus estudos na formação de C# da Rocketseat, sua proposta é oferecer uma solução eficaz para o gerenciamento de despesas pessoais.

Os usuários podem registrar suas despesas, detalhando-as por título, data e hora, escrever uma descrição (caso desejem), valor e a forma de pagamento, com os dados sendo armazenados em um banco de dados **MySQL**

Ela foi desenvolvida utilizando o **.NET 8**, adotando os principios de **Domain-Driven Design (DDD)**. Sua arquitetura baseia-se em **REST**, utilizando métodos **HTTP** para a comunicação e além disso conta com uma documentação das rotas pelo **Swagger**, que proporciona uma interface gráfica para os testes dos endpoints.

Os pacotes **NuGet** utilizados foram:
    <ul>
        <li>**AutoMapper**: Responsável pelo mapeamento entre objetos de domínio e requisição/resposta, reduzindo repetição de código.</li>
        <li>**FluentAssertions**: Utilizado nos testes de unidade, tornando as verificações mais legiveis, com uma escrita mais clara e compreensível.</li>
        <li>**FluentValidation**: Utilizado na implementação de regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo.</li>
        <li>**EnitityFramework**: Atua como um ORM (Object-Relational Mapper) simplificando as interações com o banco e dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de consultas SQL.</li>
    </ul>

![hero-image]

### Features

- **Domain-Drive Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Testes de Unidade**: Testes abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.
- **Geração de Relatórios**: Capacidade de exportar relatórios detalhados para **PDF** e **Excel**, oferencendo uma análise visual e eficaz das despesas.
- **RESTful API com Documentação Swagger**: Interface documetnada que facilita a integra;cão e o teste por parte dos desenvolvedores.

### Build With
![.NET Badge](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge)
![Windows Badge](https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge)
![Visual Studio Badge](https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge)
![MySQL Badge](https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge)
![Swagger Badge](https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge)

## Getting Started

Para obter uma cópia local funcionando, siga estes passos.

### Requisitos

* Visual Studio 2022+ ou VS Code
* Windowns 10+ ou Linux/MacOS com [.NET SDK][dot-net-sdk] instalado
* MySQL Server

### Instalação

1. Clone o repositório:
    ```sh 
    git clone git@github.com:matheussegre/CashFlow.git
    ```
2. Preencha as informações no arquivo `appsettings.Development.json`
3. Execute a API e aproveite seu teste :)




<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0

<!-- Images -->
[hero-image]: images/heroimage.png

