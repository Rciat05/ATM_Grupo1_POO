using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_ATM_Grupo1_POO.BusinessLayer;
using Tarea_ATM_Grupo1_POO.Enums;

namespace Tarea_ATM_Grupo1_POO.UI
{
    public class Menu
    {
        public void LoadMenu()
        {
            char continuar;
            do
            {
                var option = AnsiConsole.Prompt(
               new SelectionPrompt<AccountOptions>()
               .Title("[blue]Seleccione una ´de las opciones: [/]")
               .PageSize(10)
               .AddChoices(new[]
               {
                    AccountOptions.VerSaldo,
                    AccountOptions.Retirar,
                    AccountOptions.Depositar
               })
           );

                switch (option)
                {
                    case AccountOptions.VerSaldo:
                        Account.ViewBalance();
                        break;
                    case AccountOptions.Retirar:
                        Account.Withdraw();
                        break;
                    case AccountOptions.Depositar:
                        Account.Deposit();
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
