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

public struct SRVec2BoundingBoxInt
{
    public SRVector2Int Min;
    public SRVector2Int Max;
    
    public SRVec2BoundingBoxInt(SRVector2Int min, SRVector2Int max)
    {
        Min = min;
        Max = max;
    }
}