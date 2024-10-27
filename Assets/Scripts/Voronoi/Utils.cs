using Unity.Mathematics;

namespace Voronoi
{
    public static class Utils
    {
        // math.cross isn't consistent with Vector3.Cross
        public static float3 Cross(float3 lhs, float3 rhs)
        {
            return new float3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }
        
        // math.normalize isn't consistent with Vector3.Normalize
        public static float3 Normalize(this float3 value)
        {
            float magnitude = math.length(value);
            
            return magnitude == 0 ? float3.zero : value / magnitude;
        }
        
        public static float3 ProjectAndTranslate(float3 value, float3 forward, float3 up, float3 origin)
        {
            // math.quaternion.LookRotation isn't consistent with UnityEngine.Quaternion.LookRotation
            float3 t = Cross(up, forward).Normalize();
            
            var rotation = new quaternion(new float3x3(t, Cross(forward, t), forward));
            
            value = RotateFloat3(rotation, value);
            
            return value + origin;
        }

        // Taken from UnityEngine.Quaternion.operator*
        private static float3 RotateFloat3(quaternion rotation, float3 point)
        {
            float num1 = rotation.value.x * 2f;
            float num2 = rotation.value.y * 2f;
            float num3 = rotation.value.z * 2f;
            float num4 = rotation.value.x * num1;
            float num5 = rotation.value.y * num2;
            float num6 = rotation.value.z * num3;
            float num7 = rotation.value.x * num2;
            float num8 = rotation.value.x * num3;
            float num9 = rotation.value.y * num3;
            float num10 = rotation.value.w * num1;
            float num11 = rotation.value.w * num2;
            float num12 = rotation.value.w * num3;
            float3 rotatedPoint;
            rotatedPoint.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            rotatedPoint.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            rotatedPoint.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
            return rotatedPoint;
        }
    }
}