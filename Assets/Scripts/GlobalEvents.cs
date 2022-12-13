using Event.Mouse;
using System;


public static class GlobalEvents
{
    public static Action BuildingGridWasChanged;
    public static Action MainStatisticWasChanged;
    public static MouseLeftAndRightEvent MouseLeftAndRightEvent = new MouseLeftAndRightEvent();
    public static MouseUpAndDownEvent MouseUpAndDownEvent = new MouseUpAndDownEvent();
}