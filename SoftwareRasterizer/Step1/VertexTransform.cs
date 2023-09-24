using SoftwareRasterizer.Struct;
using SoftwareRasterizer.Util;

namespace SoftwareRasterizer.Step1;

public class VertexTransform
{
    public static void Main()
    {
        var boxObject = new List<SRVector3>()
        {
            new(0, 0, 0),
            new(0, 1, 0),
            new(1, 1, 0),
            new(1, 0, 0),
            new(0, 0, 1),
            new(0, 1, 1),
            new(1, 1, 1),
            new(1, 0, 1),
        };

        // ビュー変換行列 https://yttm-work.jp/gmpg/gmpg_0003.html
        var cameraPos = new SRVector3(5, 5, 5);
        var cameraTarget = new SRVector3(0, 0, 0);
        var cameraUp = new SRVector3(0, 1, 0);
    }
    
}