using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Text;
using PhoneShop.Models.DataModel;
namespace PhoneShop.Models
{
    public class EmailService
    {
        public async Task SendEmailAsync(ShippingDetails shippingDetails, Cart cart)
        {
            MailMessage mm = new MailMessage("alexandrkardinal@gmail.com", shippingDetails.Email);
            mm.Subject = "Phone Store";
            mm.Body = await Task.Run(() => BodyText(cart));
            mm.IsBodyHtml = false;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("alexandrkardinal@gmail.com", "alex60327");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
        }
        public string BodyText(Cart cart)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Dear customer !\n\n");
            text.Append("Your Order\n");
            foreach (var item in cart.Lines)
            {
                text.Append(item.Quantity+"\t")
                    .Append(item.Product.Name + "\t")
                    .Append("  " + (item.Quantity*item.Product.Price).ToString("f2") + " $\n");
            }
            text.Append("\nTotal Price : " + cart.ComputeTotalValue() + " $");
            text.Append("\n\nBest Wishes, Phone Store");
            return text.ToString();
        }
    }
}
