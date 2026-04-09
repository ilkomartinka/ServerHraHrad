using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class GiveCommand:ICommand
    {
        public void execute()
        {
            Console.WriteLine("GiveCommand");
        }
    }
}
