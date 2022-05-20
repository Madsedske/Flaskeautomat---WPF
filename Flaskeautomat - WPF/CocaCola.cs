using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flaskeautomat___WPF
{
    public class CocaCola : Bottle
    {
        private double bottlePrice;

        public double BottlePrice
        {
            get { return bottlePrice; }
            set { bottlePrice = value; }
        }

        public CocaCola(string bottleName) : base(bottleName)
        {
            BottlePrice = 3;
        }

        public override string ToString()
        {
            return BottleName + "\npant tilbage " + BottlePrice + " Kr";
        }
    }
}
