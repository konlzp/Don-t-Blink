using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LightControl : MonoBehaviour {
    
    public Slider slider;
    public int flashCount;
    public float stepTime = 1;

    private float nextTime = 0;
    private Light torch;
    private bool lightOn = false;
	private bool batteryOut = false;

    // Use this for initialization
    void Start () {
        torch = GetComponent<Light>();
        torch.intensity = 0;
    }

    IEnumerator turnOnLight()
    {
        torch.intensity = flashCount - 2;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < flashCount; i ++)
        {
            torch.intensity = i;
            yield return new WaitForSeconds(0.05f);
            torch.intensity = 0;
            yield return new WaitForSeconds(0.08f);
        }
        torch.intensity = flashCount;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z) && Time.time > nextTime) {
			if (batteryOut == false) {
				if (lightOn == false) {
					StartCoroutine (turnOnLight ());
					lightOn = true;
				} else {
					torch.intensity = 0;
					lightOn = false;
				}
				nextTime = Time.time + stepTime;
			}
		}

        if(lightOn)
        {
            slider.value = Mathf.MoveTowards(slider.value, slider.value - 1.0f, 0.01f);
			if (slider.value <= 0) {
				batteryOut = true;
				torch.intensity = 0;
				lightOn = false;
			}
        }
	}
}
