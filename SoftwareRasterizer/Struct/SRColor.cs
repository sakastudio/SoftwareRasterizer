namespace SoftwareRasterizer.Struct;


/// <summary>
/// SRはSoftwareRasterizerの略
/// .netのColorと混ざるため命名を分けてる
/// </summary>
public struct SRColor
{
    public byte R;
    public byte G;
    public byte B;
    
    public SRColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }
}