namespace Paradigmi.Models.Entities
{

    public enum Ruolo
    {
        Cliente,
        Amministratore


    }

    public static class RuoloExtensions
    {
        public static Ruolo asRuolo(string ruolo)
        {
            if (ruolo.ToLower().Equals("cliente".ToLower()))
                return Ruolo.Cliente;
            if (ruolo.ToLower().Equals("amministratore".ToLower()))
                return Ruolo.Amministratore;
            throw new ArgumentException(" ruolo non valido: " + ruolo);
        }
    }
}