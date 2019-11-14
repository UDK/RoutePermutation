using System;
using Crypto3.Core;

namespace Crypto3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите шифруемое слово");
            CoreRoutePermutation routePermutation = new CoreRoutePermutation();
            var encryptStr = routePermutation.Encrypt(Console.ReadLine());
            Console.ReadLine();
        }
       
    }
}
