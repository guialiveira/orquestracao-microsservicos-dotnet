Desenvlvido no curso "Arquitetura distribuída e escalável com .NET: do monolito ao Kubernetes" da Alura.

Criadas com .NET9 as APIs do sistema simulando um agendador de consultas entre medicos e pacientes.
Esse projeto tem uma arquitetura distribuida com microservições.
A ideia inicial foi separar um monolito com (medicos, consultas e pacientes) em diferentes projetos com seus proprios bancos de dados para evitar sobrecarga e lentidão por concorrencia de diferentes endpoints.
a partir desse ponto outros projetos foram criados para garantir a boa comunicação e orquesração entre os mesmos.

O banco é criado atraves de migrations do EF.
O projeto usa mensageria com rabbitmq garantindo uma comunicação assincrona listando eventos (receita medica disponibilizada) para que um email seja enviado para paciente.
O projeto gateway tem como objetivo centralizar as URLs e requisições e usa a ferramenta keycloak para prover segurança com token de autenticação entre as requisições.
A conteinerização está configurada para ser orquestrada com kubernetes.(projeto Host)
OpenTelemetry foi usado para garantir observabilidade atraves do aspire mesmo com o projeto rodando fora da maquina local.


<img width="1912" height="710" alt="Captura de tela 2026-02-06 233852" src="https://github.com/user-attachments/assets/e63fc217-642e-4a99-b20e-26f920183b64" />



