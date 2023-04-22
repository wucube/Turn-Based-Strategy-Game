using System;

/// <summary>
/// 格子位置
/// </summary>
public struct GridPosition : IEquatable<GridPosition>
{
    /// <summary>
    /// 格子的水平索引
    /// </summary>
    public int x;
    /// <summary>
    /// 格子的垂直索引
    /// </summary>
    public int z;
    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

#region 重写 Equals 和 GetHashCode。重载 == != 后 IDE 的修复提示
    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }
#endregion

    /// <summary>
    /// 返回格子位置索引的字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"x: {x}; z: {z}";
    }

    /// <summary>
    /// == 重载，比较格子位置结构体
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(GridPosition a, GridPosition b) => a.x == b.x && a.z == b.z;
    
    /// <summary>
    /// != 重载，比较格子位置结构体
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(GridPosition a, GridPosition b) => !( a == b );

    public static GridPosition operator +(GridPosition a, GridPosition b) => new GridPosition(a.x + b.x, a.z + b.z);
    public static GridPosition operator -(GridPosition a, GridPosition b) => new GridPosition(a.x - b.x, a.z - b.z);
}
