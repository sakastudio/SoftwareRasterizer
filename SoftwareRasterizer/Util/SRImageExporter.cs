using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Loaders;
using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class SRImageExporter
{
    public static List<SRVertex> LoadVertex(string path)
    {
        var obj = LoadObj(path);

        var vertices = new List<SRVertex>();
        foreach (var group in obj.Groups)
        {
            foreach (var face in group.Faces)
            {
                var faceIndexList = new List<int>();
                for (var j = 0; j < face.Count; j++)
                {
                    var faceVertex = face[j];
                    faceIndexList.Add(faceVertex.VertexIndex);
                    var vertex = obj.Vertices[faceVertex.VertexIndex - 1]; //VertexIndexは1から始まるので-1する
                    
                    vertices.Add(new SRVertex()
                    {
                        VertexIndex = faceVertex.VertexIndex,
                        ModelPosition = new SRVector4
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