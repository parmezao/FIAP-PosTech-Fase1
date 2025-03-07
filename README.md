# Projeto Tech Challenge 02 - .NET 8 + Sql Server com Docker 

Este é um projeto .NET que utiliza Docker para facilitar o ambiente de desenvolvimento e execução. Utiliza Docker Compose para orquestrar a aplicação juntamente com um banco de dados PostgreSQL.

## Pré-requisitos

- Docker Engine: [Instalação do Docker](https://docs.docker.com/get-docker/)
- Docker Compose: [Instalação do Docker Compose](https://docs.docker.com/compose/install/)

## Como executar

1. Clone este repositório:

 ```bash
   git clone https://github.com/parmezao/FIAP-PosTech-Fase1.git
   cd FIAP-PosTech-Fase1
  ```

2. Execute o seguinte comando para iniciar o projeto junto com o SQL Server:

```bash
  docker-compose up -d
```
Isso iniciará os contêineres Docker em segundo plano (-d para detached mode), incluindo a aplicação .NET e o banco de dados SQL Server.

Acesse a aplicação em http://localhost:8080, ou http://localhost:8080/swagger para utilizar a interface do Swagger

Usuário e Senha para a geração de Token.

- Usuário: admin
- Senha: admin@123

3. Parando a execução do projeto e removendo os containers

```bash
  docker-compose down
```

