using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Loaders;
using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class SRImageExporter
{
    public static (Dictionary<int, SRVertex> vertices, List<List<int>> faces) LoadVertex(string path)
    {
        var obj = LoadObj(path);

        var vertices = new Dictionary<int,SRVertex>();

        for (var i = 0; i < obj.Vertices.Count; i++)
        {
            var vertex = obj.Vertices[i];
            vertices.Add(i+1, new SRVertex()
            {
                VertexIndex = i+1,
                ModelPosition = new SRVector4
                {
                    X = vertex.X,
                    Y = vertex.Y,
                    Z = vertex.Z,
                    W = 1
                },
            });
        }
        
        var faces = new List<List<int>>();

        foreach (var group in obj.Groups)
        {
            foreach (var face in group.Faces)
            {
                var faceIndexList = new List<int>();
                for (var j = 0; j < face.Count; j++)
                {
                    faceIndexList.Add(face[j].VertexIndex);
                }
                faces.Add(faceIndexList);
            }
        }
        
        return (vertices,faces);
    }

    private static LoadResult LoadObj(string path)
    {
        Console.WriteLine("LoadObj FullPath:" + Path.GetFullPath(path));
        
        var objLoaderFactory = new ObjLoaderFactory();
        return objLoaderFactory.Create().Load(new FileStream(path, FileMode.Open));
    }
}