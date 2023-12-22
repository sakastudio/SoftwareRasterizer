using System.Diagnostics;
using SoftwareRasterizer.Step1;
using SoftwareRasterizer.Struct;
using SoftwareRasterizer.Util;

namespace SoftwareRasterizer.Step2;

public class Rasterizer
{
    public static void Main()
    {
        const int width = 64 * 1;
        const int height = 36 * 1;
        
        var clipSpaceVertex = VertexTransform.CreateConvertedVertex(
            TeapotPath.Path, 
            new SRVector3(0, 0, 0),
            new SRVector3(0, 90, 0), 
            new SRVector3(1, 1, 1),
            new SRVector2Int(width,height));
        
        //ライトの設定
        var lightDirection = new SRVector3(-1f, 1f, 0f);
        
        
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
                    var bb = face.GetClippingBoundingBox();

                    if (rasterizeX < bb.Min.X || rasterizeX > bb.Max.X ||
                        rasterizeY < bb.Min.Y || rasterizeY > bb.Max.Y)
                    {
                        continue;
                    }

                    var A = face[0].ClipPosition;
                    var B = face[1].ClipPosition;
                    var C = face[2].ClipPosition;

                    // Edge関数の計算
                    var edgeA = (C.X - B.X) * (rasterizeY - B.Y) - (C.Y - B.Y) * (rasterizeX - B.X); // BC×BP
                    var edgeB = (A.X - C.X) * (rasterizeY - C.Y) - (A.Y - C.Y) * (rasterizeX - C.X); // CA×CP
                    var edgeC = (B.X - A.X) * (rasterizeY - A.Y) - (B.Y - A.Y) * (rasterizeX - A.X); // AB×AP

                    if (edgeA >= 0 & edgeB >= 0 & edgeC >= 0)
                    {
                        // TODO z-バッファ法 (より手前にあるものを描画)
                        //ノーマル方向を計算する
                        var worldA = new SRVector3(face[0].WorldPosition.X, face[0].WorldPosition.Y, face[0].WorldPosition.Z);
                        var worldB = new SRVector3(face[1].WorldPosition.X, face[1].WorldPosition.Y, face[1].WorldPosition.Z);
                        var worldC = new SRVector3(face[2].WorldPosition.X, face[2].WorldPosition.Y, face[2].WorldPosition.Z);
                        var normal = SRVector3.Cross(worldB - worldA, worldC - worldA);
                        normal = normal.Normalize();
                        
                        
                        //ライトとノーマルの内積を計算する
                        var color = 0.2f + 0.8f * MathF.Max(0, SRVector3.Dot(normal, lightDirection));
                        
                        pixels[x, y] = new SRColor(color, color, color);
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