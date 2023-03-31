using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Samsung_modeli {
    /// <summary>
    /// Interaction logic for GuestModelsWindow.xaml
    /// </summary>
    public partial class GuestModelsWindow : Window {
        public static ObservableCollection<Model> Models { get; set; }

        static GuestModelsWindow() {    // staticki konstruktor
            Models = DataIO.DeSerializeObject<ObservableCollection<Model>>("Files/models.xml");
            if (Models == null) {  // U slucaju da nista nije ucitano, napravim praznu listu
                Models = new ObservableCollection<Model>();
            }
        }

        public GuestModelsWindow() {
            DataContext = this;
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void hyperlinkName_Click(object sender, RoutedEventArgs e) {
            Model selectedModel = dgModels.SelectedItem as Model;

            DetailsWindow detailsWindow = new DetailsWindow(selectedModel);
            detailsWindow.Show();
        }
    }
}
