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
        public static extern IntPtr MakeImageBlackAndWhiteStatic(IntPtr array, int height, int width);

        public struct RGB
        {
            public byte B;
            public byte G;
            public byte R;
            public byte A;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct IntRgb
        {
            [FieldOffset(0)]
            public RGB rgbValue;
            [FieldOffset(0)]
            public int intValue;
        };

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
            Bitmap result = new Bitmap(source.Width, source.Height);


            int[] arr = BitmapToIntArray(source);
            int width = source.Width;
            int height = source.Height;
            for(int i = 0; i < source.Width; i++)
                for(int j = 0; j < source.Height; j++)
                {
                    int pix = source.GetPixel(i, j).ToArgb();
                    int r = (pix >> 16) & 0xFF;
                    int green = (pix >> 8) & 0xFF;
                    int blue = pix & 0xFF;
                    IntRgb intRgb = new IntRgb() { intValue = arr[i * source.Height + j] };
                     result.SetPixel(i, j, Color.FromArgb(intRgb.rgbValue.R, intRgb.rgbValue.G, intRgb.rgbValue.B));
                }

            return result;
        }
    }
}
