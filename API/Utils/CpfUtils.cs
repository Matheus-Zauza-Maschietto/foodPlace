namespace API.Utils;

public static class CpfUtils
{
    public static bool Validar(string cpf)
    {
        cpf = String.RemoverFormatacao(cpf, new string[] { " ", "-", "." });

        if (cpf.Length != 11 || !VerificarDigitosIguais(cpf))
        {
            return false;
        }

        int primeiroDigitoVerificador = 9, segundoDigitoVerificador = 10;

        return _verificarDigitoVerificador(primeiroDigitoVerificador, cpf) && _verificarDigitoVerificador(segundoDigitoVerificador, cpf);
    }

    private static bool _verificarDigitoVerificador(int rodadas, string cpf)
    {
        string digitos = cpf.Substring(0, rodadas);
        string verificador = cpf.Substring(rodadas, 1);
        int soma = 0;
        int digito;
        
        for (int i = 0; i < rodadas; i++)
        {
            digito = int.Parse(digitos[i].ToString());
            soma += digito * ((rodadas+1) - i);
        }

        int resto = soma % 11;

        int digitoVerificador = resto < 2 ? 0 : 11 - soma % 11;
        return  digitoVerificador.ToString() == verificador;
    }

    private static bool VerificarDigitosIguais(string cpf)
    {
        for (int i = 1; i < cpf.Length; i++)
        {
            if (cpf[i] != cpf[0])
            {
                return true;
            }
        }

        return false;
    }

    public static string Formatar(string cpf)
    {
        cpf = String.RemoverFormatacao(cpf, new string[] { " ", ".", "-" });

        if (cpf.Length != 11)
        {
            return cpf;
        }

        string cpfFormatado = cpf.Substring(0, 3)+"."+cpf.Substring(3, 3)+"."+cpf.Substring(6, 3)+"-"+cpf.Substring(9, 2);
        return cpfFormatado;
    }
}
