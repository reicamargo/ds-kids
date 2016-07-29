// ReSharper disable once CheckNamespace
namespace System
{
    public static class ExtentionMethods
    {
        public static readonly Random Random = new Random();

        public static int Age(this DateTime date)
        {
            if (DateTime.Today.Month < date.Month || DateTime.Today.Month == date.Month && DateTime.Today.Day < date.Day)
                return DateTime.Today.Year - date.Year - 1;
            return DateTime.Today.Year - date.Year;
        }

        public static int GetTotalMonths(this DateTime date)
        {
            var age = DateTime.Now.Subtract(date);
            var month = (int)(age.TotalDays / 30);
            return month;
        }

        public static int GetIdade(this DateTime dataNascimentoCrianca)
        {
            var age = DateTime.UtcNow.Subtract(dataNascimentoCrianca);
            var years = (decimal)age.TotalDays / 365.242M;
            var idade = (int)Math.Truncate(years);
            return idade;
        }
    }
}
