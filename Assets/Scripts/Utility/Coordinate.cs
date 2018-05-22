using System;

public class Coordinate
{
    public int x { get; set; }
    public int y { get; set; }

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        Coordinate coordinate = obj as Coordinate;

        return coordinate.x == this.x && coordinate.y == this.y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }

    public override string ToString()
    {
        return string.Format("({0} , {1})", this.x, this.y);
    }

    public bool IsAdjacent(Coordinate coord)
    {
        return Math.Abs(this.x - coord.x) + Math.Abs(this.y - coord.y) == 1;
    }
}