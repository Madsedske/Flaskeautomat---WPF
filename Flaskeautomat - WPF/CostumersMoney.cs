using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flaskeautomat___WPF
{
    public class CostumersMoney : EventArgs
    {
        public double Money { get; private set; }

        public CostumersMoney(double money)
        {
            Money = money;
        }
    }
}
