using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Loaders;
using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class SRImageExporter
{
    public static List<SRVertex> LoadVertex(string path)
    {
        var result = LoadObj(path);

        var vertices = new List<SRVertex>();
        foreach (var group in result.Groups)
        {
            foreach (var face in group.Faces)
            {
                var faceIndexList = new List<int>();
                for (var j = 0; j < face.Count; j++)
                {
                    var faceVertex = face[j];
                    faceIndexList.Add(faceVertex.VertexIndex);
                    var vertex = result.Vertices[faceVertex.VertexIndex - 1];
                    
                    vertices.Add(new SRVertex()
                    {
                        VertexIndex = vertices.Count,
                        Position = new SRVector4
                        {
                            X = vertex.X,
                            Y = vertex.Y,
                            Z = vertex.Z,
                            W = 1
                        },
                        FaceIndex = faceIndexList
                    });
                }
            }
        }
        
        return vertices;
    }

    private static LoadResult LoadObj(string path)
    {
        Console.WriteLine("LoadObj FullPath:" + Path.GetFullPath(path));
        
        var objLoaderFactory = new ObjLoaderFactory();
        return objLoaderFactory.Create().Load(new FileStream(path, FileMode.Open));
    }
}