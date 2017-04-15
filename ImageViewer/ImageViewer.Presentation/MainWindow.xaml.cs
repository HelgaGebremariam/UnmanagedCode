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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using ImageViewer.Services;

namespace ImageViewer.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string sourceImageFileName;
        System.Drawing.Bitmap sourceImage;
        System.Drawing.Bitmap currentImage;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                sourceImageFileName = openFileDialog.FileName;
                InitializeSourceImageFromFile();
                ShowCurrentImage();
                MakeBlackAndWhite();
            }
        }

        private void InitializeSourceImageFromFile()
        {
            sourceImage = new Bitmap(sourceImageFileName);
            currentImage = sourceImage;
        }

        private void ShowCurrentImage()
        {
            using (MemoryStream memory = new MemoryStream())
            {
                currentImage.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                imgCurrentImage.Source = bitmapImage;
            }
        }

        private void MakeBlackAndWhite()
        {
            ImageService service = new ImageService();
            currentImage = service.MakeImageBlackAndWhite(currentImage);
            ShowCurrentImage();
        }
    }
}
