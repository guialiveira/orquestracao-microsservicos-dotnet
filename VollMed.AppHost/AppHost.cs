var builder = DistributedApplication.CreateBuilder(args);

// keycloak + postgres
var pgUsername = builder.AddParameter("pgUsername", "keycloak");
var pgPassword = builder.AddParameter("pgPassword", "password");

var postgres = builder
    .AddPostgres("postgres", port: 5432, userName: pgUsername, password: pgPassword)
    .WithImageTag("15")
    .WithEnvironment("POSTGRES_DB", "keycloak")
    .WithDataVolume("keycloak-pgdata")
    .WithLifetime(ContainerLifetime.Persistent);

var keycloakDb = postgres.AddDatabase("keycloakDB");

var keycloak = builder.AddKeycloak("keycloak", port: 8080)
    .WithEnvironment("KC_DB", "postgres")
    .WithEnvironment("KC_DB_URL", $"jdbc:postgresql://postgres:5432/keycloakDB")
    .WithEnvironment("KC_DB_USERNAME", "keycloak")
    .WithEnvironment("KC_DB_PASSWORD", "password")
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
    .WithEnvironment("KC_BOOTSTRAP_ADMIN_PASSWORD", "admin")
    .WithDataVolume("keycloak-pgdata")
    .WaitFor(postgres);

// sqlserver
var sqlServer = builder
    .AddSqlServer("sqlserver")
    .WithImageTag("2022-latest")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var consultasDB = sqlServer.AddDatabase("consultasDB");
var medicosDB = sqlServer.AddDatabase("medicosDB");
var pacientesDB = sqlServer.AddDatabase("pacientesDB");

// rabbitmq
var rabbitMQ = builder
    .AddRabbitMQ("rabbitmq", port: 5672)
    .WithManagementPlugin(port: 15672);

// apis
var apiConsultas = builder.AddProject<Projects.VollMed_Consultas>("vollmed-consultas")
    .WithReference(consultasDB)
    .WithReference(rabbitMQ);

var apiMedicos = builder.AddProject<Projects.VollMed_Medicos>("vollmed-medicos")
    .WithReference(medicosDB);

var apiPacientes = builder.AddProject<Projects.VollMed_Pacientes>("vollmed-pacientes")
    .WithReference(pacientesDB)
    .WithReference(rabbitMQ);

// gateway
builder.AddProject<Projects.VollMed_Gateway>("vollmed-gateway")
    .WithReference(apiConsultas)
    .WithReference(apiPacientes)
    .WithReference(apiMedicos)
    .WithReference(keycloak);

builder.Build().Run();
