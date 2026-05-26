using System.Diagnostics;

namespace Application.Extensions;

public static class Trace
{
    public static Activity? StartActivity(string name)
    {
        var activity = Activity.Current?.Source.StartActivity($"{name}");
        return activity;
    }
}