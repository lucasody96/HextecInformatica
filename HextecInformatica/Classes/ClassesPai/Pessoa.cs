namespace HextecInformatica.Classes.ClassesPai
{
    public class Pessoa(string nome, string tipoPessoa, string cpf = "", string cnpj = "")
    {
        public string Nome { get; set; } = nome;
        public string TipoPessoa { get; set; } = tipoPessoa;
        public string Cpf { get; set; } = cpf;
        public string Cnpj { get; set; } = cnpj;

        public virtual string MensagemBoasVindas()
        {
            return $"";
        }
    }
}
