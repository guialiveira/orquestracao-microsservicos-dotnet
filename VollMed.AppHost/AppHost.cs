var builder = DistributedApplication.CreateBuilder(args);


var sqlServer = builder
    .AddSqlServer("sqlserver")
    .WithImageTag("2022-latest");

var consultasDB = sqlServer.AddDatabase("consultasDB");
var medicosDB = sqlServer.AddDatabase("medicosDB");
var pacientesDB = sqlServer.AddDatabase("pacientesDB");

var apiConsultas = builder.AddProject<Projects.VollMed_Consultas>("vollmed-consultas")
    .WithReference(consultasDB);

var apiMedicos = builder.AddProject<Projects.VollMed_Medicos>("vollmed-medicos")
    .WithReference(medicosDB);

var apiPacientes = builder.AddProject<Projects.VollMed_Pacientes>("vollmed-pacientes")
    .WithReference(pacientesDB);

builder.AddProject<Projects.VollMed_Gateway>("vollmed-gateway")
    .WithReference(apiConsultas)
    .WithReference(apiPacientes)
    .WithReference(apiMedicos);

builder.Build().Run();
