using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace BillRecordingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    //створити binding list до listview 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            

            var list = new ObservableCollection<Test>();
            list.Add(new Test());
            list.Add(new Test());
            list.Add(new Test());
            list.Add(new Test());

            gridExpences.ItemsSource = list;

            var el = gridExpences.SelectedItem as Test;
        }

        class Test
        {
            public string var1 { get; set; } = "1421";
            public string var2 { get; set; } = "1421";
            public string var3 { get; set; } = "1421";
            public string var4 { get; set; } = "1421";
            public string var5 { get; set; } = "markopidar";

        }

    }
}
