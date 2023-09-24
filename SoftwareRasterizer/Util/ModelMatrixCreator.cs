using SoftwareRasterizer.Struct;

namespace SoftwareRasterizer.Util;

/// <summary>
/// オブジェクトの座標、回転、大きさから4x4のモデル行列を作成する
/// </summary>
public class ModelMatrixCreator
{
    public static SRMatrix4x4 CreateMatrix(SRVector3 pos, SRVector3 radianRot, SRVector3 scale)
    {
        var posMatrix = new SRMatrix4x4(
            1,0,0,pos.X,
            0,1,0,pos.Y,
            0,0,1,pos.Z,
            0,0,0,1
        );

        //回転行列をどうやって表すのか
        // https://rikei-tawamure.com/entry/2019/11/04/184049
        var rotXMatrix = new SRMatrix4x4(
            1,0,0,0,
            0,cos(radianRot.X),-sin(radianRot.X),0,
            0,sin(radianRot.X),cos(radianRot.X),0,
            0,0,0,1
        );
        var rotYMatrix = new SRMatrix4x4(
            cos(radianRot.Y),0,sin(radianRot.Y),0,
            0,1,0,0,
            -sin(radianRot.Y),0,cos(radianRot.Y),0,
            0,0,0,1
        );
        var rotZMatrix = new SRMatrix4x4(
            cos(radianRot.Z),-sin(radianRot.Z),0,0,
            sin(radianRot.Z),cos(radianRot.Z),0,0,
            0,0,1,0,
            0,0,0,1
        );
        
        var rotMatrix = MatrixUtil.Multi(rotXMatrix, MatrixUtil.Multi(rotYMatrix, rotZMatrix));
        
        var scaleMatrix = new SRMatrix4x4(
            scale.X,0,0,0,
            0,scale.Y,0,0,
            0,0,scale.Z,0,
            0,0,0,1
        );

        return MatrixUtil.Multi(posMatrix, MatrixUtil.Multi(rotMatrix, scaleMatrix));

    }
    
    private static float sin(float radian)
    {
        return (float) Math.Sin(radian);
    }
    private static float cos(float radian)
    {
        return (float) Math.Cos(radian);
    }
    
}