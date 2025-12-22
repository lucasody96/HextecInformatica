using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Cliente
    {
        public string Nome {  get; private set; }

        public string TipoPessoa { get; private set; }
        public string Cpf {  get; private set; }
        public string Cnpj { get; private set; }

        public Cliente(string nome )
        {
            Nome = nome;
        }
        public void DadosCliente() 
        {
            bool tipoPessoaCorreto = false;
            while (!tipoPessoaCorreto)
            {
                Console.Write("Trata-se de um cliente pessoa física (F) ou pessoa jurídica (J): ");
                TipoPessoa = Console.ReadLine();

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
                            Console.WriteLine("CPF informado incorreto, digite um CPF válido");

                    } while (!cnpjCorreto);

                    tipoPessoaCorreto = true;
                } else
                    Console.WriteLine("\nValor informado inválido, informar F para pessoa física ou J para pessoa jurídica");
            }

        }
    }
}
