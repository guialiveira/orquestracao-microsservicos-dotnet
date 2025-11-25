### Curso 1: Fundamentos de Segurança em [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)

**Objetivo:** Introduzir os conceitos básicos de segurança em aplicações [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/introduction?view=aspnetcore-8.0), abordando as ameaças e vulnerabilidades mais comuns, bem como a importância de implementar a segurança desde o início do desenvolvimento.

**Conteúdo:**
- **Introdução à Segurança em Aplicações:**
  - O que é segurança em aplicações web? **(Ensine os alunos a entenderem os fundamentos de segurança para aplicar ao projeto de cadastro de médicos e consultas).**
  - Por que a segurança é crucial? **(Explique como a segurança protege informações sensíveis, como dados dos médicos e pacientes).**
  - [Ataques e vulnerabilidades comuns](https://learn.microsoft.com/pt-br/aspnet/core/security/common-web-application-attacks?view=aspnetcore-8.0). **(Identifique possíveis vulnerabilidades ao listar e modificar consultas no projeto).**

- **Conceitos Básicos de Segurança:**
  - [Principais ameaças e vulnerabilidades no desenvolvimento](https://learn.microsoft.com/pt-br/aspnet/core/security/threats?view=aspnetcore-8.0). **(Ensine a identificar ameaças ao cadastrar médicos e consultas).**
  - A importância da segurança desde o início do projeto. **(Explique como integrar segurança no código desde o cadastro inicial de médicos).**
  - OWASP Top 10: introdução às principais vulnerabilidades. **(Revise as principais vulnerabilidades e aplique proteções nas funcionalidades do projeto).**
  - Confidencialidade, integridade e disponibilidade. **(Demonstre como proteger dados ao agendar e listar consultas médicas).**

- **Segurança em C#: Boas Práticas:**
  - Introdução a boas práticas de desenvolvimento seguro em C#. **(Demonstre práticas seguras ao desenvolver as funcionalidades de CRUD para médicos e consultas).**
  - [Sanitização e validação de dados de entrada](https://learn.microsoft.com/pt-br/aspnet/core/security/cross-site-scripting?view=aspnetcore-8.0). **(Ensine a validar e sanitizar dados no cadastro de médicos para evitar ataques).**
  - Estratégias de autenticação e autorização básicas. **(Implemente um login básico para proteger dados de pacientes).**
  - [Segurança e entrada do usuário](https://learn.microsoft.com/pt-br/dotnet/standard/security/security-and-user-input).
  - [IDataProtectionProvider e IDataProtector](https://learn.microsoft.com/pt-br/aspnet/core/security/data-protection/consumer-apis/overview?view=aspnetcore-8.0).
  - [Configurar a proteção de dados do ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-8.0).
  - [Segurança e condições de corrida](https://learn.microsoft.com/pt-br/dotnet/standard/security/security-and-race-conditions).

- **Ferramentas e Bibliotecas Básicas:**
  - Introdução ao uso de scanners de vulnerabilidade. **(Demonstre como realizar testes de segurança no projeto).**
  - [Bibliotecas e ferramentas para criptografia em C#](https://learn.microsoft.com/pt-br/aspnet/core/security/data-protection/introduction?view=aspnetcore-8.0). **(Aplique criptografia para proteger dados médicos e de pacientes).**
  - Segurança em APIs com [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/web-api/?view=aspnetcore-8.0) Core. **(Implemente segurança básica ao acessar dados médicos via API).**

**Objetivos e Aspirações:** Ao concluir este curso, o aluno será capaz de entender os principais conceitos de segurança em aplicações [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0), identificar as ameaças e vulnerabilidades mais comuns e aplicar boas práticas de segurança no desenvolvimento de código C#.

---

### Curso 2: Autenticação e Autorização em [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-8.0)

**Objetivo:** Ensinar a implementar soluções robustas de autenticação e autorização em aplicações [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-8.0), utilizando os recursos do [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-8.0) Security e outras bibliotecas de autenticação.

**Conteúdo:**

- **[ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-8.0) Security:**
  - O que é e por que usar o ASP.NET Security? **(Explique a importância do ASP.NET Security para proteger informações no projeto de gestão médica).**
  - [Arquitetura e funcionamento do framework](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-8.0). **(Demonstre como o framework gerencia autenticação e autorização no projeto).**
  - Integração com o [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-8.0) MVC e Web API. **(Ensine a proteger endpoints de APIs do projeto de consultas médicas).**

- **Autenticação em [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/?view=aspnetcore-8.0):**
  - Configuração básica de autenticação. **(Configure autenticação para proteger o acesso ao cadastro de médicos).**
  - Tipos de autenticação:
    - [Form-based authentication](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/cookie?view=aspnetcore-8.0).
    - Basic authentication.
    - [JWT authentication](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/jwt-bearer?view=aspnetcore-8.0).
    - [OAuth2](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/social/?view=aspnetcore-8.0).
  - Customização do processo de autenticação. **(Demonstre como personalizar autenticação para médicos e pacientes).**
  - [Implementação de logins sociais](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/social/?view=aspnetcore-8.0) (Facebook, Google, etc.). **(Ensine como configurar logins sociais para o projeto de consultas médicas).**

- **Autorização em [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/security/authorization/?view=aspnetcore-8.0):**
  - [Protegendo endpoints e recursos](https://learn.microsoft.com/pt-br/aspnet/core/security/authorization/secure-data?view=aspnetcore-8.0). **(Demonstre como proteger recursos de médicos e pacientes no projeto).**
  - Uso de anotações (AuthorizeAttribute) e verificações baseadas em URL. **(Configure políticas de autorização para limitar acesso no projeto).**
  - [Hierarquia de papéis (Roles)](https://learn.microsoft.com/pt-br/aspnet/core/security/authorization/roles?view=aspnetcore-8.0) e controle de acesso granular. **(Crie papéis de acesso para usuários e administradores no projeto).**
  - Integração com bancos de dados para gerenciamento de permissões. **(Ensine a configurar permissões no banco de dados para o projeto de consultas médicas).**

- **Autenticação e Autorização em APIs REST:**
  - [Boas práticas de segurança em APIs](https://learn.microsoft.com/pt-br/aspnet/core/security/?view=aspnetcore-8.0). **(Explique como proteger APIs para acesso seguro aos dados médicos).**
  - [Autenticação JWT em APIs](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/jwt-bearer?view=aspnetcore-8.0). **(Demonstre o uso de JWT para autenticar acesso à API de consultas).**
  - Implementação de APIs com autorização. **(Configure autorização em APIs que manipulam dados médicos e de consultas).**
  - [Autenticação em ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api#authentication).
  - [Autorização em ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api#authorization).

**Objetivos e Aspirações:** Ao final do curso, o aluno estará apto a implementar soluções de autenticação e autorização robustas em aplicações [ASP.NET](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0), garantindo a segurança e o controle de acesso aos recursos da aplicação de consultas médicas.