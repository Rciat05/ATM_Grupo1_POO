using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Tarea_ATM_Grupo1_POO.Enums;

namespace Tarea_ATM_Grupo1_POO.BusinessLayar
{
    public static class Account
    {
        public static string? Name { get; set; }
        public static decimal CorrienteBalance { get; set; } = 1500;
        public static decimal AhorroBalance{ get; set; } = 15000;

        public static Dictionary<string, string> UsersPIN { get; } = new Dictionary<string, string>
        {
            { "123456789", "1010" },
            { "987654321", "1020" },
            { "112233445", "1030" },
            { "566778899", "1040" },
            { "021212828", "3060" }
        };
        public static void DepositCorriente()
        {
            decimal amount = AnsiConsole.Ask<decimal>("[green]Digite la cantidad que se quiere depositar: $[/]");
            CorrienteBalance  += amount;
            AnsiConsole.WriteLine($"Prceso realizado correctamente, su nuevo balance es {CorrienteBalance }: ");
        }
        public static void DepositAhorro()
        {
            decimal amount = AnsiConsole.Ask<decimal>("[green]Digite la cantidad que se quiere depositar: $[/]");
            AhorroBalance  += amount;
            AnsiConsole.WriteLine($"Prceso realizado correctamente, su nuevo balance es {AhorroBalance }: ");
        }
        public static void WithdrawCorriente()
        {
            decimal amount = AnsiConsole.Ask<decimal>("[blue]Digite la cantidad que se quiere retirar: $[/]");
            if (amount > CorrienteBalance ) 
            {
                AnsiConsole.WriteLine("Lo siento, no cuenta con fondos suficientes...");
            }
            else
            {
                CorrienteBalance  -= amount;
                AnsiConsole.WriteLine($"Retiro realizado correctamente, su nuevo balance es ${CorrienteBalance}:");
            }  
        }
        public static void WithdrawAhorro()
        {
            decimal amount = AnsiConsole.Ask<decimal>("[blue]Digite la cantidad que se quiere retirar: $[/]");
            if (amount > AhorroBalance) 
            {
                AnsiConsole.WriteLine("Lo siento, no cuenta con fondos suficientes...");
            }
            else
            {
                AhorroBalance  -= amount;
                AnsiConsole.WriteLine($"Retiro realizado correctamente, su nuevo balance es ${AhorroBalance}:");
            }  
        }
        public static void ViewBalanceCorriente()
        {
            AnsiConsole.WriteLine($"El saldo de su cuenta corriente es: ${CorrienteBalance }");
        }
        public static void ViewBalanceAhorro()
        {
            AnsiConsole.WriteLine($"El saldo de su cuenta de ahorro es: ${AhorroBalance}");
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
                var typeBill = AnsiConsole.Prompt(
                    new SelectionPrompt<Bills>()
                    .Title("\n[aqua]Seleccione el tipo de factura:[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        Bills.Energia,
                        Bills.Agua,
                        Bills.Banco,
                        Bills.Telefono,
                        Bills.Internet
                    }));

                decimal amount = AnsiConsole.Ask<decimal>("[blue]Digite la cantidad que desea pagar: $[/]");

                switch (typeBill)
                {
                    case Bills.Energia:
                        ProcessPayment(Bills.Energia, amount);
                        break;
                    case Bills.Agua:
                        ProcessPayment(Bills.Agua, amount);
                        break;
                    case Bills.Banco:
                        ProcessPayment(Bills.Banco, amount);
                        break;
                    case Bills.Telefono:
                        ProcessPayment(Bills.Telefono, amount);
                        break;
                    case Bills.Internet:
                        ProcessPayment(Bills.Internet, amount);
                        break;
                    default:
                        AnsiConsole.WriteLine("Factura no reconocida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"[red]Error al pagar la factura: {ex.Message}[/]");
            }
        }
        private static void ProcessPayment(Bills bill, decimal amount)
        {
            if (amount > CorrienteBalance)
            {
                AnsiConsole.WriteLine("Lo siento, no cuenta con fondos suficientes para pagar esta factura.");
            }
            else
            {
                CorrienteBalance -= amount;
                AnsiConsole.WriteLine($"Factura {bill} pagada correctamente, su nuevo balance es ${CorrienteBalance}:");
            }
        }
        public static void ProcessCurrentAccount()
        {
            char continuar;
            do
            {
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<AccountOptions>()
                .Title("[blue]Seleccione una de las opciones:[/]")
                .PageSize(10)
                .AddChoices(new[]
                    {
                        AccountOptions.VerSaldo,
                        AccountOptions.Retirar,
                        AccountOptions.Depositar,
                        AccountOptions.PagarFactura,
                        AccountOptions.CambiarPin,
                        AccountOptions.Salir
                    }));

                switch (option)
                {
                    case AccountOptions.VerSaldo:
                        ViewBalanceCorriente();
                        break;
                    case AccountOptions.Retirar:
                        WithdrawCorriente();
                        break;
                    case AccountOptions.Depositar:
                        DepositCorriente();
                        break;
                    case AccountOptions.PagarFactura:
                        PayBills();
                        break;
                    case AccountOptions.CambiarPin:
                        ChangePin();
                        break;
                    case AccountOptions.Salir:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

                AnsiConsole.WriteLine();
                continuar = AnsiConsole.Ask<char>("Desea continuar S(si), N(no)");
            } while (continuar == 's');
        }
        public static void ProcessSavingsAccount()
        {
            char continuar;
            do
            {
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<AccountOptions>()
                .Title("[blue]Seleccione una de las opciones:[/]")
                .PageSize(10)
                .AddChoices(new[]
                    {
                        AccountOptions.VerSaldo,
                        AccountOptions.Retirar,
                        AccountOptions.Depositar,
                        AccountOptions.Salir
                    }));

                switch (option)
                {
                    case AccountOptions.VerSaldo:
                        ViewBalanceAhorro();
                        break;
                    case AccountOptions.Retirar:
                        WithdrawAhorro();
                        break;
                    case AccountOptions.Depositar:
                        DepositAhorro();
                        break;
                    case AccountOptions.Salir:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

                AnsiConsole.WriteLine();
                continuar = AnsiConsole.Ask<char>("Desea continuar S(si), N(no)");
            } while (continuar == 's');
        }
    }
}
