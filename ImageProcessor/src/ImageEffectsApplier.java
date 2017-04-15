/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Volha
 */

public class ImageEffectsApplier {
    public static void main(String[] args)
    {

    }
    
    public int[] MakeImageBlackAndWhite(int[] sourceImage)
    {
        int [] result = new int[sourceImage.length];
        for(int i = 0; i < sourceImage.length; i++)
        {
            int red = (sourceImage[i] >> 16) & 0xFF;
            int green = (sourceImage[i] >> 8) & 0xFF;
            int blue = sourceImage[i] & 0xFF;
            int each = (int)(((float)(red + green + blue))/3);
            red = each;
            green = each;
            blue = each;
            result[i] = red;
            result[i] = (result[i] << 8) + green;
            result[i] = (result[i] << 8) + blue;
        }
        return result;
    }
}
