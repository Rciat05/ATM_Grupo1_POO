using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_ATM_Grupo1_POO.BusinessLayar;
using Tarea_ATM_Grupo1_POO.Enums;

namespace Tarea_ATM_Grupo1_POO.UI
{
    public class Menu
    {
        public void LoadMenu()
        {

            bool authenticated = AuthenticateUser();
            if (!authenticated)
            {
                AnsiConsole.Markup("[red]Autenticación fallida. Saliendo del sistema.[/]");
                return;
            }

            var typeAccount = AnsiConsole.Prompt(
               new SelectionPrompt<TypeAccount>()
               .Title("\n[aqua]Seleccione su tipo de cuenta:[/]")
               .PageSize(10)
               .AddChoices(new[]
               {
                    TypeAccount.CuentaCorriente,
                    TypeAccount.CuentaAhorro
               }));
            switch (typeAccount)
            {
                case TypeAccount.CuentaCorriente:
                    Account.ProcessCurrentAccount();
                    break;
                case TypeAccount.CuentaAhorro:
                    Account.ProcessSavingsAccount();
                    break;
            }
            char continuar;
            do
            {
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<AccountOptions>()
                    .Title("\n[blue]Seleccione una de las opciones: [/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        AccountOptions.VerSaldo,
                        AccountOptions.Retirar,
                        AccountOptions.Depositar,
                        AccountOptions.PagarFactura,
                        AccountOptions.CambiarPin
                    }));

                switch (option)
                {
                    case AccountOptions.VerSaldo:
                        Account.ViewBalanceCorriente();
                        break;
                    case AccountOptions.Retirar:
                        Account.WithdrawCorriente();
                        break;
                    case AccountOptions.Depositar:
                        Account.DepositCorriente();
                        break;
                    case AccountOptions.PagarFactura:
                        Account.PayBills();
                        break;
                    case AccountOptions.CambiarPin:
                        Account.ChangePin();
                        break;
                    default:
                        break;
                }

                AnsiConsole.WriteLine();
                continuar = AnsiConsole.Ask<char>("Desea continuar S(si), N(no)").ToString().ToLower()[0];
                if (continuar != 's' && continuar != 'n')
                {
                    AnsiConsole.Markup("\n[red]Por favor, ingrese 'S' para sí o 'N' para no.[/]");
                }
            } while (continuar == 's');
        }

        private bool AuthenticateUser()
        {
            try
            {
                var usuariosPIN = new Dictionary<string, string>
                {
                    { "123456789", "1010" },
                    { "987654321", "1020" },
                    { "112233445", "1030" },
                    { "566778899", "1040" },
                    { "021212828", "3060" }
                };

                var numeroCuenta = AnsiConsole.Ask<string>("\n[deeppink2]Ingrese su número de cuenta:[/]");
                var pin = AnsiConsole.Ask<string>("\n[deeppink2]Ingrese su PIN:[/]");

                if (usuariosPIN.ContainsKey(numeroCuenta) && usuariosPIN[numeroCuenta] == pin)
                {
                    return true;
                }
                else
                {
                    AnsiConsole.Markup("[red]Número de cuenta o PIN incorrecto. Por favor, inténtelo de nuevo.[/]");
                    return false;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"[red]Error al autenticar al usuario: {ex.Message}[/]");
                return false;
            }
        }
    }
}