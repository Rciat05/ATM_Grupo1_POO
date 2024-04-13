using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_ATM_Grupo1_POO.BusinessLayar
{
    public static class Account
    {
        public static string Name { get; set; }

        public static decimal Balance { get; set; } = 0;

        public static Dictionary<string, string> UsersPIN { get; } = new Dictionary<string, string>
        {
            { "123456789", "1010" },
            { "987654321", "1020" },
            { "112233445", "1030" },
            { "566778899", "1040" },
            { "021212828", "3060" }
        };

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

        public static void ChangePin() 
        {
            try
            {
                var numeroCuenta = AnsiConsole.Ask<string>("Ingrese su número de cuenta:");
                if (!UsersPIN.ContainsKey(numeroCuenta))
                {
                    AnsiConsole.Markup("[red]Número de cuenta no encontrada.[/]");
                    return;
                }

                var nuevoPin = AnsiConsole.Ask<string>("Ingrese su nuevo PIN:");
                UsersPIN[numeroCuenta] = nuevoPin;
                AnsiConsole.WriteLine("PIN cambiado correctamente.");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"[red]Error al cambiar el PIN: {ex.Message}[/]");
            }
        }

        public static void PayBills() 
        {
            try
            {
                decimal amount = AnsiConsole.Ask<decimal>("[blue]Digite la cantidad que desea pagar: [/]");
                if (amount > Balance)
                {
                    AnsiConsole.WriteLine("Lo siento, no cuenta con fondos suficientes para pagar esta factura.");
                }
                else
                {
                    Balance -= amount;
                    AnsiConsole.WriteLine($"Factura pagada correctamente, su nuevo balance es {Balance}:");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"[red]Error al pagar la factura: {ex.Message}[/]");
            }
        }
    }


}
