using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wheel : MonoBehaviour
{
    public GameObject ground;
    public Slider slider;
    public TextMeshProUGUI distanceText;

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.CompareTag("Floor"))
            {
                slider.value = hit.distance;
                distanceText.text = "- " + Mathf.Ceil(hit.distance).ToString() + "m";
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            PlaneController.instance.walkToFloor = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            PlaneController.instance.walkToFloor = true;
            ground.tag = "Floor";
        }
    }
}