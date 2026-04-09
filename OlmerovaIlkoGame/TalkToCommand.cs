using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class TalkToCommand :ICommand
    {
        public void execute()
        {
            Console.WriteLine("TalkToCommand");
        }
    }
}
