using System;

using DS.Kids.Model;
using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class EsqueciMinhaSenhaTest
    {
        [TestMethod]
        public void Esqueci_Senha_Com_Token_Recuperacao_Senha_Invalido()
        {
            var esqueciMinhaSenha = new  EsqueciMinhaSenha
            {
                TokenRecuperacaoSenha = string.Empty,
                NovaSenha = "123456",
                ConfirmacaoNovaSenha = "123456"
            };

            var expected = ResultCodes.TokenRecuperacaoSenhaInvalido;
            var result = esqueciMinhaSenha.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Esqueci_Senha_Com_Nova_Senha_Invalida()
        {
            var esqueciMinhaSenha = new EsqueciMinhaSenha
            {
                TokenRecuperacaoSenha = Guid.NewGuid().ToString(),
                NovaSenha = string.Empty,
                ConfirmacaoNovaSenha = "123456"
            };

            var expected = ResultCodes.SenhaAtualInvalida;
            var result = esqueciMinhaSenha.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Esqueci_Senha_Com_Confirmacao_Nova_Senha_Invalida()
        {
            var esqueciMinhaSenha = new EsqueciMinhaSenha
            {
                TokenRecuperacaoSenha = Guid.NewGuid().ToString(),
                NovaSenha = "123456",
                ConfirmacaoNovaSenha = string.Empty
            };

            var expected = ResultCodes.NovaSenhaInvalida;
            var result = esqueciMinhaSenha.Validate();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Esqueci_Senha_Com_Informacoes_Validas()
        {
            var esqueciMinhaSenha = new EsqueciMinhaSenha
            {
                TokenRecuperacaoSenha = Guid.NewGuid().ToString(),
                NovaSenha = "123456",
                ConfirmacaoNovaSenha = "123456"
            };

            var expected = ResultCodes.Success;
            var result = esqueciMinhaSenha.Validate();
            Assert.AreEqual(expected, result);
        }
    }
}
