using System.Collections.Generic;

public static class ActionManager
{
    public static ActionType[] GetAvailableActions(int currentColumn, int totalColumns) 
    {
        List<ActionType> availableActions = new List<ActionType>();

        if (currentColumn > 0)
            availableActions.Add(ActionType.Left);

        if (currentColumn < totalColumns - 1)
            availableActions.Add(ActionType.Right);

        availableActions.Add(ActionType.Stay);

        return availableActions.ToArray();
    }
}