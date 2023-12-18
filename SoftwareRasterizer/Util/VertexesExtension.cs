using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class VertexesExtension
{
    public static List<List<SRVertex>> GetFaces(this List<SRVertex> vertices)
    {
        var faces = new List<List<SRVertex>>();
        var addedIndex = new List<int>();
        
        foreach (var vertex in vertices)
        {
            if (addedIndex.Contains(vertex.VertexIndex))
            {
                continue;
            }
            addedIndex.AddRange(vertex.FaceIndex);
            var face = vertex.FaceIndex.Select(faceIndex => vertices[faceIndex - 1]).ToList();
            faces.Add(face);
        }
        return faces;
    }
    
    public static SRBoundingBox GetBoundingBox(this List<SRVertex> vertices)
    {
        var minX = float.MaxValue;
        var minY = float.MaxValue;
        var minZ = float.MaxValue;
        var maxX = float.MinValue;
        var maxY = float.MinValue;
        var maxZ = float.MinValue;
        
        foreach (var vertex in vertices)
        {
            var position = vertex.Position;
            if (position.X < minX)
            {
                minX = position.X;
            }
            if (position.Y < minY)
            {
                minY = position.Y;
            }
            if (position.Z < minZ)
            {
                minZ = position.Z;
            }
            if (position.X > maxX)
            {
                maxX = position.X;
            }
            if (position.Y > maxY)
            {
                maxY = position.Y;
            }
            if (position.Z > maxZ)
            {
                maxZ = position.Z;
            }
        }
        
        return new SRBoundingBox(new SRVector3(minX, minY, minZ), new SRVector3(maxX, maxY, maxZ));
    }
}