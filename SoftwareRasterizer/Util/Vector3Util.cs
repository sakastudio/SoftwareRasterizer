using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public class Vector3Util
{
    public static SRVector3 Normalize(SRVector3 v)
    {
        var length = MathF.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        return new SRVector3(v.X / length, v.Y / length, v.Z / length);
    }

    public static float Dot(SRVector3 v, SRVector3 w)
    {
        return v.X * w.X + v.Y * w.Y + v.Z * w.Z;
    }

    public static SRVector3 Cross(SRVector3 v, SRVector3 w)
    {
        return new SRVector3(
           v.Y * w.Z - w.Y * v.Z, 
           -v.X * w.Z + w.X * v.Z,
              v.X * w.Y - w.X * v.Y
        );
    }
}