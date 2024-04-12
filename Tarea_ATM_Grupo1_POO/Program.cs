using Spectre.Console;
using Tarea_ATM_Grupo1_POO.UI;

namespace Tarea_ATM_Grupo1_POO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.LoadMenu();
        }
    }
}
