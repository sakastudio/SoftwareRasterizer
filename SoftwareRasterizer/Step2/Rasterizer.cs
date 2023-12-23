using System.Diagnostics;
using SoftwareRasterizer.Step1;
using SoftwareRasterizer.Struct;
using SoftwareRasterizer.Util;

namespace SoftwareRasterizer.Step2;

public class Rasterizer
{
    public static void Main()
    {
        const int width = 64 * 5;
        const int height = 36 * 5;
        
        var (vertices,faces) = VertexTransform.CreateConvertedVertex(
            TeapotPath.Path, 
            new SRVector3(0, 0, 0),
            new SRVector3(0, 90, 0), 
            new SRVector3(1, 1, 1),
            new SRVector2Int(width,height));
        
        //ライトの設定
        var lightDirection = new SRVector3(-1f, 1f, 0f);
        
        
        var pixels = new SRColor[width, height];
        var zBuffer = new float[width, height];
        
        
        var path = "step2.png";


        for (int x = 0; x < width; x++)
        {
            Console.WriteLine($"x:{x}");
            for (int y = 0; y < height; y++)
            {
                zBuffer[x, y] = float.MaxValue;
                var rasterizeX = 1.0f / width + x * (2.0f / width) - 1.0f;
                var rasterizeY = 1.0f / height + y * (2.0f / height) - 1.0f;

                foreach (var face in faces)
                {
                    var bb = face.GetClippingBoundingBox(vertices);

                    if (rasterizeX < bb.Min.X || rasterizeX > bb.Max.X ||
                        rasterizeY < bb.Min.Y || rasterizeY > bb.Max.Y)
                    {
                        continue;
                    }
                    
                    var AVertex = vertices[face[0]];
                    var BVertex = vertices[face[1]];
                    var CVertex = vertices[face[2]];

                    var A = AVertex.ClipPosition;
                    var B = BVertex.ClipPosition;
                    var C = CVertex.ClipPosition;

                    // Edge関数の計算
                    var edgeA = (C.X - B.X) * (rasterizeY - B.Y) - (C.Y - B.Y) * (rasterizeX - B.X); // BC×BP
                    var edgeB = (A.X - C.X) * (rasterizeY - C.Y) - (A.Y - C.Y) * (rasterizeX - C.X); // CA×CP
                    var edgeC = (B.X - A.X) * (rasterizeY - A.Y) - (B.Y - A.Y) * (rasterizeX - A.X); // AB×AP

                    if (!(edgeA >= 0 & edgeB >= 0 & edgeC >= 0)) continue;
                    
                    float lambda_A, lambda_B, lambda_C;
                    var temp = edgeA + edgeB + edgeC;
                    lambda_A = edgeA/temp;
                    lambda_B = edgeB/temp;
                    lambda_C = edgeC/temp;
                    var depth = lambda_A * A.Z + lambda_B * B.Z + lambda_C * C.Z;
                    if (zBuffer[x, y] < depth)
                    {
                        //continue;
                    }
                    
                    
                    // z-バッファ法 (より手前にあるものを描画)
                    zBuffer[x, y] = depth;
                        
                    //ノーマル方向を計算する
                    var worldA = new SRVector3(AVertex.WorldPosition.X, AVertex.WorldPosition.Y, AVertex.WorldPosition.Z);
                    var worldB = new SRVector3(BVertex.WorldPosition.X, BVertex.WorldPosition.Y, BVertex.WorldPosition.Z);
                    var worldC = new SRVector3(CVertex.WorldPosition.X, CVertex.WorldPosition.Y, CVertex.WorldPosition.Z);
                    var normal = SRVector3.Cross(worldB - worldA, worldC - worldA);
                    normal = normal.Normalize();
                        
                    //ライトとノーマルの内積を計算する
                    var color = 0.2f + 0.8f * MathF.Max(0, SRVector3.Dot(normal, lightDirection));
                        
                    pixels[x, y] = new SRColor(color, color, color);
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
}