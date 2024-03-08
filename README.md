# Sobre o Teste

Foi utilizado o padrão MVC para desenvolver o projeto, salvo os arquivos de Identity que foram gerados pelo Visual Studio.

Por não ter plena noção de como o .NET se comporta em requisições utilizando FormData, foi utilizado POST na Action de remover produto do carrinho. Há linguagens em que é necessário acrescentar um campo oculto para que o servidor responda corremente, como neste exemplo: 

```html
<input type="hidden" name="_method" value="DELETE">
```

Houveram algumas customizações de cores e também a utilização do logo retirado do site da IQVIA. Na medida do possível, tentei utilizar apenas classes do Bootstrap.

As migrations e seeds do banco são executadas no start da aplicação.

# Tecnologias

- .NET Core 8
- SQL Server 2022 - Local DB
- Bootstrap 5

# Funcionalidades 

## Funcionalidades obrigatórias

- Deverá ter log: Foi implementado utilizando o Serilog. Os logs serão gerados na pasta Logs do projeto
- Página de produto: Feito. Coloquei uma imagem do placehold.it para evitar alguns buracos no layout. Para o tipo do produto utilizei um Enum
- Carrinho de Compra: Acessível ao adicionar um produto no carrinho. Caso o carrinho esteja vazio, o controller redireciona para a home do site
- Permitir colocar o produto no carrinho: possível na página de visualização do produto
- Página de conclusão: página simples apenas com um texto explicativo. Ao concluir o pedido, o status do produto é alterado para Aguardando Pagamento.

## Funcionalidades não obrigatórias

- Ter usuário: utilizei o Identity para criar a parte de login e cadastro de usuário. O cadastro/login é obrigatório para finalizar o pedido. Caso o usuário esteja deslogado ao tentar fechar o pedido o controller realiza um redirecionamento para a página de login. Entendo que a melhor abordagem seria algum middleware de Authorization para este caso.
- Relatório de consulta de pedidos: Não sei exatamente o que o relatório deveria conter, mas criei uma página de "Meus Pedidos" onde o usuário autenticado poder ver a lista de seus pedidos com seus produtos e valor total

# Publicação do Projeto

O projeto foi publicado através do perfil de publicação em pasta e então importado para o IIS. 

Foi necessário seguir um procedimento que abre vulnerabilidades para garantir que o Local DB funcionasse no IIS da mesma forma como funcionou no ambiente de desenvolvimento.
Entendo que esta configuração não deve ser feita em um ambiente de produção, porém nenhuma das minhas tentativas funcionou adequadamente. O procedimento pode ser visto em: https://stackoverflow.com/a/43545063 O procedimento envolve, basicamente, modificar o modelo de processo do site de Application Pool para Local System.
