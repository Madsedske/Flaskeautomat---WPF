using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flaskeautomat___WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creation for the 3 queue.
        static Queue<Bottle> producedBottles = new Queue<Bottle>();
        static Queue<CocaCola> cocaColaBottles = new Queue<CocaCola>();
        static Queue<Heineken> heinekenBottles = new Queue<Heineken>();
        static Queue<Carlsberg> carlsbergBottles = new Queue<Carlsberg>();
        static Queue<Urge> urgeBottles = new Queue<Urge>();
        static Queue<FaxeKondi> faxeKondiBottles = new Queue<FaxeKondi>();

        static RecycleMachine recycleMachine = new RecycleMachine(producedBottles);
        static Splitter splitter = new Splitter(producedBottles, cocaColaBottles, heinekenBottles, carlsbergBottles, urgeBottles, faxeKondiBottles);
        static Consumer consumer = new Consumer(cocaColaBottles, heinekenBottles, carlsbergBottles, urgeBottles, faxeKondiBottles);

        public MainWindow()
        {
            InitializeComponent();
            Thread threadSplitter = new Thread(splitter.SplitterCon);
            Thread threadColaConsumer = new Thread(consumer.ColaConsumer);
            Thread threadHeinekenConsumer = new Thread(consumer.HeinekenConsumer);
            Thread threadFaxeKondiConsumer = new Thread(consumer.FaxeKondiConsumer);
            Thread threadUrgeConsumer = new Thread(consumer.UrgeConsumer);
            Thread threadCarlsbergConsumer = new Thread(consumer.CarlsbergConsumer);

            threadSplitter.Start();
            threadCarlsbergConsumer.Start();
            threadFaxeKondiConsumer.Start();
            threadUrgeConsumer.Start();
            threadColaConsumer.Start();
            threadHeinekenConsumer.Start();       

            consumer.bottleHandlerEvent += Machine_bottleHandlerEvent;
            consumer.costumerHandlerEvent += Machine_MoneyToCollect;
        }

        private void Machine_bottleHandlerEvent(object? sender, Bottle e)
        {
            Dispatcher.Invoke(() => { Display.Content = e.BottleName.ToString(); });
        }

        public void Machine_MoneyToCollect(object? sender, CostumersMoney e)
        {
            Dispatcher.Invoke(() => { AllCoins.Content = e.Money.ToString() + " Kr"; });
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            // Let the window be minimized.
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            // Controls for the window to be maximized or normal.
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            // Regular close button
            Application.Current.Shutdown();
        }

        private void DragImage(object sender, MouseButtonEventArgs e)
        {
            // Using MouseButtonEventArgs to store the source of the image and making it into an dataobject. 
            Image? image = e.Source as Image;
            DataObject data = new DataObject(typeof(Image), image);
            DragDrop.DoDragDrop(image, data, DragDropEffects.Link);
        }

        private void DropImage(object sender, DragEventArgs e)
        {       
            // Get the data from the dragged image.
            Image? image = e.Data.GetData(typeof(Image)) as Image;
            if (image is not null)
            {
                ImageSource image1 = image.Source; // Store the dragged image source into the image.
                string[] path = ((BitmapFrame)image1).Decoder.ToString().Split('/'); // Using a decoder ToString to split up the path where '/' is.
                string fileName = path[path.Length - 1].Split('.')[0]; // Another splitter, but clear what's coming after '.' such as '.png'
                string actualName = Regex.Replace(fileName, "([a-z])([A-Z])", "$1 $2"); // If the final word we get from filename have a 'A-Z' inside the word, we make a space between.
                recycleMachine.ProduceBottle(actualName);
            }
        }

        private void moneyButton_Click(object sender, RoutedEventArgs e)
        {
            consumer.WithdrawMoney();
        }
    }
}