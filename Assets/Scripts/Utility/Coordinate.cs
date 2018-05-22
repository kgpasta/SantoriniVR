public class Coordinate
{
    public int x { get; set; }
    public int y { get; set; }

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }

    public override string ToString()
    {
        return string.Format("({0} , {1})", this.x, this.y);
    }
}