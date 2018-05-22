public enum Building
{
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
            building = Building.TWO;
        }
        else if (building == Building.TWO)
        {
            building = Building.THREE;
        }
        else
        {
            building = Building.ROOF;
        }

        return nextBuilding;
    }
}