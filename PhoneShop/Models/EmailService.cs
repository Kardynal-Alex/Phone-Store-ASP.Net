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
            mm.Body = await Task.Run(() => BodyHtmlText(cart));
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("alexandrkardinal@gmail.com", "alex60327");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
        }
        public string BodyHtmlText(Cart cart)
        {
            StringBuilder text = new StringBuilder();
            text.Append("<html>")
                .Append("<head>")
                .Append("<h1>Dear customer !</h1>")
                .Append("</head>")
                .Append("<body>")
                .Append("<h3>Your Order<h3>");

            text.Append("<table class=\"table\">")
                .Append("<tbody>");
            foreach (var item in cart.Lines)
            {
                text.Append("<tr>");
                text.Append($"<td class=\"text - center\">{item.Quantity}</td>")
                    .Append($"<td class=\"text - left\">{item.Product.Name}</td>")
                    .Append($"<td class=\"text - right\">{(item.Quantity * item.Product.Price).ToString("f2")} $</td>");
                text.Append("</tr>");
            }
            text.Append("</tbody>");
            text.Append("<tfoot>")
                .Append("<tr>");

            text.Append("<td colspan=\"3\" class=\"text - right\">Total Price : </td>")
                .Append("<td>")
                .Append($"{cart.ComputeTotalValue().ToString("f2")} $")
                .Append("</td>");
            text.Append("</tr>");
            text.Append("<tr>");
            if (cart.TotalPriceWithDiscount != 0)
            {
                text.Append("<td colspan=\"3\" class=\"text - right\">Total Price with discount : </td>")
                    .Append("<td>")
                    .Append($"{cart.TotalPriceWithDiscount.ToString("f2")} $")
                    .Append("</td>");
            }

            text.Append("</tr>")
                .Append("</tfoot>")
                .Append("</table>");

            text.Append("<h3>Best Wishes, Phone Store</h3>");
            text.Append("</body>")
                .Append("</html>");

            return text.ToString();
        }
    }
}
