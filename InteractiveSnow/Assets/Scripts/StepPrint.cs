using UnityEngine;

public class StepPrint : MonoBehaviour
{
    public float printHeight = 0.3f;
    public float printInterval = 0.1f;
    public float drawHeight = 0.8f;
    public float drawMinDistance = 0.1f;
    public float drawMaxDistance = 0.2f;
    public Transform[] trailsTransforms;

    private int _index = 0;
    private float[] _lastPrintTimes;
    private readonly int _drawPosition = Shader.PropertyToID("_DrawPosition");
    private readonly int _drawHeight = Shader.PropertyToID("_DrawHeight");
    private readonly int _drawMinDistance = Shader.PropertyToID("_DrawMinDistance");
    private readonly int _drawMaxDistance = Shader.PropertyToID("_DrawMaxDistance");
    private readonly int _drawAngle = Shader.PropertyToID("_DrawAngle");

    private void Awake()
    {
        _lastPrintTimes = new float[trailsTransforms.Length];
    }

    public void DrawTrails(Material material)
    {
        if (Time.time - _lastPrintTimes[_index] > printInterval)
        {
            var trail = trailsTransforms[_index];
            var ray = new Ray(trail.position, Vector3.down);
            if (Physics.Raycast(ray, out var hit, printHeight, LayerMask.GetMask("Snow")))
            {
                material.SetVector(_drawPosition, hit.textureCoord);
                material.SetFloat(_drawHeight, drawHeight);
                material.SetFloat(_drawMinDistance, drawMinDistance);
                material.SetFloat(_drawMaxDistance, drawMaxDistance);
                material.SetFloat(_drawAngle, trail.rotation.eulerAngles.y * Mathf.Deg2Rad);

                // Update last print time
                _lastPrintTimes[_index] = Time.time;
            }
        }

        _index = (_index + 1) % trailsTransforms.Length;
    }
}