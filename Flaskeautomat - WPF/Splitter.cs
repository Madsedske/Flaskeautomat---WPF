using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flaskeautomat___WPF
{
    public class Splitter
    {
        Queue<Bottle> producedBottles;
        Queue<CocaCola> cocaColaBottles;
        Queue<Heineken> heinekenBottles;
        Queue<Carlsberg> carlsbergbottles;
        Queue<Urge> urgebottles;
        Queue<FaxeKondi> faxeKondiBottles;

        public Splitter(Queue<Bottle> produced, Queue<CocaCola> producedCocaCola, Queue<Heineken> producedHeineken, Queue<Carlsberg> producedCarlsberg, Queue<Urge> producedUrge, Queue<FaxeKondi> producedFaxeKondi)
        {            
            producedBottles = produced;
            cocaColaBottles = producedCocaCola;
            heinekenBottles = producedHeineken;
            carlsbergbottles = producedCarlsberg;
            urgebottles = producedUrge;
            faxeKondiBottles = producedFaxeKondi;
        }

        public void SplitterCon()
        {
            while (true)
            {
                lock (producedBottles) // Lock for the producedBottles.
                {                    
                    while (producedBottles.Count == 0) // A while loop to check if producedBottles is null.
                    {                       
                        Monitor.Wait(producedBottles);  // As long the producedBottles is empty, the thread will wait.
                    }                    
                    for (int i = 0; i < producedBottles.Count; i++) // Looping as long as the producedBottles is filled.
                    {                        
                        string checkBottle = producedBottles.Peek().BottleName; // Get the name for the latest bottle in producedBottle queue.
                        Bottle moveBottle = producedBottles.Dequeue();
                        switch (checkBottle)
                        {
                            case "Coca Cola":                                
                                lock (cocaColaBottles)
                                {
                                    cocaColaBottles.Enqueue(new CocaCola(moveBottle.ToString()));
                                    Monitor.PulseAll(cocaColaBottles);
                                }                                
                                break;
                            case "Heineken":
                                lock (heinekenBottles)
                                {
                                    heinekenBottles.Enqueue(new Heineken(moveBottle.ToString()));
                                    Monitor.PulseAll(heinekenBottles);
                                }
                                break;
                            case "Faxe Kondi":
                                lock (faxeKondiBottles)
                                {
                                    faxeKondiBottles.Enqueue(new FaxeKondi(moveBottle.ToString()));
                                    Monitor.PulseAll(faxeKondiBottles);
                                }
                                break;
                            case "Urge":
                                lock (urgebottles)
                                {
                                    urgebottles.Enqueue(new Urge(moveBottle.ToString()));
                                    Monitor.PulseAll(urgebottles);
                                }
                                break;
                            case "Carlsberg":
                                lock (carlsbergbottles)
                                {
                                    carlsbergbottles.Enqueue(new Carlsberg(moveBottle.ToString()));
                                    Monitor.PulseAll(carlsbergbottles);
                                }
                                break;
                            default:
                                break;
                        }
                        Monitor.PulseAll(producedBottles);
                    }
                }
            }
        }
    }
}