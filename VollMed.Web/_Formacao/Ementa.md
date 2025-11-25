### Curso 1: ASP.NET Core: Proteja Suas Aplicações Web

**Objetivo Geral**: Capacitar os alunos a implementar segurança robusta em aplicações ASP.NET Core, cobrindo tópicos de autenticação, autorização, gerenciamento de sessões, proteção contra ataques CSRF e XSS, e melhores práticas no uso de tokens e criptografia.

---

#### Aula 1: Introdução à Segurança no ASP.NET Core


- **Configuração de Segurança Inicial**:
  - Adicionar pacotes de segurança ao projeto ASP.NET Core com o `Microsoft.AspNetCore.Authentication` e o `Microsoft.AspNetCore.Authorization`.
  - Criar uma classe de configuração de segurança com `Startup` e `ConfigureServices`, onde serão definidas políticas e requisitos de segurança.
- **[Faça scaffold do Identity em um projeto MVC sem autorização existente](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-8.0&tabs=visual-studio#scaffold-identity-into-an-mvc-project-without-existing-authorization)**
- **Personalizando Configurações de Segurança**:
  - Configurar políticas de autorização usando `AuthorizationPolicyBuilder` e criar políticas de acesso restrito.
  - Criar usuários específicos para testes usando `InMemoryUserStore`.

---

#### Aula 2: Autenticação e Autorização Personalizadas

- **Login e Logout Personalizados**:
  - Criar páginas de login e logout com Razor Pages e personalizar a experiência do usuário.
  - **Configuração de filtros de segurança**: Configurar `AuthenticationScheme` e `CookiePolicy` para customizar os cookies de autenticação.
- **Configurações de HTTP e Filtros de Requisição**:
  - Configurar o `Middleware` de requisições HTTP para evitar a exposição de recursos de front-end não autorizados.
  - Customizar as permissões de acesso aos recursos de CSS e JavaScript para evitar problemas de carregamento.

---

#### Aula 3: Sessões, Cookies e Proteções Contra Ataques

- **Gerenciamento de Sessões e Cookies**:
  - Introduzir o uso de `Session` e `HttpContext` para armazenar dados temporários.
  - Configurar cookies de sessão e o comportamento de autenticação com `CookieAuthenticationOptions`.
- **Proteção Contra Ataques de Sessão e Hijacking**:
  - Configurar cookies com `HttpOnly` e `Secure` para mitigar ataques de hijacking.
  - **Implementar Remember Me**: Configurar o recurso de “Lembrar-me” com o uso de cookies de sessão persistentes.
- **Proteção contra CSRF**:
  - Explicar o ataque CSRF e configurar `Antiforgery` para gerar tokens de segurança.
  - Personalizar o uso de tokens CSRF com `IAntiforgery` em formulários para melhorar a segurança.

---

#### Aula 4: Autenticação Baseada em Banco de Dados e Criptografia

- **Armazenamento de Usuários no Banco de Dados**:
  - Configurar o Identity para utilizar o banco de dados com `EntityFrameworkCore` e ASP.NET Core Identity.
  - Implementar as interfaces `IUserStore` e `IRoleStore` para gerenciar usuários no banco de dados.
- **Criptografia e Password Hashing**:
  - Configurar `PasswordHasher` para criptografar senhas no banco de dados.
  - Implementar `BCrypt` com ASP.NET Identity para garantir que senhas sejam salvas com criptografia segura.
- **Injeção de Dependência para Gerenciamento de Usuários**:
  - Usar `Dependency Injection` para o serviço de usuário, permitindo a manipulação de dados do usuário com maior controle.

---

#### Aula 5: Autorização de Conteúdo e Controle de Acesso em View

- **Controle de Acesso em Razor Views**:
  - Integrar o controle de autenticação com Razor usando `@if (User.Identity.IsAuthenticated)`.
  - Criar seções de navegação visíveis apenas para usuários autenticados usando `ViewBag` e `User.Claims`.
- **Personalização de Exibição de Dados do Usuário**:
  - Exibir o nome do usuário autenticado no layout com `User.Identity.Name`.
  - Customizar a exibição do perfil do usuário com `UserManager` e `SignInManager`.
- **Criando Métodos de Acesso e Integração com ASP.NET Core Identity**:
  - Definir métodos de acesso para exibir dados do usuário logado, como informações de perfil e permissões, com `ClaimsPrincipal` e `ClaimsIdentity`.




### Curso 2: ASP.NET Core: Crie Perfis e Autorize Requisições

**Objetivo Geral**: Capacitar os alunos a implementar perfis de usuários, personalizar visualizações e autorizar requisições em aplicações ASP.NET Core, utilizando técnicas de autenticação e autorização seguras, integração com banco de dados e boas práticas de segurança.

---

#### Aula 1: Configuração de Usuários e Perfis no ASP.NET Core

- **Transformar Entidades em Usuários**:
  - Integrar a classe de usuário da aplicação com o ASP.NET Identity para permitir que as entidades sejam tratadas como usuários autenticáveis.
  - Discutir como compartilhar identificadores de usuário e entidade para otimizar consultas e evitar junções excessivas no banco de dados.
- **Estratégias para Salvar Usuários**:
  - Explorar opções para criar e salvar usuários no banco de dados, utilizando o `UserManager` e o `RoleManager` para gerenciar dados de perfil e permissões.

---

#### Aula 2: Configuração e Personalização de Perfis de Usuário

- **Classificar Usuários em Perfis (Roles)**:
  - Criar perfis de usuário utilizando o `IdentityRole` e definir permissões específicas para cada tipo de perfil.
  - Configurar o perfil único por usuário e entender como usar múltiplos perfis para futuras evoluções.
- **Autenticação e Obtenção do Usuário Logado**:
  - Utilizar o `User` no `Controller` para obter informações sobre o usuário autenticado e verificar suas permissões.
- **Configuração de Permissões com Políticas e Atributos**:
  - Criar políticas de autorização com `AuthorizationPolicy` e usar atributos como `[Authorize]`, `[Authorize(Roles = "Admin")]`, e `[Authorize(Policy = "CustomPolicy")]`.
  - Aplicar diferentes restrições de URL com `UseAuthorization` no `Startup` para proteger rotas específicas, considerando a hierarquia de permissões.
- **Permissões por perfil**
	- Médicos
		- Consultas (get/delete)
	- Pacientes
		- Medicos (get)
		- Consultas (get/post/put/delete)
	- Atendentes
		- Medicos (get/post/put/delete)
		- Consultas (get/post/put/delete)
		- Pacientes (get/post/put/delete)
---

#### Aula 3: Personalização de Visualizações e Senhas

- **Visualizações Específicas para Cada Perfil**:
  - Configurar visualizações personalizadas utilizando Razor para exibir informações de acordo com o perfil do usuário autenticado.
  - Exibir elementos condicionalmente no front-end com base no perfil do usuário, utilizando `User.Identity` e verificações em Razor.
- **Formulário de Alteração de Senha e Verificação**:
  - Criar um formulário com três campos (senha atual, nova senha e confirmação de nova senha) e implementar lógica para verificar e confirmar a alteração.
  - Utilizar o `PasswordHasher` para verificar a senha atual e criptografar a nova senha antes de salvá-la no banco de dados.
- **Forçando a Alteração de Senha**:
  - Explorar estratégias para solicitar ao usuário que altere a senha, utilizando redirecionamentos e verificações para garantir a segurança contínua da conta.

---

#### Aula 4: Recuperação de Senha com Envio de Token

- **Implementar Lógica para Recuperação de Senha**:
  - Configurar a funcionalidade "Esqueci minha senha", criando um token exclusivo para a redefinição e um prazo de expiração para este token.
- **Controladores e Parâmetros para Redefinição de Senha**:
  - Configurar o `AccountController` para lidar com parâmetros de token, exibir mensagens de erro e personalizar a experiência de redefinição de senha.
- **Envio de E-mails para Recuperação de Senha**:
  - Integrar o envio de e-mails com `SmtpClient` criando mensagens personalizadas para recuperação de senha.

---

#### Aula 5: Redefinição de Senha Segura e Gerenciamento de Tokens

- **Recuperar o Usuário a partir do Token de Redefinição**:
  - Configurar o ASP.NET Core Identity para buscar o usuário correspondente ao token de redefinição e permitir a alteração de senha.
- **Resetar Tokens Após a Redefinição**:
  - Limpar o token e o prazo de expiração após o uso, evitando reutilização e garantindo a integridade do processo de redefinição de senha.
- **Boas Práticas para Gerenciamento de Senhas e Segurança**:
  - Utilizar hashing e o `DataProtectionProvider` para criptografar senhas de forma segura, com `BCrypt` e `PasswordHasher`.

