using UnityEngine;

namespace Kernel.Extentions
{
    public static class NumberExtensions
    {
        public static bool InRange(this int number, int min, int max) => number >= min && number <= max;
        
        public static bool InRange(this float number, float min, float max) => number >= min && number <= max;
        
        public static float Clamp(this float number, float min, float max) => Mathf.Clamp(number, min, max);

        public static float Round(this float value) => Mathf.Round(value);
        public static Vector3 AsVector3(this float value) => new(value, value, value);
        public static Vector3 AsVector2(this float value) => new Vector2(value, value);
        public static Vector3 AsYVector3(this float value) => value.AsYVector2();
        public static Vector3 AsXVector3(this float value) => value.AsXVector2();
        public static Vector3 AsZVector3(this float value) => new(0, 0, value);
        public static Vector2 AsYVector2(this float value) => new(0, value);
        public static Vector2 AsXVector2(this float value) => new(value, 0);

    }
}