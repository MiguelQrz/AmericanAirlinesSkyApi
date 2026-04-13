namespace AmericanAirlinesApi.Models;

public class Tripulante
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Funcao { get; set; }
    public required string NumeroLicenca { get; set; }

}