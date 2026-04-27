####### Good Hamburger #######

# 1. Descrição do Projeto
Sistema de gestão de uma hamburgueria com foco em facil usabilidade, regras de negócios bem estruturadas e boas práticas de arquitetura e desenvolvimento.

# 2. Tecnologias utilizadas
- Backend: .NET 10 (WebApi - ASP.NET Core)
- Frontend: Blazor WebAssembly
- ORM: EFCore
- Bootstrap 5
- Base de Dados: SQLite
- Arquitetura: Clean Architecture / DDD

# 3. Base de Dados (![IMG: Diagrama de Banco de Dados](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/diagrama-banco-de-dados.png))

# 3.1 Escolha da stack
- Optei por SQLite por ser uma base de dados leve, portátil e sem configurações, o que torna ideal para projetos modelos e facilita o uso para os avaliadores.

# 3.2 Modelagem de Dados
- A modelagem foi pensada para garantir integridade dos dados, flexibilidade na relação entre entidades e consistência histórica dos pedidos da maneira mais simples possível
- Foquei na normalização e separação das responsabilidades pensando em performance e em um eventual crescimento do sistema.
- As principais tabelas são: Order, MenuItem e OrderItem
- Me utilizei de entidade intermediária (OrderItem) para relacionamentos N:N
- As tabelas OrderStatus e MenuItemTypes são para evitar problemas com variações de strings e garantir a integridade referencial das informações via FK. Isso também facilita a manutenção do sistemas para possíveis futuras inclusões de novos tipos.

# 4. Arquitetura
- Optei pela Clean Architetura por promover baixo acoplamento, alta coesão e separação clara de responsabilidade entre as camadas.
Além disso, o coração do sistema proposto são as regras de negócio o que torna a Clean Architecture o modelo ideal por isolar as regras de negócio na camada de Domínio.
- Outro ganho é referente aos testes, com esse modelo os testes focam totalmente no que importa, que são as regras de negócio.

# 4.1. Camadas e Responsabilidades
- Tests: Testes unitários utilizando xUnit
- WebAPI: Exposição dos endpoints
- Domain: Regras de Negócio.
- Application: Orquestração.
- DataApplication: Objetos de transporte Request e Response, respeitando o Princípio da Responsabilidade Única (SRP)
- Infrastructure: Comunicação com a Base de Dados.
- IoC: Inversão de Dependência, desacopla as camadas, facilita a testabilidade e manutenção do código.
- BlazorWASM: Interface do usuário.

- Fluxo padrão: UI -> WebAPI -> Application -> Domain -> Infrastructure

# 5. Testes Unitários
- O projeto conta com testes unitários utilizando xUnit, focados na validação das regras de negócio da camada de domínio.

# 6. Instruções para execução
## ADICIONAR INSTRUÇÕES DE EXECUÇÃO


# 7. Telas do Usuário

# 7.1. Tela Inicial (![IMG: Tela Inicial](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/tela-inicial.png))
- Foco nas informações mais importantes como valores acumulados com filtros dinâmicos, listagem com histórico de pedidos e ranking de itens mais vendidos.

# 7.1.1. Card "Total de Pedido" (![IMG: Total de Pedido](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/total-pedidos.png))
- Informações detalhadas sobre valores acumulados.
- Filtros dinâmicos, dando ao usuário a liberadade de escolher suas visualizações.

# 7.1.2. Listagem "Pedidos" (![IMG: Listagemm de Pedidos](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/listagem-pedidos.png))
- Lista os pedidos junto com suas principais informações, permitindo ao usuário rápida visualização de suas vendas.

# 7.1.2. Card "Os mais vendidos" (![IMG: Os mais vendidos](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/mais-vendidos.png))
- De maneira dinâmica, mostra os itens mais vendidos da Good Hamburger.

# 7.2. Novo Pedido (![IMG: Tela Novo Pedido](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/novo-pedido.png))
- A ideia foi ter todas as informações na mesma tela, tornando simples e rápido todo o processo de inclusão de um novo pedido.

# 7.2.1 Cliente e Itens do Pedido ![IMG: Etapas 1 e 2](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/cliente-itens-pedido.png)
- Nessa etapa o usuário informa o nome e seleciona os itens do pedido.
- Há uma tratativa obrigando a inserção de um cliente. Essa tratativa não permite o avanço até seu preenchimento.

# 7.2.2 Itens Selecionados (![IMG: Etapa 3](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/itens-selecionados.png))
- Permite de maneira fácil a seleção da quantidade de cada um dos itens e também sua exclusão.
- Aplica as regras de negócios e mostra os erros de maneira clara na tela para o usuário.

# 7.2.3 Resumo do Pedido (![IMG: Etapa 4](GoodHamburger.BlazorWASM/wwwroot/images/readme-images/resumo-pedido.png))
- Mostra os valores do pedido de forma detalhada, incluindo o desconto aplicado segundo as regras de itens selecionados.
- Permite finalizar ou limpar pedido.

# 8. Endpoints

# 8.1. Pedidos (Order)
- (POST) api/orders/validate-insert: endpoint que valida e insere um pedido e seus respectivos itens. Existe apenas para testes de inserção via backend, pois o fluxo do front faz a validação e inserção de maneira apartada.
- (POST) api/orders/validate: valida o pedido de acordo com as regras de negócio explicitadas no escopo do Desafio Técnico.
- (POST) api/orders: faz a inserção do pedido e de seus respectivos itens.
- (GET) api/order: lista todos os pedidos.
- (GET) api/order/summary: retorna um relatório sobre os pedidos incluindo detalhamento de valores.
- (DELETE) api/orders/{id}: optei pelo soft delete para evitar problemas de deleção de pedidos que possuem itens vinculados a ele.

# 8.2 Cardápio (MenuItem)
- (GET) api/menu-items: lista todos os itens do cardápio.
- (GET) api/menu-items/{id}: recupera um item do cardápio pelo seu id.

# 9. Ficou de fora
# Por limitação de tempo, algumas melhorias importantes não foram implementadas:
- Tratamento de exceções: gostaria de ter implementado algo mais robusto confiável, como um tratamento de excessões via middleware, padronizando as respostas e gerando logs para melhorar ainda mais a rastreabilidade de erros (além de ajudar em casos de hotfix).
- Testes mutantes: optei por testes unitário cobrindo as principais regras de negócio. Os testes mutantes seriam importantes para avaliar a efetividade desses mesmos testes, o que eleva significativamente a qualidade da aplicação.
- Imagem do item na base de dados: em um contexto base de dados maior, como SQL Server, adicionaria um campo do tipo blob na tabela de items do cardápio (MenuItems) para que cada item tenha definida sua imagem diretamente em seu registro.


# 10. Considerações finais AINDA MELHORAR ESSE TEXTO
- O projeto foi desenvolvido com foco em boas práticas de arquitetura, separação de responsabilidades e escalabilidade. Também tentei seguir ao máximo, como em todo desenvolvimento que faço, a idéia do clean code e os princípios fundamentais do SOLID.