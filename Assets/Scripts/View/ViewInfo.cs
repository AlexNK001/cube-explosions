public struct ViewInfo
{
    public int NumberObjectAppearances;
    public int NumberCreatedObjects;
    public int NumberActiveObjects;

    public ViewInfo(int numberObjectAppearances, int numberCreatedObjects, int numberActiveObjects)
    {
        NumberObjectAppearances = numberObjectAppearances;
        NumberCreatedObjects = numberCreatedObjects;
        NumberActiveObjects = numberActiveObjects;
    }
}
