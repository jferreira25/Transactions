# Projeto para criação de transações simples

## Funcionalidades

- Criar transação de acordo com o valor passado
- Consultar a transação


## Tecnologias utilizadas

- .NET 6.0
- Web API
- MongoDB (API)
- RabbitMq
- Rebus orquestrador do mqqt para tratar poly,circu
- Docker

## Iniciando a aplicação

- Para iniciarmos a aplicação basta rodar o comando

``` docker-compose up ```

nele já está configurado tudo que necessitamos para a subida da api.

com isso basta acessar a aplicação pelo : <http://localhost:8081/swagger/index.html>

## Instalação k6

Para rodarmos o teste de carga necessitamos instalar o k6 no powershell para isso
execute o comando:

[k6 - intalação](https://k6.io/docs/get-started/installation/)

``` winget install k6 --source winget no powershell ```

- reinicie powerShell

- após instalado navegue até o diretório "testes-stress" e execute o comando

``` k6 run todo-list.js ```

no arquivo js está configurado uma rota da aplicação para poder iniciar os testes de cargas para a aplicação.
Após finalizada ele gerará um arquivo html com algumas informações para em um caso real conseguirmos escalonar a aplicação de forma adequada.

Para exemplificar quando rodado eu desliguei o mongo para forçar falhas de request onde eu só considerava status code 200 e 404 como sucesso.

e o resultado foi:

![teste-carga](testes-stress/teste-carga.png)

## Testes de unidade da aplicação

![testes-unitario](testes-stress/testes-unitarios.png)