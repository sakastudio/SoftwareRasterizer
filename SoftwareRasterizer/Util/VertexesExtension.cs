using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class VertexesExtension
{
    public static SRBoundingBox GetClippingBoundingBox(this List<int> faces,Dictionary<int,SRVertex> vertices)
    {
        var min = new SRVector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new SRVector3(float.MinValue, float.MinValue, float.MinValue);

        foreach (var face in faces)
        {
            var vertex = vertices[face];
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