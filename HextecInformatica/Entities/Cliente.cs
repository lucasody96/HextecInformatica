using HextecInformatica.Entities.Core;

namespace HextecInformatica.Entities
{
    public class Cliente : Pessoa
    {
        private static int _contadorClientes = 0;   
        public decimal DescProximaCompra { get; private set; }

        public Cliente(string nome, string tipoPessoa = "F", string cpf = "", string cnpj = "")
            : base(nome, tipoPessoa, cpf, cnpj)
        {
            //geração do ID automático
            _contadorClientes++;
            Id = _contadorClientes;

            Nome = nome;
            TipoPessoa = tipoPessoa;
            Cpf = cpf;
            Cnpj = cnpj;
        }

        public void AdicionarDescontoProximaCompra(decimal valor) 
        {
            DescProximaCompra += Math.Round(valor, 2);
        }

        public void DebitaDescontoProximaCompra(decimal valor)
        {
            DescProximaCompra -= Math.Round(valor, 2);
        }

        public override string MensagemBoasVindas()
        {
            return $"\nSeja bem vindo {Nome}! Pressione enter para seguir com a compra.";
        }
    }
}
