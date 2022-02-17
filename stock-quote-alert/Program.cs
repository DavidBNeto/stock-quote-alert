using System;

public class Alerter {

    public static int Main(String[] args) {
        if (args.Length == 0) {
           Console.Error.WriteLine("Parâmetro obrigatório faltando: símbolo do ativo")
           return 0;
        } 
        if (args.Length == 1) {
           Console.Error.WriteLine("Parâmetro obrigatório faltando: limite superior de preço");
           return 0;
        } 
        if (args.Length == 2) {
           Console.Error.WriteLine("Parâmetro obrigatório faltando: limite inferior de preço");
           return 0;
        }
    }
}

//windows service
//use bat archive for executing services
//outras línguas
//outras moedas
//se evento não for possível, tentar de tantos em tantos segundos consultar o preço
//prazo de inicio, de fim
//opção relatório
//banco de dados: email, teto, piso, ativo
//validar nomes
//8W4T3X62ADBWQHG9