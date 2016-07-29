using System.Net.Mail;

namespace DS.Kids.API.Emails
{
    public class TrocaSenha : EmailBase<Model.Responsavel>
    {
        protected override MailMessage ObterConteudoEmail(Model.Responsavel referencia)
        {
            MailMessage mail = new MailMessage("taurafigueiredo@minhavida.com.br", referencia.Email);
            mail.Subject = "DSKids - Esqueci Minha Senha";
            mail.IsBodyHtml = true;
            mail.Body = this.ObterHtml(referencia.TokenRecuperacaoSenha);
            return mail;
        }

        private string ObterHtml(string tokenRecuperacaoSenha)
        {
            var html = @"
                <html>
                    <body>
                        <a href='http://www.dskids.com.br/Suporte/TrocaDeSenha/{0}'>Clicque aqui para trocar sua senha</a> 
                    </body>
                </html>
            ";

            return string.Format(html, tokenRecuperacaoSenha);
        }
    }
}