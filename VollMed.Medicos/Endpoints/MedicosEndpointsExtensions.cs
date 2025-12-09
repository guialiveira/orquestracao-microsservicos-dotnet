namespace VollMed.Medicos.Endpoints
{
    public static class MedicosEndpointsExtensions
    {
        public static WebApplication MapMedicosEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/medicos")
                .WithTags("Médicos");

            group.MapGet("", GetMedicos.Handle)
                .WithName("ListarMedicos")
                .Produces<VollMed.Shared.Pagination.PaginatedList<GetMedicos.MedicoListResponse>>();

            group.MapGet("{id}", GetMedicoById.Handle)
                .WithName("ObterMedico")
                .Produces<GetMedicoById.MedicoDetailResponse>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("", PostMedico.Handle)
                .WithName("CriarMedico")
                .Produces<PostMedico.CreateMedicoResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("{id}", PutMedico.Handle)
                .WithName("AtualizarMedico")
                .Produces<PutMedico.UpdateMedicoResponse>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("{id}", DeleteMedico.Handle)
                .WithName("ExcluirMedico")
                .Produces(StatusCodes.Status204NoContent);

            group.MapGet("/especialidades/{especialidade}", GetMedicosByEspecialidade.Handle)
                .WithName("ListarMedicosPorEspecialidade")
                .Produces<List<GetMedicosByEspecialidade.MedicosByEspecialidadeResponse>>()
                .Produces(StatusCodes.Status400BadRequest);

            return app;
        }
    }
}
