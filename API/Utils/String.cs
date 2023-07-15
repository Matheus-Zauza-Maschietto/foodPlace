namespace API.Utils;

public static class String
{
    public static string RemoverFormatacao(string stringInicial, string[] caracteresParaSubstituicao)
    {
        foreach (var caracter in caracteresParaSubstituicao)
            stringInicial = stringInicial.Replace(caracter, string.Empty);
        return stringInicial;
    }
}
