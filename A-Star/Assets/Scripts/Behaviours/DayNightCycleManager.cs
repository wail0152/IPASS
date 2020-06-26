using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{

    public GameObject sun;
    public GameObject moon;
    public float timeRate;
    public bool timeCycleEnabled = true;

    //Updating the sun and moon position and rotation every frame if timeCycleEnabled is true
    private void Update()
    {
        if (timeCycleEnabled)
        {
            Cycle(sun, Vector3.right);
            Cycle(moon, -Vector3.right);
        }
    }

    //Rotating an object around the worl orign while looking at it
    private void Cycle(GameObject obj, Vector3 dir)
    {
        obj.transform.RotateAround(Vector3.zero, dir, timeRate * Time.deltaTime);
        obj.transform.LookAt(Vector3.zero);
    }
}
