using UnityEngine;

[CreateAssetMenu(menuName = "Data/Camera/Shake")]
public class CameraShakeData : ScriptableObject
{
    public float Amplitude { get => amplitude; }
    public float Frequency { get => frequency; }
    public float ShakeTime { get => shakeTime; }
    public float InTime { get => inTime; }
    public float OutTime { get => outTime; }

    [SerializeField]
    private float amplitude = 0.0f;
    [SerializeField]
    private float frequency = 0.0f;
    [SerializeField]
    private float shakeTime = 0.0f;
    [SerializeField]
    private float inTime = 0.0f;
    [SerializeField]
    private float outTime = 0.0f;
}
