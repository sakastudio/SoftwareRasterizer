namespace SoftwareRasterizer.Struct;

public struct SRVector4
{
    public float X;
    public float Y;
    public float Z;
    public float W;
    
    public SRVector4(float x, float y, float z,float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }
    public SRVector4(SRVector3 v,float w)
    {
        X = v.X;
        Y = v.Y;
        Z = v.Z;
        W = w;
    }

    public override string ToString()
    {
        return $"({X},{Y},{Z},{W})";
    }
}