using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class BloomController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector2 interact_position;
    [SerializeField] private float interact_radius;
    [SerializeField] private bool use_mouse_position;
    [SerializeField] private GameObject flower_root_obj;


    void Update()
    {
        if (use_mouse_position) interact_position = Input.mousePosition;
        
        set_flower(flower_root_obj);

    }

    bool is_flower(GameObject flower)
    {
        return flower.GetComponent<Flower>()? true : false;
    }

    void set_flower(GameObject flower)
    {
        
        if (is_flower(flower))
        set_flower_activate(flower, check_flower_in_radius(flower));

        if (flower.transform.childCount == 0) return;

        for (int i = 0; i < flower.transform.childCount; i++)
        {
            set_flower(flower.transform.GetChild(i).gameObject);
        }
    }


    bool check_flower_in_radius(GameObject flower)
    {
        bool flower_in_radius = false;
        Vector2 flower_pos = Camera.main.WorldToScreenPoint(flower.transform.position);
        float distance = (flower_pos - interact_position).magnitude;
        if (distance < interact_radius + flower.GetComponent<Flower>().radius)
        {
            flower_in_radius = true;
            print(flower_pos);
        }
        return flower_in_radius;
    }

    void set_flower_activate(GameObject flower, bool activation)
    {
        flower.GetComponent<Flower>().set_active(activation);
        // flower.GetComponent<Animator>().SetBool("bloom",activation);

    }
}
