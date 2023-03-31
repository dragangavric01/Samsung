using System;
using System.Collections.Generic;
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
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window {
        public Model SelectedModel { get; set; }

        public DetailsWindow(Model selectedModel) {
            SelectedModel = selectedModel;

            DataContext = this;
            InitializeComponent();

            tbName.Text = SelectedModel.Name;
            tbProdStartYear.Text = SelectedModel.ProductionStartYear.ToString();

            rtbEditor.Document = DataIO.ReadFromRtfFile(SelectedModel.RtfPath);

            image.Source = new BitmapImage(new Uri(SelectedModel.ImagePath, UriKind.Relative));
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
