namespace API.Utils;

public static class TelefoneUtils
{
    public static bool Validar(string telefone)
    {
        telefone = String.RemoverFormatacao(telefone, new string[] { " ", "-", "(", ")" });

        if (telefone.Length <= 10 || telefone.Length > 11)
            return false;
        return true;
    }

    public static string Formatar(string telefone)
    {
        telefone = String.RemoverFormatacao(telefone, new string[] { " ", "-", "(", ")" });

        if (telefone.Length != 11)
        {
            return telefone;
        }

        return "(" + telefone.Substring(0, 2)+") " + telefone.Substring(2, 5) + " " + telefone.Substring(5, 4);
    }
}
