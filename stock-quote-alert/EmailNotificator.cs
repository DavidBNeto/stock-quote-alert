using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace stock_quote_alert; 

public class EmailNotificator {

    private EmailConfig emailConfig;
    private SmtpClient client;

    public EmailNotificator() {
        emailConfig = new EmailConfig();
        client = new SmtpClient();
        Connect();
    }

    private void Connect() {
        client.Connect(
            emailConfig.Smtp,
            emailConfig.Port,
            false
        );
        client.Authenticate(
            emailConfig.SenderEmail,
            emailConfig.Password
        );
    }

    public void SendSellOrder(string asset, string price) {
        MimeMessage message = BuildInitialMessage();
        message.Subject = new StringBuilder().Append("Hora de Vender ").Append(asset).Append("!!!").ToString();
        message.Body = new TextPart("plain") {
            Text = @BuildEmailMessage(asset, price, SellMessage())
        };
        if (client.Send(message).Contains(" OK ")) {
            Console.Out.Write("Notificação enviada com sucesso por email");
        }
    }

    public void SendBuyOrder(string asset, string price) {
        MimeMessage message = BuildInitialMessage();
        message.Subject = new StringBuilder().Append("Hora de Comprar ").Append(asset).Append("!!!").ToString();
        message.Body = new TextPart("plain") {
            Text = @BuildEmailMessage(asset, price, BuyMessage())
        };
        if (client.Send(message).Contains(" OK ")) {
            Console.Out.Write("Notificação enviada com sucesso por email");
        }
    }

    private MimeMessage BuildInitialMessage() {
        MimeMessage message = new MimeMessage();
        message.Importance = MessageImportance.High;
        message.Priority = MessagePriority.Urgent;
        message.From.Add(new MailboxAddress(
            emailConfig.SenderName,
            emailConfig.SenderEmail
        ));
        message.To.Add(new MailboxAddress(
            emailConfig.RecipientName,
            emailConfig.RecipientEmail    
        ));
        return message;
        
    }

    private string BuildEmailMessage(string asset, string price, string action) {
        return new StringBuilder().Append("Olá ")
            .Append(emailConfig.RecipientName)
            .Append(", \n \n")
            .Append("Considerando que o ativo ")
            .Append(asset)
            .Append(" atingiu o preço de R$")
            .Append(price)
            .Append(" na B3, é fortemente recomendado que você ")
            .Append(action)
            .Append("\n \nAtt,\nSeu Alerta de Preço de Ativos.")
            .ToString();
    }

    private string SellMessage() {
        return "venda as unidades que você possui desse ativo.";
    }

    private string BuyMessage() {
        return "compre mais unidades desse ativo.";
    }
}