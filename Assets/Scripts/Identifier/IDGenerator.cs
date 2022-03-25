using System;

namespace DroneBase.Identifier
{
    public static class IDGenerator
    {
        public static int GetNewId (object obj)
        {
            return obj.GetHashCode();
        }
    }
}