using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AdminModelsWindow.xaml
    /// </summary>
    public partial class AdminModelsWindow : Window {
        public static ObservableCollection<Model> Models { get; set; }
        public static List<int> checkedModelsIndexes = new List<int>();

        static AdminModelsWindow() {    // staticki konstruktor
            Models = DataIO.DeSerializeObject<ObservableCollection<Model>>("Files/models.xml");
            if (Models == null) {  // U slucaju da nista nije ucitano, napravim praznu listu
                Models = new ObservableCollection<Model>();
            }
        }

        public AdminModelsWindow() {
            DataContext = this;

            InitializeComponent();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            DataIO.SerializeObject<ObservableCollection<Model>>(Models, "Files/models.xml");
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e) {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e) {
            if (checkedModelsIndexes.Count == 0) {
                MessageBox.Show("No models selected. You must check the boxes for models you want to delete.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                string selectedModelsNames = "\n";
                foreach (int index in checkedModelsIndexes) {
                    selectedModelsNames += "\n" + "\t" + Models[index].Name;
                }

                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete these models?" + selectedModelsNames, "Message", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.No) {
                    return;
                } else {
                    List<Model> selectedModels = new List<Model>();

                    foreach (int index in checkedModelsIndexes) {
                        selectedModels.Add(Models[index]);    // ne mogu odmah da brisem po indeksima sa RemoveAt jer kad se obrise jedan model onda indeksi modela iza njega smanje za jedan
                    }

                    foreach (Model m in selectedModels) {
                        if (File.Exists("Files/" + m.Name + ".rtf")) {
                            File.Delete("Files/" + m.Name + ".rtf");
                        }

                        Models.Remove(m);
                    }

                    checkedModelsIndexes.Clear();
                    dgModels.UnselectAll();     // zato sto iz nekog razloga posle brisanja selektuje prvi red pa se checkira prvi checkbox
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            checkedModelsIndexes.Add(dgModels.SelectedIndex);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            checkedModelsIndexes.Remove(dgModels.SelectedIndex);
        }

        private void hyperlinkName_Click(object sender, RoutedEventArgs e) {
            Model selectedModel = dgModels.SelectedItem as Model;

            foreach (Model m in Models) {
                if (m.Name == selectedModel.Name) {
                    EditWindow editWindow = new EditWindow(m);  // jer hocu da mi SelectedModel bude bas taj objekat koji se nalazi u listi
                    editWindow.Show();
                    break;
                }
            }

        }
    }
}
