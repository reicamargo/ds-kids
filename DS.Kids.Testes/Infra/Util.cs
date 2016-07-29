using System;
using System.Text;
using System.Threading.Tasks;

using DS.Kids.Model.Validations;

namespace DS.Kids.Testes.Infra
{
    public static class Util
    {
        public static string CreateEmail()
        {
            var x = Task.Delay(5);
            Task.WaitAll(x);

            return string.Format("a{0}@{1}.com.br", DateTime.Now.ToString("ddMMyyyyhhmmssffffff"), CreateString(5));
        }

        public static string CreateEmail(string seed)
        {
            var x = Task.Delay(5);
            Task.WaitAll(x);

            return string.Format("{0}{1}@{2}.com.br", seed, DateTime.Now.ToString("ddMMyyyyhhmmssffffff"), CreateString(5));
        }

        public static string CreateString(int length)
        {
            var sb = new StringBuilder();

            var caracteres = @"qwertyuiopasdfghjklzxcvbnm1234567890";
            for (int i = 0; i < length; i++)
            {
                var indice = ExtentionMethods.Random.Next(0, caracteres.Length);
                sb.Append(caracteres[indice]);
                caracteres = caracteres.Remove(indice, 1);
            }

            return sb.ToString();
        }

        public static void PrintResult(ResultCodes errorCode)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (errorCode == ResultCodes.Success)
                Console.BackgroundColor = ConsoleColor.Green;
            else
                Console.BackgroundColor = ConsoleColor.Red;

            Console.WriteLine(errorCode);
            Console.ResetColor();
        }
    }
}
