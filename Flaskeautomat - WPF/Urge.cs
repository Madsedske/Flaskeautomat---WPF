using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flaskeautomat___WPF
{
    public class Urge : Bottle
    {
        private double bottlePrice;

        public double BottlePrice
        {
            get { return bottlePrice; }
            set { bottlePrice = value; }
        }

        public Urge(string bottleName) : base(bottleName)
        {
            BottlePrice = 1;
        }

        public override string ToString()
        {
            return BottleName + "\npant tilbage " + BottlePrice + " Kr";
        }
    }
}
