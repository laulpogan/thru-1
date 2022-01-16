namespace Thru
{
    public enum State
    {
        Loading,
        Quit,
        Start,
        Menu,
        NewGame,
        Game,
        Final,
        PlayerDesign,
        MainSettings,
        Options,
        CharacterCreation
    }


    public enum GameState
    {
        Play,
        Pause,
        Encounter,
        Map,
        Inventory,
        Dialogue,
        Cutscene,
        Settings,
        Road,
        Town,
        Trailhead,
    };

    public enum InventoryState
    {
        Mouse,
        FreeSpace,
        Equipment,
        InventoryBoard
    }
}