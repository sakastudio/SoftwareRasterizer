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
}