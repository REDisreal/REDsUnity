using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class HamSwap : MonoBehaviour
{
    public static void Swap<T> (ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }
}
