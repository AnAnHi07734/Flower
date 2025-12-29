using UnityEngine;

public class MPBColor : MonoBehaviour
{
    Renderer rend;
    MaterialPropertyBlock mpb;
    public Color color = Color.white;
    [Header("Random Color")]
    [Space(5)]
    public bool random_activate = true;
    [Range(0f,1f)]
    public float max_hue = 1f;
    [Range(0f,1f)]
    public float max_saturation = 0.2f;
    [Range(0f,1f)]
    public float min_value = 0f;
    GameObject flower_model;

    void Awake()
    {
        flower_model = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        rend = flower_model.GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    void Start()
    {
        rend.GetPropertyBlock(mpb); // 讀取現有設定（保險）
        if (random_activate) mpb.SetColor("_Color", Random.ColorHSV(
            0f, max_hue,
            0f, max_saturation,
            min_value, 1f
        ));
        else mpb.SetColor("_Color", color); // Standard 用 "_Color"
        rend.SetPropertyBlock(mpb);
    }
}