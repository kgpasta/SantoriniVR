public enum Building
{
    NONE,
    ONE,
    TWO,
    THREE,
    ROOF
}

static class BuildingUtils
{
    public static Building NextBuilding(Building? building)
    {
        Building nextBuilding = Building.ONE;

        if (building == Building.ONE)
        {
            nextBuilding = Building.TWO;
        }
        else if (building == Building.TWO)
        {
            nextBuilding = Building.THREE;
        }
        else
        {
            nextBuilding = Building.ROOF;
        }

        return nextBuilding;
    }
}