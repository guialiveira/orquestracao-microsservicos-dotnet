var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.VollMed_Consultas>("vollmed-consultas");

builder.AddProject<Projects.VollMed_Gateway>("vollmed-gateway");

builder.AddProject<Projects.VollMed_Medicos>("vollmed-medicos");

builder.AddProject<Projects.VollMed_Pacientes>("vollmed-pacientes");

builder.Build().Run();
