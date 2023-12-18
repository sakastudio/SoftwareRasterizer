using System.Diagnostics;
using SoftwareRasterizer.Struct;
using SoftwareRasterizer.Util;

namespace SoftwareRasterizer.Step1;

public class VertexTransform
{
    public static void _Main()
    {
        const int width = 64 * 4;
        const int height = 36 * 4;
        
        var clipSpaceVertex = VertexTransform.CreateConvertedVertex(
            @"G:\RiderProjects\SoftwareRasterizer\SoftwareRasterizer\Asset\teapod.obj", 
            new SRVector3(0, 0, 0),
            new SRVector3(180, 90, 0), 
            new SRVector3(1, 1, 1),
            new SRVector2(width,height));

        // 出力画像に変換する
        var pixels = new SRColor[width, height];
        var path = "step1.png";
        foreach (var vertex in clipSpaceVertex)
        {
            var point = vertex.Position;
            var x = (int)((point.X / point.W + 1) * 0.5f * width);
            var y = (int)((1 - point.Y / point.W) * 0.5f * height);
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                continue;
            }

            pixels[x, y] = new SRColor(255, 255, 255);
        }

        SROutputImage.ExportImage(pixels, path);
        //画像ファイルを開く
        using var ps1 = new Process();
        ps1.StartInfo.UseShellExecute = true;
        ps1.StartInfo.FileName = path;
        ps1.Start();
    }


    public static List<SRVertex> CreateConvertedVertex(string path, SRVector3 objectPos, SRVector3 objectRotateDegree, SRVector3 objectScale,SRVector2 screenSize)
    {
        var teaPodVertex = SRImageExporter.LoadVertex(path);

        //MVP変換をする
        {
            // モデル変換行列（オブジェクト座標系からワールド座標系へ変換する）
            //http://marupeke296.sakura.ne.jp/DXG_No39_WorldMatrixInformation.html
            var posMatrix = new SRMatrix4x4(
                1, 0, 0, objectPos.X,
                0, 1, 0, objectPos.Y,
                0, 0, 1, objectPos.Z,
                0, 0, 0, 1
            );

            var objectRoteRadius = new SRVector3(
                objectRotateDegree.X * (MathF.PI / 180),
                objectRotateDegree.Y * (MathF.PI / 180),
                objectRotateDegree.Z * (MathF.PI / 180)
            );
            //回転行列をどうやって表すのか
            // https://rikei-tawamure.com/entry/2019/11/04/184049
            var rotXMatrix = new SRMatrix4x4(
                1, 0, 0, 0,
                0, cos(objectRoteRadius.X), -sin(objectRoteRadius.X), 0,
                0, sin(objectRoteRadius.X), cos(objectRoteRadius.X), 0,
                0, 0, 0, 1
            );
            var rotYMatrix = new SRMatrix4x4(
                cos(objectRoteRadius.Y), 0, sin(objectRoteRadius.Y), 0,
                0, 1, 0, 0,
                -sin(objectRoteRadius.Y), 0, cos(objectRoteRadius.Y), 0,
                0, 0, 0, 1
            );
            var rotZMatrix = new SRMatrix4x4(
                cos(objectRoteRadius.Z), -sin(objectRoteRadius.Z), 0, 0,
                sin(objectRoteRadius.Z), cos(objectRoteRadius.Z), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );


            var scaleMatrix = new SRMatrix4x4(
                objectScale.X, 0, 0, 0,
                0, objectScale.Y, 0, 0,
                0, 0, objectScale.Z, 0,
                0, 0, 0, 1
            );

            var a = MatrixUtil.Multi(posMatrix, rotYMatrix);
            var b = MatrixUtil.Multi(a, rotXMatrix);
            var c = MatrixUtil.Multi(b, rotZMatrix);
            var modelMatrix = MatrixUtil.Multi(c, scaleMatrix);

            foreach (var teaPodPoint in teaPodVertex)
            {
                teaPodPoint.Position = MatrixUtil.Multi(modelMatrix, teaPodPoint.Position);
            }
        }


        {
            // ビュー変換行列 （ワールド座標からカメラ座標への変換）
            // https://yttm-work.jp/gmpg/gmpg_0003.html
            //http://marupeke296.com/DXG_No72_ViewProjInfo.html
            var cameraPos = new SRVector3(20, 10, 20);
            var cameraTarget = new SRVector3(0, 0, 0);
            var cameraUp = new SRVector3(0, 1, 0);

            var forward = Vector3Util.Normalize(cameraTarget - cameraPos);
            var right = Vector3Util.Normalize(Vector3Util.Cross(cameraUp, forward));
            var up = Vector3Util.Cross(forward, right);

            var viewMatrix = new SRMatrix4x4(
                right.X, right.Y, right.Z, -Vector3Util.Dot(right, cameraPos),
                up.X, up.Y, up.Z, -Vector3Util.Dot(up, cameraPos),
                forward.X, forward.Y, forward.Z, -Vector3Util.Dot(forward, cameraPos),
                0, 0, 0, 1);

            foreach (var teaPodPoint in teaPodVertex)
            {
                teaPodPoint.Position = MatrixUtil.Multi(viewMatrix, teaPodPoint.Position);
            }
        }


        {
            //プロジェクション座標変換行列（カメラ座標からクリップ座標への変換）
            //https://yttm-work.jp/gmpg/gmpg_0004.html
            //http://marupeke296.com/DXG_No70_perspective.html
            const float viewAngle = 100 * (MathF.PI / 180);
            const float cameraNear = 0.1f;
            const float cameraFar = 100;
            var aspectRate = screenSize.Y / screenSize.X;


            var perspectiveMatrix = new SRMatrix4x4(
                1 / (float)Math.Tan(viewAngle / 2) / aspectRate, 0, 0, 0,
                0, 1 / (float)Math.Tan(viewAngle / 2), 0, 0,
                0, 0, 1 / (cameraFar - cameraNear) * cameraFar, 1,
                0, 0, -cameraNear / (cameraFar - cameraNear) * cameraFar, 0
            );

            foreach (var teaPodPoint in teaPodVertex)
            {
                teaPodPoint.Position = MatrixUtil.Multi(perspectiveMatrix, teaPodPoint.Position);
            }

        }
        {
            //デバイス座標系変換
            
            foreach (var teaPodPoint in teaPodVertex)
            {
                teaPodPoint.Position.X /= teaPodPoint.Position.W;
                teaPodPoint.Position.Y /= teaPodPoint.Position.W;
                teaPodPoint.Position.Z /= teaPodPoint.Position.W;
                teaPodPoint.Position.W /= teaPodPoint.Position.W;
            }
        }
        
        //TODO クリップ座標系の頂点をクリッピングする
   
        return teaPodVertex;
    }

    private static float sin(float radian)
    {
        return (float)Math.Sin(radian);
    }

    private static float cos(float radian)
    {
        return (float)Math.Cos(radian);
    }
}