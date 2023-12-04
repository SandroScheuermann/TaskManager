# Execução do projeto

Abrir algum prompt de comando na pasta raiz, executar o comando "docker-compose up", a API vai estar disponível no localhost:8080, e o banco no localhost:27017. Deixei o swagger habilitado em release, para facilitar os testes.
 

# Uso dos endpoints 

**Em alguns endpoints em que eu usaria informações do token JWT, por exemplo, para linkar informações do usuário, utilizei o próprio Id da entidade User.

POST /assignment : Usado na criação de tarefas, retorna o ObjectId gerado pelo mongodb. \
GET /assignment : Obtém todas as tarefas. \
GET /assignment/id : Obtém tarefas por id. \
DELETE /assignment/id : ... \
PATCH /assignment/id : Atualiza informações da tarefa, como é um PATCH, atualiza o que for enviado.\
PATCH /assignment/id/comments : Adiciona comentários a uma tarefa \
___
POST /projects : Insere um novo projeto. \  
GET /projects : Obtém todas os projetos. \
DELETE /projects/id : ... \
GET /projects/id/assignments : Obtém todas as tarefas linkadas a um projeto específico\

___
POST /users : Insere um novo usuário.\
GET /users/id/projects : Obtém todos os projetos linkados a um usuário específico.\
GET /users/managerUserId/id/assignments/report : Obtém o relatório de atividades concluídas nos últimos 30 dias. Nesse caso fiz o endpoint precisando de dois ids, um autenticador, e o id específico , por conta do token JWT que citei no início.

# Questões para a fase de refinamento

- Devemos manter os dados no banco após o "dono" deles ter sido removido? (Ex : Ao remover um projeto, as tarefas linkadas devem permanecer no banco?)
- Como a aplicação deve agir quando uma tarefa expira?
- Quando uma tarefa é finalizada, o status dela pode ser alterado novamente?
- No requisito : "Remoção de tarefas - remover uma tarefa de um projeto", atulamente, a API se comporta deletando a tarefa, tendo em vista que não faz sentido ter uma tarefa sem projeto, seguimos nessa linha, ou é necessário somente remover o link entre as entidades?
