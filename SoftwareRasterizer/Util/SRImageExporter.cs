using ObjLoader.Loader.Loaders;
using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class SRImageExporter
{
    public static List<SRVector3> LoadVertex(string path)
    {
        var result = LoadObj(path);

        var vertices = new List<SRVector3>();
        foreach (var vertex in result.Vertices)
        {
            vertices.Add(new SRVector3
            {
                X = vertex.X,
                Y = vertex.Y,
                Z = vertex.Z
            });
        }

        return vertices;
    }

    public static List<SRVertex> LoadVertex2(string path)
    {
        var result = LoadObj(path);

        var vertices = new List<SRVertex>();
        foreach (var vertex in result)
        {
            var faceIndexList = new List<int>();
            foreach (var VARIABLE in vertex)
            {
                
            }
            var a = new SRVertex()
            {
                VertexIndex = vertices.Count,
                Position = new SRVector3
                {
                    X = vertex.X,
                    Y = vertex.Y,
                    Z = vertex.Z
                }
                
            };
            vertices.Add(a);
        }
    }

    private static LoadResult LoadObj(string path)
    {
        Console.WriteLine("LoadObj FullPath:" + Path.GetFullPath(path));
        
        var objLoaderFactory = new ObjLoaderFactory();
        return objLoaderFactory.Create().Load(new FileStream(path, FileMode.Open));
    }
}