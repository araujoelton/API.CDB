using System;

namespace CDB.Api
{
    internal static class Program
    {
        private static void Main()
        {
            const string url = "http://localhost:5054";
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"API rodando em {url}");
                Console.WriteLine("Pressione ENTER para encerrar.");
                Console.ReadLine();
            }
        }
    }
}
