using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.UI;

public class BloomController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Vector2 interact_position;

    [SerializeField] private float interact_radius;
    [SerializeField] private bool use_mouse_position;
    [SerializeField] private GameObject flower_root_obj;

    [SerializeField] private float no_interaction_time = 3f;
    private float interaction_timer;
    [SerializeField] private GameObject no_interaction_image;

    void Update()
    {
        if (use_mouse_position) set_interact_position(Input.mousePosition);
        if (interaction_timer > 0)
        {
            interaction_timer -= Time.deltaTime;
            interaction_timer = Math.Max(interaction_timer,0);

            CanvasGroup canvasGroup = no_interaction_image.GetComponent<CanvasGroup>();
            canvasGroup.alpha -= Time.deltaTime * 2f;
            
        }
        else
        {
            CanvasGroup canvasGroup = no_interaction_image.GetComponent<CanvasGroup>();
            canvasGroup.alpha += Time.deltaTime * 1f;
        }
    }

    public void set_interact_position(Vector2 position)
    {
        set_flower(flower_root_obj, position);
        interaction_timer = no_interaction_time;
    }

    bool is_flower(GameObject flower)
    {
        return flower.GetComponent<Flower>()? true : false;
    }

    void set_flower(GameObject flower, Vector2 _interact_pos)
    {
        
        if (is_flower(flower))
        set_flower_activate(flower, check_flower_in_radius(flower, _interact_pos));

        if (flower.transform.childCount == 0) return;

        for (int i = 0; i < flower.transform.childCount; i++)
        {
            set_flower(flower.transform.GetChild(i).gameObject, _interact_pos);
        }
    }


    bool check_flower_in_radius(GameObject flower, Vector2 _interact_pos)
    {
        bool flower_in_radius = false;
        Vector2 flower_pos = Camera.main.WorldToScreenPoint(flower.transform.position);
        float distance = (flower_pos - _interact_pos).magnitude;
        if (distance < interact_radius + flower.GetComponent<Flower>().radius)
        {
            flower_in_radius = true;
        }
        return flower_in_radius;
    }

    void set_flower_activate(GameObject flower, bool activation)
    {
        flower.GetComponent<Flower>().set_active(activation);
        // flower.GetComponent<Animator>().SetBool("bloom",activation);

    }

    [Serializable]
    public class FlowerEvent : UnityEvent{};
    public FlowerEvent OnBloom;
    public FlowerEvent OnBloomHalfway;
    public FlowerEvent OnWither;

}
