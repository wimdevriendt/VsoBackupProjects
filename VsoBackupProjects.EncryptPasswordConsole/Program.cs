using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsoBackupProjects.Cryptography;

namespace VsoBackupProjects.EncryptPasswordConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter password : ");
            string password = Console.ReadLine();
            Console.WriteLine("============================================================");
            Console.WriteLine(password.Encrypt());
            Console.WriteLine("============================================================");

            Console.WriteLine("Copy this encrypted string in your application configuration file.");
            Console.WriteLine("Press a enter to quit...");
            Console.ReadLine();

        }
    }
}
