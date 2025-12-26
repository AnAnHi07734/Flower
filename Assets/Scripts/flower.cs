using System.Security.Claims;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public string flower_name = "";
    public float radius = 10;
    public AnimationClip flower_animation;
    public float bloom_speed;
    public float wither_speed;
    public float bloom_duration = 1.5f;
    bool is_bloom = false;
    bool blooming = false;
    float bloom_stag;
    float animation_time = 0;

    void Start()
    {
        animation_time = flower_animation.length;
        bloom_stag = 0;

        // transform.Rotate(0,Random.Range(0f,360f),0);
    }

    void Update()
    {
        if (!blooming && is_bloom){
            blooming = true;
        }

        if (is_bloom) bloom_stag = bloom_duration;
        else
        {
            bloom_stag -= Time.deltaTime;
            bloom_stag = Mathf.Max(bloom_stag,0);
        }

        if (blooming) bloom();
        else wither();
        
    }

    public void set_active(bool activation)
    {
        is_bloom = activation;
    }

    void bloom()
    {
        animation_time -= bloom_speed * Time.deltaTime;
        animation_time = Mathf.Clamp(animation_time,0,flower_animation.length);
        flower_animation.SampleAnimation(gameObject,animation_time);
        if (bloom_stag == 0 && animation_time <= 0)
        {
            blooming = false;
        }

    }
    void wither()
    {
        animation_time += wither_speed * Time.deltaTime;
        animation_time = Mathf.Clamp(animation_time,0,flower_animation.length);
        flower_animation.SampleAnimation(gameObject,animation_time);
    }
}