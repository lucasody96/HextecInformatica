namespace HextecInformatica.Entities
{
    public class Utils
    {
        // Largura padrão para manter tudo alinhado (pode ajustar para 80 ou 100)
        private const int LarguraPadrao = 80;

        public static int EvitaQuebraCodInt(string mensagem)
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

        public static double EvitaQuebraCodFloat(string mensagem)
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

        public static decimal EvitaQuebraCodDecimal(string mensagem)
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

        public static void FormataCabecalho(string texto, char caractereBorda = '=')
        {
            string linhaSeparadora = new(caractereBorda, LarguraPadrao);
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
        public static void ImprimeLinhaSeparadora(char caractere)
        {
            Console.WriteLine(new string(caractere, LarguraPadrao));
        }

        // O SEGREDO DO GRID: Este método aceita colunas dinâmicas
        // Exemplo de uso: FormataLinhaTabela("COD", "NOME", "PRECO");
        // O sinal negativo (-) alinha à esquerda. O positivo alinha à direita.
        public static void FormataLinhaProdutos(int id, string nome, decimal valor, int estoque)
        {
            // Definição das larguras:
            // ID: 5 caracteres | Nome: 35 caracteres | Valor: 15 caracteres | Estoque: Restante

            string linha = string.Format("| {0, -6} | {1, -35} | {2, 12} | {3, -7} |",
                                         id,
                                         nome.Length > 35 ? string.Concat(nome.AsSpan(0, 32), "...") : nome, // Corta nome se for muito longo
                                         valor.ToString("C"), // Formata como dinheiro
                                         estoque);

            // Preenche o resto da linha com espaços até bater a largura padrão
            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
        }

        // Sobrecarga para o Cabeçalho da Tabela (apenas textos)
        public static void FormataCabecalhoTabela()
        {
            string linha = string.Format("| {0, -6} | {1, -35} | {2, -12} | {3, -7} |",
                                         "CÓDIGO", "DESCRIÇÃO PRODUTO", "VALOR UNIT", "ESTOQUE");

            ImprimeLinhaSeparadora('-');
            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
            ImprimeLinhaSeparadora('-');
        }

        public static void ImprimeDetalheFinanceiro(string texto, decimal valor, bool ehDesconto = false)
        {
            string textoComPontos = texto.PadRight(62, '.'); // Ajusta largura dos pontos
            string valorFormatado = ehDesconto ? $"-{valor,13:C}" : $"{valor,14:C}";
            Console.WriteLine($"| {textoComPontos}{valorFormatado} |");
        }

        public static string LerSenhaComAsterisco()
        {
            string senha = "";
            ConsoleKeyInfo key;

            do
            {
                // Lê a tecla pressionada, o 'true' impede que ela apareça na tela
                key = Console.ReadKey(true);

                // Se não for Backspace e não for Enter, adiciona a letra e imprime *
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    senha += key.KeyChar;
                    Console.Write("*");
                }
                // Se for Backspace e tiver algo digitado, apaga o último caractere
                else if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
                {
                    senha = senha[..^1];
                    Console.Write("\b \b"); // O truque para apagar visualmente no Console
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Pula uma linha no final
            return senha;
        }

        public static void FormataCabecalhoTabelaClientes()
        {
            // Ajustamos os tamanhos: ID(4), Nome(30), Tipo(4), Documento(18)
            string linha = string.Format("| {0, -4} | {1, -30} | {2, -4} | {3, -18} |",
                                         "ID", "NOME DO CLIENTE", "TIPO", "CPF / CNPJ");

            ImprimeLinhaSeparadora('-');
            // O PadRight garante que a linha vá até o final da largura padrão (80)
            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
            ImprimeLinhaSeparadora('-');
        }

        public static void FormataLinhaCliente(int id, string nome, string tipo, string documento)
        {
            // Garante que se o documento vier null, não quebra (vira vazio)
            string docExibir = documento ?? "";

            // Formatação específica para CPF (11 digitos) e CNPJ (14 digitos)
            // Se quiser simplificar, pode remover esse bloco e passar o documento direto
            if (docExibir.Length == 11 && long.TryParse(docExibir, out long cpfNum))
                docExibir = cpfNum.ToString(@"000\.000\.000\-00");
            else if (docExibir.Length == 14 && long.TryParse(docExibir, out long cnpjNum))
                docExibir = cnpjNum.ToString(@"00\.000\.000\/0000\-00");

            // Monta a linha usando os mesmos tamanhos do cabeçalho
            string linha = string.Format("| {0, -4} | {1, -30} | {2, -4} | {3, -18} |",
                                         id,
                                         nome.Length > 30 ? nome.Substring(0, 27) + "..." : nome, // Corta nome longo
                                         tipo,
                                         docExibir);

            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
        }
    }
}
