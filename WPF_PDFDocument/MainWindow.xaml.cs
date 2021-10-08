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
using System.Windows.Navigation;

using Microsoft.Win32;

namespace WPF_PDFDocument
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _pdfPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // printDialog.PrintDocument(((IDocumentPaginatorSource) pdf_images).DocumentPaginator,"Flow Document Print Job");
                printDialog.PrintVisual(PdfImages, "Print Button");
            }
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            var fd = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "pdf files (*.pdf)|*.pdf",
                RestoreDirectory = true
            };

            if (fd.ShowDialog() == true)
            {
                //Get the path of specified file
                _pdfPath = fd.FileName;
                PdfImages.PdfPath = _pdfPath;
            }
        }


        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(files[0]) == ".pdf")
                {
                    PdfImages.PdfPath = files[0];
                }
            }
        }

        private void ContentElement_OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }
    }
}
