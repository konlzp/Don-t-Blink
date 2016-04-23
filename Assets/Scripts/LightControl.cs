using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LightControl : MonoBehaviour {
    
    public Slider slider;
    public int flashCount;
    public AudioClip lightOnClip;
    public AudioClip lightOffClip;
    public float decRate;
    public float stepTime = 1;
    public float rechargeDelay;
    public float rechargeRate;

    private AudioSource lightAudio;
    private float nextLightOn = 0;
    private float nextRecharge = 0;
    private Light torch;
    private bool lightOn = false;
	private bool batteryOut = false;
    private Image sliderFill;
    private Color deadFlashlight = new Color(110.0f / 255, 90.0f / 255, 80.0f / 255);

    // Use this for initialization
    void Start () {
        torch = GetComponent<Light>();
        torch.intensity = 0;
        lightAudio = GetComponent<AudioSource>();
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
        lightOn = true;
        lightAudio.clip = lightOnClip;
        lightAudio.Play();
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
        lightAudio.clip = lightOffClip;
        lightAudio.Play();
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
        lightOn = false;
    }

    public bool getLightStatus()
    {
        return lightOn;
    }
	
	// Update is called once per frame
	void Update () {
        //Turning on or off light
        if (Input.GetKeyDown (KeyCode.Z) && Time.time > nextLightOn) {
			if (batteryOut == false) {
				if (lightOn == false) {
					StartCoroutine (turnOnLight ());
				} else {
                    StartCoroutine(turnOffLight());
				}
				nextLightOn = Time.time + stepTime;
			}
		}

        //Recharge the flashlight
        if(!lightOn && Input.GetKeyDown(KeyCode.Space) && Time.time > nextRecharge)
        {
            slider.value = Mathf.Min(slider.value + rechargeRate, 100f);
            nextRecharge = Time.time + rechargeDelay;
        }

        //Change the value of the slider
        if(lightOn)
        {
            slider.value = Mathf.MoveTowards(slider.value, slider.value - 1.0f, decRate);
			if (slider.value <= 0) {
				batteryOut = true;
                StartCoroutine(turnOffLight());
                lightOn = false;
			}
        }

        //Change the color of the slider and the flashlight
        float remainBattery = slider.value / 100;
        torch.color = Color.Lerp(Color.white, deadFlashlight, 1 - remainBattery);
        sliderFill.color = new Color(1, remainBattery, remainBattery, 1);
    }
}
