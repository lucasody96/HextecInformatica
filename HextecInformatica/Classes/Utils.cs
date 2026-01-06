using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HextecInformatica.Classes
{
    public class Utils
    {
        // Largura padrão para manter tudo alinhado (pode ajustar para 80 ou 100)
        private const int LarguraPadrao = 80;
        public Utils()
        {

        }

        public int EvitaQuebraCodInt(string mensagem)
        {
            int numInteiro;

            Console.Write(mensagem);

            while (!int.TryParse(Console.ReadLine(), out numInteiro))
            {
                Console.Write("Erro: Valor inválido (Informe apenas números inteiros) \n\n");
                Console.Write(mensagem);
            }
            return numInteiro;
        }

        public double EvitaQuebraCodFloat(string mensagem)
        {
            double numFloat;

            Console.Write(mensagem);

            while (!double.TryParse(Console.ReadLine(), out numFloat))
            {
                Console.Write("Erro: Valor inválido , não é permitido informar letras e deve ser informado algum valor\n\n");
                Console.Write(mensagem);
            }

            return numFloat;
        }

        public decimal EvitaQuebraCodDecimal(string mensagem)
        {
            decimal numDecimal; // 1. Mudamos a variável para decimal

            Console.Write(mensagem);

            // 2. Usamos decimal.TryParse
            while (!decimal.TryParse(Console.ReadLine(), out numDecimal))
            {
                Console.Write("Erro: Valor inválido. Não é permitido informar letras e deve ser informado algum valor.\n\n");
                Console.Write(mensagem);
            }

            return numDecimal;
        }

        public void FormataCabecalho(string texto, char caractereBorda = '=')
        {
            string linhaSeparadora = new string(caractereBorda, LarguraPadrao);
            Console.WriteLine(linhaSeparadora);

            // Centraliza o texto
            int espacosTotais = LarguraPadrao - texto.Length;
            int espacosEsquerda = espacosTotais / 2;
            // O PadRight garante que complete a linha caso a divisão seja ímpar
            string textoCentralizado = texto.PadLeft(espacosEsquerda + texto.Length).PadRight(LarguraPadrao);

            Console.WriteLine(textoCentralizado);
            Console.WriteLine(linhaSeparadora);
        }

        // linhas divisórias simples
        public void ImprimeLinhaSeparadora(char caractere = '-')
        {
            Console.WriteLine(new string(caractere, LarguraPadrao));
        }

        // O SEGREDO DO GRID: Este método aceita colunas dinâmicas
        // Exemplo de uso: FormataLinhaTabela("COD", "NOME", "PRECO");
        // O sinal negativo (-) alinha à esquerda. O positivo alinha à direita.
        public void FormataLinhaProdutos(int id, string nome, decimal valor, int estoque)
        {
            // Definição das larguras:
            // ID: 5 caracteres | Nome: 35 caracteres | Valor: 15 caracteres | Estoque: Restante

            string linha = string.Format("| {0, -6} | {1, -35} | {2, 12} | {3, -7} |",
                                         id,
                                         nome.Length > 35 ? nome.Substring(0, 32) + "..." : nome, // Corta nome se for muito longo
                                         valor.ToString("C"), // Formata como dinheiro
                                         estoque);

            // Preenche o resto da linha com espaços até bater a largura padrão
            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
        }

        // Sobrecarga para o Cabeçalho da Tabela (apenas textos)
        public void FormataCabecalhoTabela()
        {
            string linha = string.Format("| {0, -6} | {1, -35} | {2, -12} | {3, -7} |",
                                         "CÓDIGO", "DESCRIÇÃO PRODUTO", "VALOR UNIT", "ESTOQUE");

            ImprimeLinhaSeparadora('-');
            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
            ImprimeLinhaSeparadora('-');
        }
    }
}
