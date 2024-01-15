# Tecnologias utilizadas

- .NET 8.0
- Minimal API
- MongoDB
- Arquitetura Mediator com Mediatr
- FluentValidator para validações de commands/events
- Testes com NUnit e FluentAssertions

# Execução do projeto

1. Abrir algum prompt de comando na pasta raiz.
2. Executar o comando "docker-compose up".
3. A API vai estar disponível no localhost:8080, e o banco no localhost:27017.
4. Swagger habilitado em release, para facilitar os testes.
 
# Uso dos endpoints 

POST /assignment : Cria uma tarefa, retorna ObjectId. \
GET /assignment : Obtém todas as tarefas. \
GET /assignment/id : Obtém tarefa por id. \
DELETE /assignment/id : Remove tarefa por id. \
PATCH /assignment/id : Atualiza tarefa (PATCH para atualização parcial). \
PATCH /assignment/id/comments : Adiciona comentários à tarefa. 
___
POST /projects : Cria um projeto.  
GET /projects : Obtém todos os projetos. \
DELETE /projects/id : Remove projeto por id. \
GET /projects/id/assignments : Obtém tarefas vinculadas a um projeto. 

___
POST /users : Cria um usuário.\
GET /users/id/projects : Obtém projetos vinculados a um usuário.\
GET /users/managerUserId/id/assignments/report : Obtém relatório de atividades nos últimos 30 dias.

# Histórico de alterações

Toda criação, update, delete e comentário adicionado na tarefa, é guardado em uma tabela do banco, dessa forma temos o tracking completo do documento desde sua criação.
 
