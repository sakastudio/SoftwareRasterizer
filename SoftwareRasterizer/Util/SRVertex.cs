using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public class SRVertex
{
    public SRVector3 Position;
    public int VertexIndex;
    
    public List<int> FaceIndexList = new List<int>();
}