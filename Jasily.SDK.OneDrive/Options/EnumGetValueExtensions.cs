using System;

namespace Jasily.SDK.OneDrive.Options
{
    public static class EnumGetValueExtensions
    {
        public static string GetValue(this ConflictBehavior conflictBehavior)
        {
            switch (conflictBehavior)
            {
                case ConflictBehavior.Fail:
                    return "fail";

                case ConflictBehavior.Rename:
                    return "rename";

                case ConflictBehavior.Replace:
                    return "replace";

                default:
                    throw new ArgumentOutOfRangeException(nameof(conflictBehavior), conflictBehavior, null);
            }
        }
    }
}