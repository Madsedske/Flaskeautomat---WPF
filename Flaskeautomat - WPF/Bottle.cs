using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flaskeautomat___WPF
{
    public class Bottle : EventArgs
    {       
        public Bottle(string bottleName)
        {           
            BottleName = bottleName;
        }

        public string BottleName { get; private set; }

        public override string ToString()
        {
            return BottleName;
        }
    }
}
