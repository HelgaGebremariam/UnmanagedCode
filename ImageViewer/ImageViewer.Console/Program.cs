using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace ImageViewer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[4] { 30000, 32000, -234234, 23425 };

            IntPtr unmanagedPointer = Marshal.AllocHGlobal(array.Length);
            Marshal.Copy(array, 0, unmanagedPointer, array.Length);
            var arr = Services.ImageService.MakeImageBlackAndWhiteStatic(unmanagedPointer, 2, 2);
            Marshal.FreeHGlobal(unmanagedPointer);
            int[] darr = new int[4];
            Marshal.Copy(arr, darr, 0, darr.Length);

        }
    }
}
