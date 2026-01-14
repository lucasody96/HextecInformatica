namespace HextecInformatica.Entities.Core
{
    public abstract class Pessoa(string nome, string tipoPessoa, string cpf = "", string cnpj = "") : Entity
    {
        public string Nome { get; set; } = nome;
        public string TipoPessoa { get; set; } = tipoPessoa;
        public string Cpf { get; set; } = cpf;
        public string Cnpj { get; set; } = cnpj;

        public abstract string MensagemBoasVindas();
    }
}
