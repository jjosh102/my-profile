
namespace MyProfile.Shared.Extensions;
public static class Utility
{
    public static void Check<T>(this T obj, bool condition,Action action)
    {
        if (condition)
        {
            action();
        }
        else
        {

        }
    }
}
