using UnityEngine;

public class GetNormalisedLatLang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left 0");

            Vector3 mousePos = Input.mousePosition;
            Debug.Log("X mousePosition : " + mousePos.x);
            Debug.Log("Y mousePosition : " + mousePos.y);


            // Convert to a unit vector so our y coordinate is in the range -1...1.
            mousePos.Normalize();

            // The vertical coordinate (y) varies as the sine of latitude, not the cosine.
            float lat = Mathf.Asin(mousePos.y) * Mathf.Rad2Deg;

            // Use the 2-argument arctangent, which will correctly handle all four quadrants.
            float lon = Mathf.Atan2(mousePos.x, mousePos.z) * Mathf.Rad2Deg;

            // Here I'm assuming (0, 0, 1) = 0 degrees longitude, and (1, 0, 0) = +90.
            // You can exchange/negate the components to get a different longitude convention.

            Debug.Log(lat + ", " + lon);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right 1");
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Middle 2");
        }
    }
}