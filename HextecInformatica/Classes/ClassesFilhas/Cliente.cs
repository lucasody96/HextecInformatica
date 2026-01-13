using HextecInformatica.Classes.ClassesPai;

namespace HextecInformatica.Classes.ClassesFilhas
{
    public class Cliente : Pessoa
    {
        public decimal DescProximaCompra { get; private set; }

        public Cliente(string nome) : base(nome, "", "", "")
        {
            Nome = nome;
        }

        public void DadosCliente() 
        {
            bool tipoPessoaCorreto = false;
            while (!tipoPessoaCorreto)
            {
                Console.Write("Trata-se de um cliente pessoa física (F) ou pessoa jurídica (J): ");
                var input = Console.ReadLine();
                TipoPessoa = input != null ? input.ToUpper() : string.Empty;

                if (TipoPessoa == "F")
                {
                    bool cpfCorreto = false;
                    do
                    {
                        Console.Write("\nInforme o seu CPF: ");
                        Cpf = Console.ReadLine() ?? string.Empty;

                        if (Cpf.Length == 11)
                            cpfCorreto = true;
                        else
                            Console.WriteLine("CPF informado incorreto, digite um CPF válido");

                    } while (!cpfCorreto);
                    
                    tipoPessoaCorreto = true; 
                    
                } else if (TipoPessoa == "J")
                {
                    bool cnpjCorreto = false;
                    do
                    {
                        Console.Write("\nInforme o seu CNPJ: ");
                        Cnpj = Console.ReadLine() ?? string.Empty;

                        if (Cnpj.Length == 14)
                            cnpjCorreto = true;
                        else
                            Console.WriteLine("CNPJ informado incorreto, digite um CNPJ válido");

                    } while (!cnpjCorreto);

                    tipoPessoaCorreto = true;
                } else
                    Console.WriteLine("\nValor informado inválido, informar F para pessoa física ou J para pessoa jurídica");
            }

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
