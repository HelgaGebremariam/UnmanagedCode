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
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using ImageViewer.Services;

namespace ImageViewer.Presentation
{
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
            }
        }

        private void InitializeSourceImageFromFile()
        {
            sourceImage = new Bitmap(sourceImageFileName);
            currentImage = sourceImage;
            ResizeCurrentImage();
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
            
        }

        private void btnBlackAndWhite_Click(object sender, RoutedEventArgs e)
        {
            MakeBlackAndWhite();
            ShowCurrentImage();
        }

        private void ResizeCurrentImage()
        {
            int height = (int)imgCurrentImage.Height;
            int width = (int)imgCurrentImage.Width;
            double scale = 1;
            if(currentImage.Height < currentImage.Width)
            {
                scale = (double)height / (double)currentImage.Height;
            }
            else
            {
                scale = (double)width/(double)currentImage.Width;
            }
            currentImage = new Bitmap(currentImage, (int)(currentImage.Width * scale), (int)(currentImage.Height * scale));
        }
    }
}
