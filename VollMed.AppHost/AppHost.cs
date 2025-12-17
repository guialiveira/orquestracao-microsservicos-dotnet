var builder = DistributedApplication.CreateBuilder(args);

var apiConsultas = builder.AddProject<Projects.VollMed_Consultas>("vollmed-consultas");

var apiMedicos = builder.AddProject<Projects.VollMed_Medicos>("vollmed-medicos");

var apiPacientes = builder.AddProject<Projects.VollMed_Pacientes>("vollmed-pacientes");

builder.AddProject<Projects.VollMed_Gateway>("vollmed-gateway")
    .WithReference(apiConsultas)
    .WithReference(apiPacientes)
    .WithReference(apiMedicos);

builder.Build().Run();
