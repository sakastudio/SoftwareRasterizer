using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public class SRVertex
{
    public SRVector4 ModelPosition;
    public SRVector4 WorldPosition;
    public SRVector4 ViewPosition;
    public SRVector4 ClipPosition;
    public SRVector2Int ScreenPos; 
        
    public int VertexIndex;
    public List<int> FaceIndex;
}