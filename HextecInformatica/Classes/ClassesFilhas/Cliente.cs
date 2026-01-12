using HextecInformatica.Classes.ClassesPai;

namespace HextecInformatica.Classes.ClassesFilhas
{
    public class Cliente : Pessoa
    {
        public decimal DescProximaCompra { get; private set; }

        public Cliente(string nome) : base(nome, "", "", "")
        {
           
        }

        public void DadosCliente() 
        {
            bool tipoPessoaCorreto = false;
            while (!tipoPessoaCorreto)
            {
                Console.Write("Trata-se de um cliente pessoa física (F) ou pessoa jurídica (J): ");
                TipoPessoa = Console.ReadLine().ToUpper();

                if (TipoPessoa == "F")
                {
                    bool cpfCorreto = false;
                    do
                    {
                        Console.Write("\nInforme o seu CPF: ");
                        Cpf = Console.ReadLine();

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
                        Cnpj = Console.ReadLine();

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
    }
}
