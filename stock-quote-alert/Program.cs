using Microsoft.Extensions.Configuration;

namespace stock_quote_alert; 

public static class Program {

    public static async Task<int> Main(string[] args) {
        if (!ContainsAllNeededArgs(args.Length)) {
            return -1; // there are missing arguments 
        }
        if (!LimitsAreDoubles(args[1], args[2])) {
            return -2; // the limits are not in proper double format or an overflow error occurred
        }
        Consultant consultant = new Consultant(args[0], args[1], args[2], new SystemClock());
        AssetOperation operation = await consultant.Consult();
        return GuideTrader(
            operation, 
            args[0], 
            operation == AssetOperation.Buy ? args[2] : args[1]
        );
    }

    private static bool ContainsAllNeededArgs(int size){
        if (size > 2) {
            return true;
        } 
        if (size <= 2) {
            Console.Error.WriteLine("Parâmetro obrigatório faltando: limite inferior de preço");
        }
        if (size <= 1) {
            Console.Error.WriteLine("Parâmetro obrigatório faltando: limite superior de preço");
        } 
        if (size == 0) {
            Console.Error.WriteLine("Parâmetro obrigatório faltando: símbolo do ativo");
        }
        return false;
    }

    private static bool LimitsAreDoubles(string ceil, string floor) {
        try {
            Convert.ToDouble(ceil.Replace('.', ','));
            Convert.ToDouble(floor.Replace('.', ','));
            return true;
        }
        catch (FormatException exception) {
            return false;
        }
        catch (OverflowException exception) {
            return false;
        }
    }

    private static int GuideTrader(AssetOperation operation, string asset, string price) {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
         switch (operation) {
            case AssetOperation.Buy:
                new EmailNotificator().SendBuyOrder(asset, price);
                return 1;
            case AssetOperation.Sell:
                new EmailNotificator().SendSellOrder(asset, price);
                return 1;
            case AssetOperation.InvalidAsset:
                Console.Error.WriteLine("Ativo inválido");
                return -3;
            case AssetOperation.Error:
                Console.Error.WriteLine("Erro");
                return -4;
            default:
                return -4;
         }
    }
}