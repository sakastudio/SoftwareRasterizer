using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

public static class MatrixUtil
{
    public static SRMatrix4x4 Multi(SRMatrix4x4 a, SRMatrix4x4 b)
    {
        return new SRMatrix4x4(
            a.M00 * b.M00 + a.M01 * b.M10 + a.M02 * b.M20 + a.M03 * b.M30, // Row 0, Column 0
            a.M00 * b.M01 + a.M01 * b.M11 + a.M02 * b.M21 + a.M03 * b.M31, // Row 0, Column 1
            a.M00 * b.M02 + a.M01 * b.M12 + a.M02 * b.M22 + a.M03 * b.M32, // Row 0, Column 2
            a.M00 * b.M03 + a.M01 * b.M13 + a.M02 * b.M23 + a.M03 * b.M33, // Row 0, Column 3

            a.M10 * b.M00 + a.M11 * b.M10 + a.M12 * b.M20 + a.M13 * b.M30, // Row 1, Column 0
            a.M10 * b.M01 + a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31, // Row 1, Column 1
            a.M10 * b.M02 + a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32, // Row 1, Column 2
            a.M10 * b.M03 + a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33, // Row 1, Column 3

            a.M20 * b.M00 + a.M21 * b.M10 + a.M22 * b.M20 + a.M23 * b.M30, // Row 2, Column 0
            a.M20 * b.M01 + a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31, // Row 2, Column 1
            a.M20 * b.M02 + a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32, // Row 2, Column 2
            a.M20 * b.M03 + a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33, // Row 2, Column 3

            a.M30 * b.M00 + a.M31 * b.M10 + a.M32 * b.M20 + a.M33 * b.M30, // Row 3, Column 0
            a.M30 * b.M01 + a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31, // Row 3, Column 1
            a.M30 * b.M02 + a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32, // Row 3, Column 2
            a.M30 * b.M03 + a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33  // Row 3, Column 3
        );
    }

}