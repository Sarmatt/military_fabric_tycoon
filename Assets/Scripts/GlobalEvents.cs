using Event.Mouse;
using System;


public static class GlobalEvents
{
    public static Action BuildingGridWasChanged;
    public static Action MainStatisticWasChanged;
    public static TouchLeftAndRightEvent TouchLeftAndRightEvent = new TouchLeftAndRightEvent();
    public static TouchUpAndDownEvent TouchUpAndDownEvent = new TouchUpAndDownEvent();
}