using System;
using System.Collections.Generic;

using DS.Kids.Model.Validations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Kids.Testes.Model
{
    [TestClass]
    public class MessagesTest
    {

        private Dictionary<ResultCodes, string> _dict;

        [TestInitialize]
        public void Initialize()
        {
            IMessageResource resource = Messages.GetResources();
            _dict = resource.Get();
        }

        [TestMethod]
        public void Todas_As_Mensagens_Existem_Para_Todos_Os_Valor_Do_Enum()
        {
            foreach (ResultCodes resultCode in Enum.GetValues(typeof(ResultCodes)))
            {
                Assert.IsTrue(_dict.ContainsKey(resultCode), string.Format("Você não implementou a mensagem de erro para o enum {0}.", resultCode));
            }

            Assert.AreEqual(Enum.GetValues(typeof(ResultCodes)).Length, _dict.Count, "Você implementou alguma mensagem mais de uma vez.");
        }
    }
}
