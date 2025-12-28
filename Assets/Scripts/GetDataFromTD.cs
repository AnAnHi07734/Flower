using System;
using OscJack;
using Unity.VisualScripting;
using UnityEngine;

public class GetDataFromTD : MonoBehaviour
{
    [SerializeField] bool h1_active = false;
    [SerializeField] Vector2 h1_positon = Vector2.zero;
    [SerializeField] bool h2_active = false;
    [SerializeField] Vector2 h2_positon = Vector2.zero;
    [SerializeField] GameObject bloom;
    BloomController bloomController;
    [SerializeField] GameObject hand1_sphere;
    [SerializeField] GameObject hand2_sphere;

    void Start()
    {
        bloomController = bloom.GetComponent<BloomController>();
    }

    void Update()
    {
        // if (h1_active) bloomController.set_interact_position(get_viewport_pos(h1_positon));

        if (h1_active)
        {
            hand1_sphere.SetActive(true);
            Vector2 v = get_viewport_pos(h1_positon);
            Vector3 target_position = Camera.main.ScreenToWorldPoint(new Vector3(v.x,v.y,10));
            hand1_sphere.transform.position = Vector3.Lerp(hand1_sphere.transform.position,target_position,0.35f);
            bloomController.set_interact_position(Camera.main.WorldToScreenPoint(hand1_sphere.transform.position));
        }
        else hand1_sphere.SetActive(false);

        // if (h2_active)
        // {
        //     hand2_sphere.SetActive(true);
        //     Vector2 v = get_viewport_pos(h2_positon);
        //     Vector3 target_position = Camera.main.ScreenToWorldPoint(new Vector3(v.x,v.y,10));
        //     hand2_sphere.transform.position = Vector3.Lerp(hand2_sphere.transform.position,target_position,0.35f);
        //     bloomController.set_interact_position(Camera.main.WorldToScreenPoint(hand2_sphere.transform.position));
        // }
        // else hand2_sphere.SetActive(false);
    }

    Vector2 get_viewport_pos(Vector2 position)
    {
        return new Vector2(
            position.x * 1.5f * Screen.width - 0.25f * Screen.width,
            position.y * 1.5f * Screen.height - 0.25f * Screen.height
        );
    }


    public void get_h1_active(float activation)
    {
        h1_active = Convert.ToBoolean(activation);
    }
    public void get_h2_active(float activation)
    {
        h2_active = Convert.ToBoolean(activation);
    }
    public void get_h1_x(float value)
    {
        h1_positon.x = value;
    }
    public void get_h1_y(float value)
    {
        h1_positon.y = value;
    }
    public void get_h2_x(float value)
    {
        h2_positon.x = value;
    }
    public void get_h2_y(float value)
    {
        h2_positon.y = value;
    }
}