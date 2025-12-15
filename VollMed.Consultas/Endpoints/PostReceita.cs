using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VollMed.Consultas.Data;
using VollMed.Shared.Eventos;

namespace VollMed.Consultas.Endpoints
{
    public class PostReceita
    {
        public record PostReceitaRequest(
            [Required] long ConsultaId, 
            [Required] string Receita);

        public record PostReceitaResponse(
            [Required] long ConsultaId,
            [Required] string Receita);

        public static async Task<IResult> Handle(
            PostReceitaRequest request,
            VollMedDbContext context,
            IPublishEndpoint publishEndpoint
            )
        {
            var consulta = await context.Consultas
                .FirstOrDefaultAsync(c => c.Id == request.ConsultaId);

            if (consulta is null) return Results.NotFound();

            consulta.Receita = request.Receita;
            await context.SaveChangesAsync();

            // envio do email
            var evento = new ReceitaCriadaEvent
            {
                ConsultaId = consulta.Id,
                PacienteId = consulta.PacienteId,
                MedicoId = consulta.MedicoId,
                Receita  = consulta.Receita
            };
            await publishEndpoint.Publish(evento);

            return Results.Ok(new PostReceitaResponse(consulta.Id, consulta.Receita));
        }
    }
}
