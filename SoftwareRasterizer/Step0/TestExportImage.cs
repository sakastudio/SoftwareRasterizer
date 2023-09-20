using SoftwareRasterizer.Struct;
using SoftwareRasterizer.Util;

namespace SoftwareRasterizer.Step0;

public class TestExportImage
{
    public static void Main()
    {
        var pixels = new SRColor[255, 255];
        var path = "test.png";

        for (int x = 0; x < 255; x++)
        {
            for (int y = 0; y < 255; y++)
            {
                pixels[x, y] = new SRColor
                {
                    R = (byte)x,
                    G = (byte)y,
                    B = 0
                };
            }
        }
        
        OutputImage.ExportImage(pixels,path);
    }
}