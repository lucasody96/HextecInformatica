using HextecInformatica.Interfaces;


namespace HextecInformatica.Entities.Descontos
{
    public class DescontoCashback : IDesconto
    {
        private readonly Cliente? ClienteCashback;

        public DescontoCashback(Cliente? clienteCashback)
        {
            ClienteCashback = clienteCashback;
        }

        public decimal CalcularDesconto(decimal subtotal)
        {
            bool valRestantePositivo = false;
            decimal descontoCashback = 0m;

            // Adiciona verificação de nulo para ClienteCashback
            if (ClienteCashback == null)
            {
                Console.WriteLine("Cliente não informado para cashback.");
                return 0m;
            }

            while (valRestantePositivo == false)
            {
                Console.WriteLine($"Valor a ser pago: R$ {subtotal:F2}");
                decimal valorCashbackUsado = Utils.EvitaQuebraCodDecimal("Valor a ser utilizado: R$ ");

                if (valorCashbackUsado > 0 && valorCashbackUsado <= subtotal && valorCashbackUsado <= ClienteCashback.DescProximaCompra)
                {
                    ClienteCashback.DebitaDescontoProximaCompra(valorCashbackUsado);

                    Console.WriteLine(ObterMensagem(valorCashbackUsado));
                    Console.WriteLine($"Saldo restante de cashback: R$ {ClienteCashback.DescProximaCompra:F2}");

                    descontoCashback = valorCashbackUsado;
                    valRestantePositivo = true;
                }
                else
                {
                    Console.WriteLine("\nValor inválido! Verifique se o valor é positivo, se não excede o total da compra ou seu saldo de cashback.");
                    Console.WriteLine($"Saldo Cashback: {ClienteCashback.DescProximaCompra:F2} | Valor Compra: {subtotal:F2}");
                }
            }

            return descontoCashback;
        }

        public string ObterMensagem(decimal valorDesconto)
        {
            return $"\nDesconto de R$ {valorDesconto:F2} aplicado com sucesso!";
        }

    }
}
