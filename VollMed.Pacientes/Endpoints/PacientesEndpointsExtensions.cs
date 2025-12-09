namespace VollMed.Pacientes.Endpoints
{
    public static class PacientesEndpointsExtensions
    {
        public static WebApplication MapPacientesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/pacientes")
                .WithTags("Pacientes");

            group.MapGet("", GetPacientes.Handle)
                .WithName("ListarPacientes")
                .Produces<VollMed.Shared.Pagination.PaginatedList<GetPacientes.PacienteListResponse>>();

            group.MapGet("{id}", GetPacienteById.Handle)
                .WithName("ObterPaciente")
                .Produces<GetPacienteById.PacienteDetailResponse>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("", PostPaciente.Handle)
                .WithName("CriarPaciente")
                .Produces<PostPaciente.CreatePacienteResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("{id}", PutPaciente.Handle)
                .WithName("AtualizarPaciente")
                .Produces<PutPaciente.UpdatePacienteResponse>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("{id}", DeletePaciente.Handle)
                .WithName("ExcluirPaciente")
                .Produces(StatusCodes.Status204NoContent);

            group.MapPost("importarlote", PostImportarLote.Handle)
                .WithName("ImportarLotePacientes")
                .Produces<PostImportarLote.Response>();

            return app;
        }
    }
}
