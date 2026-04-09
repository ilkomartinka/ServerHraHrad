using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class TakeCommand : ICommand
    {
        public void execute()
        {
            Console.WriteLine("TakeCommand");
        }
    }
}
