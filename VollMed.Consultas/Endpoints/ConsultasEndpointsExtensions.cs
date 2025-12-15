using Microsoft.AspNetCore.Builder;

namespace VollMed.Consultas.Endpoints
{
    public static class ConsultasEndpointsExtensions
    {
        public static WebApplication MapConsultasEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/consultas")
                .WithTags("Consultas");

            group.MapGet("", GetConsultas.Handle)
                .WithName("ListarConsultas")
                .Produces<VollMed.Shared.Pagination.PaginatedList<GetConsultas.ConsultaListResponse>>();

            group.MapGet("{id}", GetConsultaById.Handle)
                .WithName("ObterConsulta")
                .Produces<GetConsultaById.ConsultaDetailResponse>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("", PostConsulta.Handle)
                .WithName("CriarConsulta")
                .Produces<PostConsulta.CreateConsultaResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("{id}", PutConsulta.Handle)
                .WithName("AtualizarConsulta")
                .Produces<PutConsulta.UpdateConsultaResponse>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("{id}", DeleteConsulta.Handle)
                .WithName("ExcluirConsulta")
                .Produces(StatusCodes.Status204NoContent);

            return app;
        }

        public static WebApplication MapReceitasEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/receitas")
                .WithTags("Receitas");

            group.MapPost("", PostReceita.Handle)
                .WithName("CriarReceita")
                .Produces<PostReceita.PostReceitaResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);

            return app;
        }
    }
}
