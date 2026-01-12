namespace HextecInformatica.Classes.ClassesPai
{
    public class Pessoa
    {
        public string Nome { get;  set; }
        public string TipoPessoa { get;  set; }
        public string Cpf { get;  set; }
        public string Cnpj { get; set; }

        public Pessoa(string nome, string tipoPessoa, string cpf = "", string cnpj = "")
        {
            Nome = nome;
            TipoPessoa = tipoPessoa;
            Cpf = cpf;
            Cnpj = cnpj;
        }

    }
}
