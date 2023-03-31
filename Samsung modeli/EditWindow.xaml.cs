using System;
using System.Collections.Generic;
using System.IO;
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
using static Samsung_modeli.DataIO;

namespace Samsung_modeli {
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window {
        public Model SelectedModel { get; set; }

        public EditWindow(Model selectedModel) {
            SelectedModel = selectedModel;

            DataContext = this;
            InitializeComponent();

            tbName.Text = SelectedModel.Name;
            tbProdStartYear.Text = SelectedModel.ProductionStartYear.ToString();

            cbFontSize.ItemsSource = Common.FontSizes;
            cbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);

            rtbEditor.Document = DataIO.ReadFromRtfFile(SelectedModel.RtfPath);

            cbImage.ItemsSource = Common.ImageNames;
            cbImage.SelectedItem = System.IO.Path.GetFileNameWithoutExtension(SelectedModel.ImagePath);

            imageSelected.Source = new BitmapImage(new Uri(SelectedModel.ImagePath, UriKind.Relative));
        }
        private void rtbEditor_TextChanged(object sender, TextChangedEventArgs e) {
            string text = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            tbWordCount.Text = "Word count: " + text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private void cbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbFontFamily.SelectedItem != null && !rtbEditor.Selection.IsEmpty) {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cbFontFamily.SelectedItem);
            }
        }

        private void cbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbFontSize.SelectedItem != null && !rtbEditor.Selection.IsEmpty) {
                int fontSize = (int)cbFontSize.SelectedItem;
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, (double)fontSize);
            }
        }

        private void ColorFeedBackOnClick() {
            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonRed.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            var start = rtbEditor.CaretPosition;
            var end = start.GetNextContextPosition(LogicalDirection.Backward);
            rtbEditor.Selection.Select(end, start);

            object temp = rtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty);
            string colorHex = temp.ToString();

            Thickness thickness3 = new Thickness(3);

            if (colorHex == Brushes.Black.ToString()) {
                buttonBlack.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Red.ToString()) {
                buttonRed.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Brown.ToString()) {
                buttonBrown.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Yellow.ToString()) {
                buttonYellow.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Green.ToString()) {
                buttonGreen.BorderThickness = thickness3;
            } else if (colorHex == Brushes.DarkBlue.ToString()) {
                buttonDarkBlue.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Purple.ToString()) {
                buttonPurple.BorderThickness = thickness3;
            }

            rtbEditor.Selection.Select(start, start);
        }

        private void ColorFeedbackOnSelect() {
            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonRed.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            object temp = rtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty);
            string colorHex = temp.ToString();

            Thickness thickness3 = new Thickness(3);

            if (colorHex == Brushes.Black.ToString()) {
                buttonBlack.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Red.ToString()) {
                buttonRed.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Brown.ToString()) {
                buttonBrown.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Yellow.ToString()) {
                buttonYellow.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Green.ToString()) {
                buttonGreen.BorderThickness = thickness3;
            } else if (colorHex == Brushes.DarkBlue.ToString()) {
                buttonDarkBlue.BorderThickness = thickness3;
            } else if (colorHex == Brushes.Purple.ToString()) {
                buttonPurple.BorderThickness = thickness3;
            }
        }

        private void FeedbackOnClick() {
            var start = rtbEditor.CaretPosition;
            var end = start.GetNextContextPosition(LogicalDirection.Backward);
            rtbEditor.Selection.Select(end, start);

            var fontFamily = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            var fontSize = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);

            cbFontFamily.SelectedItem = fontFamily;
            cbFontSize.SelectedItem = Int32.Parse(fontSize.ToString());

            ColorFeedBackOnClick();

            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            rtbEditor.Selection.Select(start, start);
        }

        private void FeedbackOnSelect() {
            var fontFamily = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            var fontSize = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            var fontColor = rtbEditor.Selection.GetPropertyValue(TextElement.ForegroundProperty);

            cbFontFamily.SelectedItem = fontFamily;

            if (fontSize != DependencyProperty.UnsetValue) {
                cbFontSize.SelectedItem = Int32.Parse(fontSize.ToString());
            } else {
                cbFontSize.SelectedItem = null;
            }

            ColorFeedbackOnSelect();

            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
        }

        private void rtbEditor_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (rtbEditor.Selection.Text == "") {
                FeedbackOnClick();
            } else {
                FeedbackOnSelect();
            }
        }

        private void buttonBlack_Click(object sender, RoutedEventArgs e) {
            rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, Brushes.Black);

            Thickness thickness0 = new Thickness(0);
            buttonRed.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            buttonBlack.BorderThickness = new Thickness(3);
        }

        private void buttonRed_Click(object sender, RoutedEventArgs e) {
            rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, Brushes.Red);

            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            buttonRed.BorderThickness = new Thickness(3);
        }

        private void buttonBrown_Click(object sender, RoutedEventArgs e) {
            rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, Brushes.Brown);

            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonRed.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            buttonBrown.BorderThickness = new Thickness(3);
        }

        private void buttonYellow_Click(object sender, RoutedEventArgs e) {
            rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, Brushes.Yellow);

            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonRed.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            buttonYellow.BorderThickness = new Thickness(3);
        }

        private void buttonGreen_Click(object sender, RoutedEventArgs e) {
            rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, Brushes.Green);

            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonRed.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            buttonGreen.BorderThickness = new Thickness(3);
        }

        private void buttonDarkBlue_Click(object sender, RoutedEventArgs e) {
            rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, Brushes.DarkBlue);

            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonRed.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonPurple.BorderThickness = thickness0;

            buttonDarkBlue.BorderThickness = new Thickness(3);
        }

        private void buttonPurple_Click(object sender, RoutedEventArgs e) {
            rtbEditor.Selection.ApplyPropertyValue(Inline.ForegroundProperty, Brushes.Purple);

            Thickness thickness0 = new Thickness(0);
            buttonBlack.BorderThickness = thickness0;
            buttonRed.BorderThickness = thickness0;
            buttonBrown.BorderThickness = thickness0;
            buttonYellow.BorderThickness = thickness0;
            buttonGreen.BorderThickness = thickness0;
            buttonDarkBlue.BorderThickness = thickness0;

            buttonPurple.BorderThickness = new Thickness(3);
        }

        private bool IsInputOk() {
            tbNameError.Visibility = Visibility.Hidden;
            tbProdStartYearError.Visibility = Visibility.Hidden;
            tbImageError.Visibility = Visibility.Hidden;

            bool inputOk = true;

            string trimmedName = tbName.Text.Trim();
            string trimmedProdStartYear = tbProdStartYear.Text.Trim();
            int prodStartYear;

            if (trimmedName == "") {
                tbNameError.Text = "Name must be entered";
                tbNameError.Visibility = Visibility.Visible;

                inputOk = false;
            } else if (trimmedName.Length >= 20) {
                tbNameError.Text = "Name must be shorter than 20 characters.";
                tbNameError.Visibility = Visibility.Visible;

                inputOk = false;
            } else {
                foreach (Model m in AdminModelsWindow.Models) {
                    if (trimmedName == m.Name && m != SelectedModel) {
                        tbNameError.Text = "The entered model already exists";
                        tbNameError.Visibility = Visibility.Visible;

                        inputOk = false;
                        break;
                    }
                }
            }

            if (trimmedProdStartYear == "") {
                tbProdStartYearError.Text = "Production start year must be entered";
                tbProdStartYearError.Visibility = Visibility.Visible;

                inputOk = false;
            } else if (!Int32.TryParse(trimmedProdStartYear, out prodStartYear)) {
                tbProdStartYearError.Text = "Production start year must be an integer";
                tbProdStartYearError.Visibility = Visibility.Visible;

                inputOk = false;
            } else if (prodStartYear < 1984 || prodStartYear > DateTime.Now.Year) {         // "It was around 1985 when Samsung built its first cell phone made for in-car use, the Samsung SC-1000. This is the first Samsung phone ever."
                tbProdStartYearError.Text = "Production start year is incorrect";
                tbProdStartYearError.Visibility = Visibility.Visible;

                inputOk = false;
            }

            foreach (Model m in AdminModelsWindow.Models) {
                if ("Images/" + cbImage.SelectedItem + ".jpg" == m.ImagePath && m != SelectedModel) {
                    tbImageError.Text = "This image is already in use";
                    tbImageError.Visibility = Visibility.Visible;

                    inputOk = false;
                    break;
                }
            }

            return inputOk;
        }


        private void buttonSave_Click(object sender, RoutedEventArgs e) {
            if (!IsInputOk()) {
                return;
            }

            Model editedModel = new Model();
            editedModel.Name = tbName.Text.Trim();
            editedModel.ProductionStartYear = Int32.Parse(tbProdStartYear.Text.Trim());
            editedModel.AddDate = SelectedModel.AddDate;
            editedModel.ImagePath = "Images/" + cbImage.SelectedItem + ".jpg";
            editedModel.RtfPath = "Files/" + tbName.Text.Trim() + ".rtf";

            if (editedModel.Equals(SelectedModel)) {    // ako korisnik nije nista mijenjao (osim rtf)
                DataIO.WriteToExistingRtfFile(SelectedModel.RtfPath, rtbEditor.Document);
                Close();
            } else {
                if (editedModel.Name == SelectedModel.Name) {
                    DataIO.WriteToExistingRtfFile(SelectedModel.RtfPath, rtbEditor.Document);
                        } else {
                    DataIO.WriteToNewRtfFile(editedModel.RtfPath, rtbEditor.Document);

                    if (File.Exists(SelectedModel.RtfPath)) {
                        File.Delete(SelectedModel.RtfPath);
                    }
                }

                AdminModelsWindow.Models[AdminModelsWindow.Models.IndexOf(SelectedModel)] = editedModel;

                Close();
            }
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void cbImage_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            imageSelected.Source = new BitmapImage(new Uri("Images/" + cbImage.SelectedItem + ".jpg", UriKind.Relative));
            tbImageError.Visibility = Visibility.Hidden;
        }
    }
}
