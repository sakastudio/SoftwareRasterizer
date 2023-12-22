namespace SoftwareRasterizer.Struct;

/// <summary>
/// 3次元のベクトルを表す
/// SRはSoftwareRasterizerの略
/// </summary>
public struct SRVector3
{
    public float X;
    public float Y;
    public float Z;
    
    public SRVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public static SRVector3 operator +(SRVector3 v, SRVector3 w)
    {
        return new SRVector3(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
    }
    public static SRVector3 operator -(SRVector3 v, SRVector3 w)
    {
        return new SRVector3(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
    }

    public override string ToString()
    {
        return $"({X},{Y},{Z})";
    }

    public static float Dot(SRVector3 a, SRVector3 b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }

    public static SRVector3 Cross(SRVector3 a, SRVector3 b)
    {
        return new SRVector3(
            a.Y * b.Z - a.Z * b.Y,
            a.Z * b.X - a.X * b.Z,
            a.X * b.Y - a.Y * b.X);
    }

    public SRVector3 Normalize()
    {
        var length = (float) Math.Sqrt(X * X + Y * Y + Z * Z);
        return new SRVector3(X / length, Y / length, Z / length);
    }
}