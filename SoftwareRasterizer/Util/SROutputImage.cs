using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class SROutputImage
{
    public static void ExportImage(SRColor[,] pixels, string path)
    {
        var width = pixels.GetLength(0);
        var height = pixels.GetLength(1);
        
        using var image = new Image<Rgba32>(width,height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++) 
            {
                var pixel = pixels[x, y];
                image[x, y] = new Rgba32(pixel.R, pixel.G, pixel.B);
            }
        }

        //export
        image.Save(path);
        
        Console.WriteLine("画像を出力しました fullPath:" + Path.GetFullPath(path));
    }
}