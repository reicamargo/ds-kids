using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace DS.Kids.API.Emails
{
    public abstract class EmailBase<T> : IDisposable
    {
        private SmtpClient _smtpClient  = new SmtpClient("smtp.critsend.com", 587);
        public EmailBase()
        { 
            this._smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            this._smtpClient.Credentials = new NetworkCredential("taurafigueiredo@minhavida.com.br", "iFlkvoGqp1uYRLAyK");
            this._smtpClient.EnableSsl = true;
        }

        protected abstract MailMessage ObterConteudoEmail(T referencia);

        public async Task EnviarAsync(T referencia)
        {
            var mail = this.ObterConteudoEmail(referencia);
            await this._smtpClient.SendMailAsync(mail);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            this._smtpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}