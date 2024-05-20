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