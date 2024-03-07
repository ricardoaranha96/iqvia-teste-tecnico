# Sobre o Teste

Foi utilizado o padr�o MVC para desenvolver o projeto, salvo os arquivos de Identity que foram gerados pelo Visual Studio.

Por n�o ter plena no��o de como o .NET se comporta em requisi��es utilizando FormData, foi utilizado POST na Action de remover produto do carrinho. H� linguagens em que � necess�rio acrescentar um campo oculto para que o servidor responda corremente, como neste exemplo: 

```html
<input type="hidden" name="_method_" value="DELETE">
```

Houveram algumas customiza��es de cores e tamb�m a utiliza��o do logo retirado do site da IQVIA. Na medida do poss�vel, tentei utilizar apenas classes do Bootstrap.

As migrations e seeds do banco s�o executadas no start da aplica��o.

# Tecnologias

- .NET Core 8
- SQL Server 2022 - Local DB
- Bootstrap 5

# Funcionalidades 

## Funcionalidades obrigat�rias

- Dever� ter log: Foi implementado utilizando o Serilog. Os logs ser�o gerados na pasta Logs do projeto
- P�gina de produto: Feito. Coloquei uma imagem do placehold.it para evitar alguns buracos no layout. Para o tipo do produto utilizei um Enum
- Carrinho de Compra: Acess�vel ao adicionar um produto no carrinho. Caso o carrinho esteja vazio, o controller redireciona para a home do site
- Permitir colocar o produto no carrinho: poss�vel na p�gina de visualiza��o do produto
- P�gina de conclus�o: p�gina simples apenas com um texto explicativo. Ao concluir o pedido, o status do produto � alterado para Aguardando Pagamento.

## Funcionalidades n�o obrigat�rias

- Ter usu�rio: utilizei o Identity para criar a parte de login e cadastro de usu�rio. O cadastro/login � obrigat�rio para finalizar o pedido. Caso o usu�rio esteja deslogado ao tentar fechar o pedido o controller realiza um redirecionamento para a p�gina de login. Entendo que a melhor abordagem seria algum middleware de Authorization para este caso.
- Relat�rio de consulta de pedidos: N�o sei exatamente o que o relat�rio deveria conter, mas criei uma p�gina de "Meus Pedidos" onde o usu�rio autenticado poder ver a lista de seus pedidos com seus produtos e valor total

# Publica��o do Projeto

O projeto foi publicado atrav�s do perfil de publica��o em pasta e ent�o importado para o IIS. 

Foi necess�rio seguir um procedimento que abre vulnerabilidades para garantir que o Local DB funcionasse no IIS da mesma forma como funcionou no ambiente de desenvolvimento.
Entendo que esta configura��o n�o deve ser feita em um ambiente de produ��o, por�m nenhuma das minhas tentativas funcionou adequadamente. O procedimento pode ser visto em: https://stackoverflow.com/a/43545063 O procedimento envolve, basicamente, modificar o modelo de processo do site de Application Pool para Local System.