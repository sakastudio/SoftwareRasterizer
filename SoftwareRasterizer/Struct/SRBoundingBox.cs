namespace SoftwareRasterizer.Struct;

public struct SRBoundingBox
{
    public SRVector3 Min;
    public SRVector3 Max;
    
    public SRBoundingBox(SRVector3 min, SRVector3 max)
    {
        Min = min;
        Max = max;
    }
}