![build](https://github.com/FrankWendel/webapi-with-background-task/actions/workflows/dotnet.yml/badge.svg?branch=main)

# Web API com Tarefas em background

Essa aplicação é um exemplo onde uma WebAPI recebe requisições de tarefas que precisam rodar em segundo plano, pode ser usado em cenários onde o tempo da tarefa é muito longo e não faz sentido deixar o usuário esperando. Essa implementação é bem simples e tem alguns problemas de escalabilidade e rastreabilidade, para uma solução mais robusta eu aconcelho o uso do [HangFire](https://www.hangfire.io/)

## Exemplo de problema a ser resolvido
Para cada chamada que o usuario fizer ao endoint eu preciso processar, gerar e enviar um arquivo por e-mail. Esse trabalho pode levar algum tempo e usuário não precisa ficar com a tela travada enquanto isso acontece.

## Tecnologia
Essa aplicação foi construida com dotnet 5.0, mesclando os tamplates de webapi e Hosted Service.
- WebAPi: https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0
- Hosted Service: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0

Dentro do código tem diversos comentários explicando cada etapa e ele foi criado com base na seguinte documentação: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio#queued-background-tasks-1
