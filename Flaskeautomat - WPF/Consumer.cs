using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flaskeautomat___WPF
{
    public class Consumer
    {
        public double AllMoney { get; private set; }

        public event EventHandler<Bottle> bottleHandlerEvent;
        public event EventHandler<CostumersMoney> costumerHandlerEvent;

        CostumersMoney costumersMoney;
        Queue<CocaCola> cocaColaBottles;
        Queue<Heineken> heinekenBottles;
        Queue<Carlsberg> carlsbergbottles;
        Queue<Urge> urgebottles;
        Queue<FaxeKondi> faxeKondiBottles;

        public Consumer(Queue<CocaCola> producedCocaCola, Queue<Heineken> producedHeineken, Queue<Carlsberg> producedCarlsberg, Queue<Urge> producedUrge, Queue<FaxeKondi> producedFaxeKondi)
        {
            cocaColaBottles = producedCocaCola;
            heinekenBottles = producedHeineken;
            carlsbergbottles = producedCarlsberg;
            urgebottles = producedUrge;
            faxeKondiBottles = producedFaxeKondi;
        }

        public void WithdrawMoney()
        {
            AllMoney = 0;
        }

        public void DisplayMoney(CostumersMoney money)
        {
            costumerHandlerEvent?.Invoke(this, money);
        }

        public void AllCoins(double removedBottle)
        {
            AllMoney = AllMoney + removedBottle;
            DisplayMoney(new CostumersMoney(AllMoney));           
        }

        public void DisplayBottle(Bottle bottleName)
        {
            bottleHandlerEvent?.Invoke(this, bottleName);
        }

        public void ColaConsumer()
        {
            while (true)
            {
                // Lock for the colaBottles
                lock (cocaColaBottles)
                {
                    // A while loop to check if colaBottles contains an object.
                    while (cocaColaBottles.Count == 0)
                    {
                        // As long the colaBottles is empty, the thread will wait.
                        Monitor.Wait(cocaColaBottles);
                    }
                    // Removing the latest cola from colaBottles.
                    CocaCola removedBottle = cocaColaBottles.Dequeue();
                    DisplayBottle(new Bottle(removedBottle.ToString()));
                    AllCoins(removedBottle.BottlePrice);
                    Thread.Sleep(2000);
                }
            }
        }

        public void HeinekenConsumer()
        {
            while (true)
            {
                // Lock for the heinekenBottles.
                lock (heinekenBottles)
                {
                    // A while loop to check if heinekenBottles contains a object.
                    while (heinekenBottles.Count == 0)
                    {
                        // As long the beerBottles is empty, the thread will wait.
                        Monitor.Wait(heinekenBottles);
                    }
                    // Removing the latest heineken from heinekenBottles.
                    Heineken removedBottle = heinekenBottles.Dequeue();
                    DisplayBottle(new Bottle(removedBottle.ToString()));
                    AllCoins(removedBottle.BottlePrice);
                    Thread.Sleep(2000);
                }
            }
        }

        public void FaxeKondiConsumer()
        {
            while (true)
            {
                // Lock for the faxeBottles.
                lock (faxeKondiBottles)
                {
                    // A while loop to check if faxeBottles contains a object.
                    while (faxeKondiBottles.Count == 0)
                    {
                        // As long the faxeBottles is empty, the thread will wait.
                        Monitor.Wait(faxeKondiBottles);
                    }
                    // Removing the latest faxe from faxeBottles.
                    FaxeKondi removedBottle = faxeKondiBottles.Dequeue();
                    DisplayBottle(new Bottle(removedBottle.ToString()));
                    AllCoins(removedBottle.BottlePrice);
                    Thread.Sleep(2000);
                }
            }
        }

        public void UrgeConsumer()
        {
            while (true)
            {
                // Lock for the urgeBottles.
                lock (urgebottles)
                {
                    // A while loop to check if urgeBottles contains a object.
                    while (urgebottles.Count == 0)
                    {
                        // As long the urgeBottles is empty, the thread will wait.
                        Monitor.Wait(urgebottles);
                    }
                    // Removing the latest urge from urgeBottles.
                    Urge removedBottle = urgebottles.Dequeue();
                    DisplayBottle(new Bottle(removedBottle.ToString()));
                    AllCoins(removedBottle.BottlePrice);
                    Thread.Sleep(2000);
                }
            }
        }

        public void CarlsbergConsumer()
        {
            while (true)
            {
                // Lock for the carlsbergBottles.
                lock (carlsbergbottles)
                {
                    // A while loop to check if carlsbergBottles contains a object.
                    while (carlsbergbottles.Count == 0)
                    {
                        // As long the carlsbergBottles is empty, the thread will wait.
                        Monitor.Wait(carlsbergbottles);
                    }
                    // Removing the latest carlsberg from carlsbergBottles.
                    Carlsberg removedBottle = carlsbergbottles.Dequeue();
                    DisplayBottle(new Bottle(removedBottle.ToString()));
                    AllCoins(removedBottle.BottlePrice);
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
