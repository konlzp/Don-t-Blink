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
    private Image sliderFill;
    private Color deadFlashlight = new Color(110.0f / 255, 90.0f / 255, 80.0f / 255);

    // Use this for initialization
    void Start () {
        torch = GetComponent<Light>();
        torch.intensity = 0;
        for(int obj = 0; obj < slider.transform.childCount; obj ++)
        {
            var tempObj = slider.transform.GetChild(obj).gameObject;
            if(tempObj.name == "Fill Area")
            {
                tempObj = tempObj.transform.GetChild(0).gameObject;
                sliderFill = tempObj.GetComponent<Image>();
            }
        }
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

    IEnumerator turnOffLight()
    {
        torch.intensity = 2;
        yield return new WaitForSeconds(0.3f);
        for (int i = flashCount; i > 0; i --)
        {
            torch.intensity = i;
            yield return new WaitForSeconds(0.05f);
            torch.intensity = 0;
            yield return new WaitForSeconds(0.08f);
        }
        torch.intensity = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z) && Time.time > nextTime) {
			if (batteryOut == false) {
				if (lightOn == false) {
					StartCoroutine (turnOnLight ());
					lightOn = true;
				} else {
                    StartCoroutine(turnOffLight());
					lightOn = false;
				}
				nextTime = Time.time + stepTime;
			}
		}

        if(lightOn)
        {
            slider.value = Mathf.MoveTowards(slider.value, slider.value - 1.0f, 0.2f);
            float remainBattery = slider.value / 100;
			if (slider.value <= 0) {
				batteryOut = true;
                StartCoroutine(turnOffLight());
                lightOn = false;
			}
            torch.color = Color.Lerp(Color.white, deadFlashlight, 1 - remainBattery);
            sliderFill.color = new Color(1, remainBattery, remainBattery, 1);
        }
	}
}
