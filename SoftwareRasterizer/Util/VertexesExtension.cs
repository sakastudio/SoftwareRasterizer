using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class VertexesExtension
{
    public static List<List<SRVertex>> GetFaces(this List<SRVertex> vertices)
    {
        var faces = new List<List<SRVertex>>();
        
        foreach (var vertex in vertices)
        {
            var face = new List<SRVertex>();
            foreach (var faceIndex in vertex.FaceIndex)
            {
                face.Add(vertices.Find(v => v.VertexIndex == faceIndex));
            }
            faces.Add(face);
        }
        return faces;
    }
    
    public static SRBoundingBox GetClippingBoundingBox(this List<SRVertex> vertices)
    {
        var min = new SRVector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new SRVector3(float.MinValue, float.MinValue, float.MinValue);
            
        foreach (var vertex in vertices)
        {
            var clipping = vertex.ClipPosition;
            
            min.X = Math.Min(min.X, clipping.X);
            min.Y = Math.Min(min.Y, clipping.Y);
            min.Z = Math.Min(min.Z, clipping.Z);
            
            max.X = Math.Max(max.X, clipping.X);
            max.Y = Math.Max(max.Y, clipping.Y);
            max.Z = Math.Max(max.Z, clipping.Z);
        }
        
        return new SRBoundingBox(min, max);
    }
}