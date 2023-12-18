using System.Diagnostics;
using SoftwareRasterizer.Step1;
using SoftwareRasterizer.Struct;
using SoftwareRasterizer.Util;

namespace SoftwareRasterizer.Step2;

public class Rasterizer
{
    public static void Main()
    {
        const int width = 64 * 4;
        const int height = 36 * 4;
        
        var clipSpaceVertex = VertexTransform.CreateConvertedVertex(
            @"G:\RiderProjects\SoftwareRasterizer\SoftwareRasterizer\Asset\teapod.obj", 
            new SRVector3(0, 0, 0),
            new SRVector3(180, 90, 0), 
            new SRVector3(1, 1, 1),
            new SRVector2(width,height));
        
        
        var pixels = new SRColor[width, height];
        var path = "step2.png";

        var faces = clipSpaceVertex.GetFaces();

        for (int x = 0; x < width; x++)
        {
            Console.WriteLine($"x:{x}");
            for (int y = 0; y < height; y++)
            {
                var rasterizeX = 1.0f / width + x * (2.0f / width) - 1.0f;
                var rasterizeY = 1.0f / height + y * (2.0f / height) - 1.0f;

                foreach (var face in faces)
                {
                    var bb = face.GetBoundingBox();
                    //var bbScreenMin = GetScreenPos(bb.Min, width, height);

                    if (rasterizeX < bb.Min.X || rasterizeX > bb.Max.X ||
                        rasterizeY < bb.Min.Y || rasterizeY > bb.Max.Y)
                    {
                        continue;
                    }

                    var A = GetScreenPos(face[0].Position, width, height);
                    var B = GetScreenPos(face[1].Position, width, height);
                    var C = GetScreenPos(face[2].Position, width, height);

                    // Edge関数の計算
                    var edgeA = (C.X - B.X) * (rasterizeY - B.Y) - (C.Y - B.Y) * (rasterizeX - B.X); // BC×BP
                    var edgeB = (A.X - C.X) * (rasterizeY - C.Y) - (A.Y - C.Y) * (rasterizeX - C.X); // CA×CP
                    var edgeC = (B.X - A.X) * (rasterizeY - A.Y) - (B.Y - A.Y) * (rasterizeX - A.X); // AB×AP

                    if (edgeA >= 0 & edgeB >= 0 & edgeC >= 0)
                    {
                        // 三角形の内側(表側)であればラスタライズ
                        // TODO z-バッファ法 (より手前にあるものを描画)
                        pixels[x, y] = new SRColor(255, 255, 255);
                    }
                }
            }
        }
        
        SROutputImage.ExportImage(pixels, path);
        //画像ファイルを開く
        using var ps1 = new Process();
        ps1.StartInfo.UseShellExecute = true;
        ps1.StartInfo.FileName = path;
        ps1.Start();
    }

    private static SRVector2 GetScreenPos(SRVector4 perspectivePos, int width, int height)
    {
        var screenX = (int)((perspectivePos.X / perspectivePos.W + 1) * 0.5f * width);
        var screenY = (int)((1 - perspectivePos.Y / perspectivePos.W) * 0.5f * height);
        return new SRVector2(screenX, screenY);
    }
}