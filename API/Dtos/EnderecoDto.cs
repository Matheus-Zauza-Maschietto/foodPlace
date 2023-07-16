namespace API.Dtos
{
    public class EnderecoDto
    {
        public int Estado { get; set; }
        public int Cidade { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
    }
}
