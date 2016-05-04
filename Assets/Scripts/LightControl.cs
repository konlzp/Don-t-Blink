using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LightControl : MonoBehaviour {
    
    public Slider slider;
    public int flashCount;
    public AudioClip lightOnClip;
    public AudioClip lightOffClip;
    public AudioClip momClip;
    public float decRate;
    public float stepTime = 1;
    public float rechargeDelay;
    public float rechargeRate;
    public int maxMomCount;
    public Light mainLight;
    public GameController gameController;

    private AudioSource audioSource;
    private float nextLightOn = 0;
    private float nextRecharge = 0;
    private Light flashLight;
    private bool lightOn = false;
    private Image sliderFill;
    private Color deadFlashlight = new Color(110.0f / 255, 90.0f / 255, 80.0f / 255);
    private int momCount = 0;
    private bool momCalled = false;

    // Use this for initialization
    void Start () {
        flashLight = GetComponent<Light>();
        flashLight.intensity = 0;
        audioSource = GetComponent<AudioSource>();
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
        audioSource.clip = lightOnClip;
        audioSource.Play();
        flashLight.intensity = flashCount - 2;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < flashCount; i ++)
        {
            flashLight.intensity = i;
            yield return new WaitForSeconds(0.05f);
            flashLight.intensity = 0;
            yield return new WaitForSeconds(0.08f);
        }
        flashLight.intensity = flashCount;
    }

    IEnumerator turnOffLight()
    {
        audioSource.clip = lightOffClip;
        audioSource.Play();
        flashLight.intensity = 2;
        yield return new WaitForSeconds(0.3f);
        for (int i = flashCount; i > 0; i --)
        {
            flashLight.intensity = i;
            yield return new WaitForSeconds(0.05f);
            flashLight.intensity = 0;
            yield return new WaitForSeconds(0.08f);
        }
        flashLight.intensity = 0;
        lightOn = false;
    }

    public bool IsLightOn {
        get { return lightOn; }
    }

    void FlashlightControl()
    {
        //Turning on or off light
        if (Input.GetMouseButton(0) && Time.time > nextLightOn)
        {
            if (slider.value != 0)
            {
                if (lightOn == false)
                {
                    StartCoroutine(turnOnLight());
                }
                else {
                    StartCoroutine(turnOffLight());
                }
                nextLightOn = Time.time + stepTime;
            }
        }

        //Change the value of the slider
        if (lightOn)
        {
            slider.value = Mathf.Max(Mathf.MoveTowards(slider.value, slider.value - 1.0f, decRate), 0);
            if (slider.value <= 0)
            {
                StartCoroutine(turnOffLight());
                lightOn = false;
            }
        }

        //Change the color of the slider and the flashlight
        float remainBattery = slider.value / 100;
        flashLight.color = Color.Lerp(Color.white, deadFlashlight, 1 - remainBattery);
        sliderFill.color = new Color(1, remainBattery, remainBattery, 1);
    }

    void RechargeFlashlight()
    {
        //Recharge the flashlight
        slider.value = Mathf.Min(slider.value + rechargeRate, 100f);
        nextRecharge = Time.time + rechargeDelay;
    }

    IEnumerator CallMom()
    {
        momCalled = true;
        yield return new WaitForSeconds(2f);
        mainLight.intensity = 1.5f;
        yield return new WaitForSeconds(5);
        mainLight.intensity = 0;
        momCalled = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!gameController.IsGameActive)
        {
            return;
        }

        FlashlightControl();

        if (!lightOn && Input.GetKeyDown(KeyCode.Space) && Time.time > nextRecharge)
        {
            RechargeFlashlight();
        }

        if (Input.GetMouseButton(1) && !momCalled)
        {
            audioSource.clip = momClip;
            audioSource.Play();
            if(momCount < maxMomCount)
            {
                StartCoroutine(CallMom());
                momCount += 1;
            }
        }
    }

    public void ImmediatelyTurnOnLight() 
    {
        lightOn = true;
        flashLight.intensity = 2.0F;
        Debug.Log ("Turned light ON");
    }

    public void ImmediatelyTurnOffLight() 
    {
        lightOn = false;
        flashLight.intensity = 0.0F;
        Debug.Log ("Turned light OFF");
    }
}
