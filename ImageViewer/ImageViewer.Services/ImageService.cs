using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ImageViewer.Services
{
    public class ImageService
    {
        [DllImport("F:\\.net mentoring\\Hometask\\UnmanagedCode\\ImageViewer\\x64\\Release\\ImageViewer.CppJavaBridge.dll", EntryPoint = "?MakeImageBlackAndWhite@@YAPEAHPEAHHH@Z")]
        public static extern IntPtr MakeImageBlackAndWhiteStatic(int[] array, int height, int width);

        private int[] BitmapToIntArray(Bitmap source)
        {
            int[] result = new int[source.Height * source.Width];
            for (int i = 0; i < source.Width; i++)
                for (int j = 0; j < source.Height; j++)
                {
                    result[i * source.Height + j] = source.GetPixel(i, j).ToArgb();
                }
            return result;
        }

        private Bitmap IntArrayToBitmap(int[] arr, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    result.SetPixel(i, j, Color.FromArgb(arr[i * height + j]));
                }
            return result;
        }

        public Bitmap MakeImageBlackAndWhite(Bitmap source)
        {
            int[] result = new int[source.Width*source.Height];
            int[] array = BitmapToIntArray(source);
            IntPtr resultArray = MakeImageBlackAndWhiteStatic(array, array.Length, 1);
            Marshal.Copy(resultArray, result, 0, array.Length);
            return IntArrayToBitmap(result, source.Width, source.Height);

        }
    }
}
