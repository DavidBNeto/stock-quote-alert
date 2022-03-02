# stock-quote-alert

O stock-quote-alert, como o nome já diz, é um alerta que notifica o usuário, via email, caso uma certa ação ultrapasse um limite de preço definido pelo mesmo.
Ao ser executado, devem ser passados três parametros:
- O ativo a ser monitorado
- O preço de referência para venda
- O preço de referência para compra

Segue um exemplo:

```bash
> stock-quote-alert.exe PETR4 22.67 22.59
```

## Arquitetura

Usando a api [fcsapi](https://fcsapi.com/), o stock-quote-alert consulta o preço de determinados ativos através do método de [polling](https://en.wikipedia.org/wiki/Polling_(computer_science)), fazendo requests entre períodos pré determinados.