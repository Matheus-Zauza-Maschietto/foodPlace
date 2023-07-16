namespace API.Utils;

public static class CnpjUtils
{
    public static bool Validar(string cnpj)
    {
        cnpj = String.RemoverFormatacao(cnpj, new string[] { " ", "-", ".", "/" });

        if (cnpj.Length != 14 || !VerificarDigitosIguais(cnpj))
        {
            return false;
        }

        int[] multiplicadoresDigitos1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadoresDigitos2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        return _verificarDigitoVerificador(12, cnpj, multiplicadoresDigitos1) && _verificarDigitoVerificador(13, cnpj, multiplicadoresDigitos2);
    }

    private static bool _verificarDigitoVerificador(int rodadas, string cnpj, int[] multiplicador)
    {
        string digitos = cnpj.Substring(0, rodadas);
        string verificador = cnpj.Substring(rodadas, 1);
        int soma = 0;
        int digito;

        for (int i = 0; i < rodadas; i++)
        {
            digito = int.Parse(digitos[i].ToString());
            soma += (digito * (multiplicador[i]));
        }

        int resto = soma % 11;
        int digitoVerificador = 11 - resto;

        return digitoVerificador.ToString() == verificador;
    }

    private static bool VerificarDigitosIguais(string cnpj)
    {
        for (int i = 1; i < cnpj.Length; i++)
        {
            if (cnpj[i] != cnpj[0])
            {
                return true;
            }
        }

        return false;
    }
}
