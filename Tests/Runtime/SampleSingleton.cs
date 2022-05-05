using UnityEngine;
using Cronus.Singleton;

[Singleton(Name = "Singleton UnitTest", Persistent = true)]
public class SampleSingleton : MonoBehaviour
{
    public static SampleSingleton Instance => Singleton<SampleSingleton>.Instance;
}
