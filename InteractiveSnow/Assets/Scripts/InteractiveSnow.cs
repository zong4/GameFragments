using UnityEngine;
using UnityEngine.Rendering;

public class InteractiveSnow : MonoBehaviour
{
    public Material snowMaterial;
    public Material heightMapMaterial;
    public StepPrint[] stepPrints;
    private CustomRenderTexture _snowHeightMap;
    private readonly int _heightMap = Shader.PropertyToID("_HeightMap");

    // CRT only can process one step print per frame, so we need to keep track of the index
    private int _index = 0;

    private void Awake()
    {
        // Initialize material
        var material = new Material(snowMaterial);
        heightMapMaterial.SetVector("_DrawPosition", new Vector4(-1, -1, 0, 0)); // Don't draw at the beginning
        
        // Initialize terrain
        var terrain = gameObject.GetComponent<Terrain>();
        _snowHeightMap = CreateHeightMap(512, 512, heightMapMaterial);
        terrain.materialTemplate = material;
        terrain.materialTemplate.SetTexture(_heightMap, _snowHeightMap);
        _snowHeightMap.Initialize();
    }

    private void Update()
    {
        if (stepPrints.Length == 0) return;
        
        stepPrints[_index].DrawTrails(heightMapMaterial);
        _snowHeightMap.Update();
        _index = (_index + 1) % stepPrints.Length;
    }

    private static CustomRenderTexture CreateHeightMap(int weight, int height, Material material)
    {
        var texture = new CustomRenderTexture(weight, height)
        {
            dimension = TextureDimension.Tex2D,
            format = RenderTextureFormat.R8,
            material = material,
            updateMode = CustomRenderTextureUpdateMode.Realtime,
            doubleBuffered = true
        };

        // Initialize to white
        var activeRT = RenderTexture.active;
        RenderTexture.active = texture;
        GL.Clear(true, true, Color.white);
        RenderTexture.active = activeRT;

        return texture;
    }
}