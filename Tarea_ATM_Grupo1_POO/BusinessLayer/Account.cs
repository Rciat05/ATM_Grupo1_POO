using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_ATM_Grupo1_POO.BusinessLayer
{
    public static class Account
    {
        public static string Name { get; set; }

        public static decimal Balance { get; set; } = 0;

        public static void Deposit()
        {
            decimal amount = AnsiConsole.Ask<decimal>("[green]Digite la cantidad que se quiere depositar: [/]");
            Balance += amount;
            AnsiConsole.WriteLine($"Prceso realizado correctamente, su nuevo balance es {Balance}: ");
        }

        public static void Withdraw()
        {
            decimal amount = AnsiConsole.Ask<decimal>("[blue]Digite la cantidad que se quiere depositar: [/]");
            if (amount > Balance) 
            {
                AnsiConsole.WriteLine("Lo siento, no cuenta con fondos suficientes...");
            }
            else
            {
                Balance -= amount;
                AnsiConsole.WriteLine($"Retiro realizado correctamente, su nuevo balance es {Balance}:");
            }
            
        }

        public static void ViewBalance()
        {
            AnsiConsole.WriteLine($"El saldo de su cuenta es {Balance}");
        }
    }


}
