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
}