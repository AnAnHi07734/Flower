using System.Security.Claims;
using UnityEngine;
using UnityEngine.Events;

public class Flower : MonoBehaviour
{
    public GameObject bloomController;
    BloomController bloomCtrl;
    public string flower_name = "";
    public float radius = 10;
    public AnimationClip flower_animation;
    public float bloom_speed;
    public float wither_speed;
    public float bloom_duration = 1.5f;
    bool is_bloom = false;
    bool is_wither = true;
    bool blooming = false;
    float bloom_stag;
    float animation_time = 0;

    void Start()
    {
        bloomCtrl = bloomController.GetComponent<BloomController>();

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
        if (!is_bloom && activation)
        {
            is_wither = false;
            if (animation_time > flower_animation.length / 3 * 2) bloomCtrl.OnBloom.Invoke();
            else if (animation_time > flower_animation.length / 3) bloomCtrl.OnBloomHalfway.Invoke();
        }

        is_bloom = activation;
    }

    void bloom()
    {
        animation_time -= bloom_speed * Time.deltaTime;
        animation_time = Mathf.Clamp(animation_time,0,flower_animation.length);
        flower_animation.SampleAnimation(gameObject,animation_time);
        if (animation_time <= 0)
        {
            is_bloom = false;
        }
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

        if (!is_wither)
        {
            if (animation_time > flower_animation.length / 3 * 2)
            {
                bloomCtrl.OnWither.Invoke();
                is_wither = true;
            }
        }
    }
}