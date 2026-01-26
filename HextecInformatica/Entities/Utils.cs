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

        public static void FormataCabecalhoColaborador()
        {
            // REMOVI a barra "|" do final desta string de formatação.
            // O sistema vai preencher com espaços até a largura padrão e fechar com a barra depois.
            string linha = string.Format("| {0, -6} | {1, -30} | {2, -15} | {3, -16}",
                                         "ID", "NOME", "LOGIN", "CPF");

            ImprimeLinhaSeparadora('='); // Usei '=' pois parece ser o padrão do topo da sua imagem
            Console.WriteLine(linha.PadRight(Utils.LarguraPadrao - 1) + "|");
            ImprimeLinhaSeparadora('-');
        }
        public static void FormataLinhaColaborador(int id, string nome, string login, string cpf)
        {
            // Tratamento CPF
            string cpfExibir = cpf ?? "";
            if (cpfExibir.Length == 11 && long.TryParse(cpfExibir, out long cpfNum))
                cpfExibir = cpfNum.ToString(@"000\.000\.000\-00");

            // Tratamento Nome (Truncar)
            string nomeExibir = nome ?? "";
            if (nomeExibir.Length > 30)
                nomeExibir = nomeExibir.Substring(0, 27) + "...";

            // Tratamento Login (Truncar)
            string loginExibir = login ?? "";
            if (loginExibir.Length > 15)
                loginExibir = loginExibir.Substring(0, 12) + "...";

            // REMOVI a barra "|" do final aqui também.
            string linha = string.Format("| {0, -6} | {1, -30} | {2, -15} | {3, -16}",
                                         id,
                                         nomeExibir,
                                         loginExibir,
                                         cpfExibir);

            // O PadRight preenche o espaço vazio e adicionamos a barra final manual
            Console.WriteLine(linha.PadRight(Utils.LarguraPadrao - 1) + "|");
        }
        public static void FormataCabecalhoVendas()
        {
            // Apenas inicia a tabela visualmente
            ImprimeLinhaSeparadora('=');
            string titulo = "| LISTAGEM DE VENDAS (RESUMO FINANCEIRO)";
            Console.WriteLine(titulo.PadRight(LarguraPadrao - 1) + "|");
            ImprimeLinhaSeparadora('=');
        }

        public static void FormataLinhaVenda(int nota, DateTime data, string cliente, decimal total, decimal frete, decimal desconto)
        {
            // LINHA 1: Identificação (Nota, Data e Cliente)
            // Estratégia: Nota e Data fixos à esquerda, Cliente ocupa o resto
            string parte1 = string.Format(" NOTA: {0} | DATA: {1}", nota, data.ToString("dd/MM/yyyy"));

            // Tratamento do Cliente (Preenche o resto da linha)
            int espacoRestante = LarguraPadrao - parte1.Length - 5; // -5 para bordas e espaços
            string clienteExibir = cliente.Length > espacoRestante ? cliente.Substring(0, espacoRestante - 3) + "..." : cliente;

            string linha1 = string.Format("|{0} | CLIENTE: {1}", parte1, clienteExibir);

            // LINHA 2: Valores Financeiros (Total, Frete, Desconto)
            // Estratégia: Espaçamento fixo para ficar alinhado
            string linha2 = string.Format("| TOTAL: {0, -12} | FRETE: {1, -10} | DESC.: {2, -10}",
                                          total.ToString("C"),
                                          frete.ToString("C"),
                                          desconto.ToString("C"));

            // IMPRESSÃO DO BLOCO
            Console.WriteLine(linha1.PadRight(LarguraPadrao - 1) + "|");
            Console.WriteLine(linha2.PadRight(LarguraPadrao - 1) + "|");

            // Separador entre uma venda e outra (usar traço simples para não poluir)
            ImprimeLinhaSeparadora('-');
        }

        public static void FormataCabecalhoItens()
        {
            // Cabeçalho interno para quando você detalhar uma venda
            string linha = string.Format("| {0, -4} | {1, -35} | {2, -5} | {3, -12} | {4, -12}",
                                         "ID", "PRODUTO", "QTD", "UNITÁRIO", "TOTAL");

            Console.WriteLine("".PadRight(LarguraPadrao, ' ')); // Linha vazia para respiro
            Console.WriteLine("| ITENS DA VENDA:".PadRight(LarguraPadrao - 1) + "|");
            ImprimeLinhaSeparadora('-');
            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
            ImprimeLinhaSeparadora('-');
        }

        public static void FormataLinhaItem(int id, string produto, int qtd, decimal valorUnitario)
        {
            decimal valorTotalItem = qtd * valorUnitario;

            // Trunca nome do produto se for muito grande
            if (produto.Length > 35) produto = produto.Substring(0, 32) + "...";

            string linha = string.Format("| {0, -4} | {1, -35} | {2, -5} | {3, -12} | {4, -12}",
                                         id,
                                         produto,
                                         qtd,
                                         valorUnitario.ToString("C"),
                                         valorTotalItem.ToString("C"));

            Console.WriteLine(linha.PadRight(LarguraPadrao - 1) + "|");
        }

        public static void ExibirItensDaVenda(Venda venda)
        {
            Console.WriteLine();
            Console.WriteLine("   ITENS DESTA VENDA:");
            Console.WriteLine("   " + new string('-', 70)); // Linha menorzinha decorativa

            // Cabeçalho dos Itens
            // QTD | PRODUTO | V.UNIT | TOTAL
            string cabecalho = string.Format("   | {0, -4} | {1, -30} | {2, -12} | {3, -12} |",
                                             "QTD", "PRODUTO", "V.UNIT", "TOTAL");
            Console.WriteLine(cabecalho);
            Console.WriteLine("   " + new string('-', 70));

            // Loop dos Itens
            foreach (var item in venda.ListaItensVenda)
            {
                // NOTA: Estou assumindo que seu objeto 'item' tem a propriedade 'QuantidadeComprada'.
                // Se na sua classe Produto a propriedade for apenas 'Quantidade', ajuste abaixo.

                decimal totalItem = item.Valor * item.QuantidadeComprada;

                string linha = string.Format("   | {0, -4} | {1, -30} | {2, -12} | {3, -12} |",
                                             item.QuantidadeComprada,
                                             item.Descricao.Length > 30 ? item.Descricao.Substring(0, 27) + "..." : item.Descricao,
                                             item.Valor.ToString("C"),
                                             totalItem.ToString("C"));

                Console.WriteLine(linha);
            }
            Console.WriteLine("   " + new string('-', 70));
            Console.WriteLine();
        }
    }
}
