namespace Paradigmi.Models.Entities
{
    public enum Tipologia
    {
        Antipasto,
        Primo,
        Secondo,
        Contorno,
        Dolce,
        Bevande
    }
    
    

    public static class TipologiaExtensions
    {
        public static Tipologia AsTipologia(string tipo)
        {
            if (tipo.ToLower().Equals("antipasto".ToLower()))
                return Tipologia.Antipasto;
            if (tipo.ToLower().Equals("primo".ToLower()))
                return Tipologia.Primo;
            if (tipo.ToLower().Equals("secondo".ToLower()))
                return Tipologia.Secondo;
            if (tipo.ToLower().Equals("contorno".ToLower()))
                return Tipologia.Contorno;
            if (tipo.ToLower().Equals("dolce".ToLower()))
                return Tipologia.Dolce;
            if (tipo.ToLower().Equals("bevande".ToLower()))
                return Tipologia.Bevande;
            throw new ArgumentException(" tipologia non valida: " + tipo);
        }
        
    }
}