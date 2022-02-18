using System;
using stock_quote_alert;

public class Alerter {

    public static async Task<int> Main(string[] args) {
        if (verifyArgs(args.Length) < 1) {
            return -args.Length;
        }
        Consultant consultant = new Consultant(args[0], Convert.ToDouble(args[1]), Convert.ToDouble(args[2]));
        AssetOperation operation = await consultant.Consult();
        guideTrader(operation);
        return 1;
    }

    private static int verifyArgs(int size){
        if (size > 2) {
            return size;
        } 
        if (size < 2) {
            Console.Error.WriteLine("Parâmetro obrigatório faltando: limite inferior de preço");
        }
        if (size < 1) {
            Console.Error.WriteLine("Parâmetro obrigatório faltando: limite superior de preço");
        } 
        if (size == 0) {
            Console.Error.WriteLine("Parâmetro obrigatório faltando: símbolo do ativo");
        }
        return -size;
    }

    private static void guideTrader(AssetOperation operation) {
        switch (operation) {
            case AssetOperation.Buy:
                Console.Out.WriteLine("COMPRA COMPRA COMPRA");
                break;
            case AssetOperation.Sell:
                Console.Out.WriteLine("VENDE VENDE VENDE");
                break;
            case AssetOperation.InvalidAsset:
                Console.Error.WriteLine("Ativo inválido");
                break;
            case AssetOperation.Error:
                Console.Error.WriteLine("Erro");
                break;
        }
    }
}