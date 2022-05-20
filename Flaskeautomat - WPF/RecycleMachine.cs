using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Flaskeautomat___WPF
{
    public class RecycleMachine
    {
        Queue<Bottle> producedBottles;

        public RecycleMachine(Queue<Bottle> produced)
        {
            producedBottles = produced;
        }

        public void ProduceBottle(string actualName)
        {           
            Monitor.Enter(producedBottles);
            Bottle bottle = new Bottle(actualName);
            producedBottles.Enqueue(bottle);
            Thread.Sleep(1000);
            Monitor.PulseAll(producedBottles);
            Monitor.Exit(producedBottles);
        }        
    }
}
