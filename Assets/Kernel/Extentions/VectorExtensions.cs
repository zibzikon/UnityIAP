using System.Collections.Generic;
using UnityEngine;

namespace Kernel.Extentions
{
    public static class VectorExtensions
    {
        public static int GetNearestVectorIndex(this IEnumerable<Vector2> vectors, Vector2 position, float maxDistance)
        {
            var nearestIndex = -1;

            var minDistance = maxDistance;

            var index = 0;
            foreach (var vector in vectors)
            {
                var distance = Vector2.Distance(vector, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestIndex = index;
                }

                index++;
            }

            return nearestIndex;
        }
        
        
        
        public static Vector2 SetY(this Vector2 vector, float value) => new (vector.x, value);
        public static Vector2 SetX(this Vector2 vector, float value) => new (value, vector.y);

        public static Vector2 AddX(this Vector2 vector, float value) => vector.SetX(vector.x + value);
        public static Vector2 SubtractX(this Vector2 vector, float value) => vector.SetX(vector.x - value);
        public static Vector2 MultiplyX(this Vector2 vector, float value) => vector.SetX(vector.x * value);
        public static Vector2 DivideX(this Vector2 vector, float value) => vector.SetX(vector.x / value);
        
        public static Vector2 AddY(this Vector2 vector, float value) => vector.SetY(vector.y + value);
        public static Vector2 SubtractY(this Vector2 vector, float value) => vector.SetY(vector.y - value);
        public static Vector2 MultiplyY(this Vector2 vector, float value) => vector.SetY(vector.y * value);
        public static Vector2 DivideY(this Vector2 vector, float value) => vector.SetY(vector.y / value);
        
        
        
        public static Vector3 SetY(this Vector3 vector, float value) => new (vector.x, value, vector.z);
        public static Vector3 SetX(this Vector3 vector, float value) => new (value, vector.y, vector.z);
        public static Vector3 SetZ(this Vector3 vector, float value) => new (vector.x, vector.y, value);
        
        public static Vector3 AddX(this Vector3 vector, float value) => vector.SetX(vector.x + value);
        public static Vector3 SubtractX(this Vector3 vector, float value) => vector.SetX(vector.x - value);
        public static Vector3 MultiplyX(this Vector3 vector, float value) => vector.SetX(vector.x * value);
        public static Vector3 DivideX(this Vector3 vector, float value) => vector.SetX(vector.x / value); 
        
        public static Vector3 AddY(this Vector3 vector, float value) => vector.SetY(vector.y + value);
        public static Vector3 SubtractY(this Vector3 vector, float value) => vector.SetY(vector.y - value);
        public static Vector3 MultiplyY(this Vector3 vector, float value) => vector.SetY(vector.y * value);
        public static Vector3 DivideY(this Vector3 vector, float value) => vector.SetY(vector.y / value);      
        
        public static Vector3 AddZ(this Vector3 vector, float value) => vector.SetZ(vector.z + value);
        public static Vector3 SubtractZ(this Vector3 vector, float value) => vector.SetZ(vector.z - value);
        public static Vector3 MultiplyZ(this Vector3 vector, float value) => vector.SetZ(vector.z * value);
        public static Vector3 DivideZ(this Vector3 vector, float value) => vector.SetZ(vector.z / value);

    }
}