using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VollMed.Consultas.Data;

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
            VollMedDbContext context
            )
        {
            var consulta = await context.Consultas
                .FirstOrDefaultAsync(c => c.Id == request.ConsultaId);

            if (consulta is null) return Results.NotFound();

            consulta.Receita = request.Receita;
            await context.SaveChangesAsync();

            return Results.Ok(new PostReceitaResponse(consulta.Id, consulta.Receita));
        }
    }
}
