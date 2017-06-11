using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SunCycle : MonoBehaviour {

	public Gradient nightDayColor;

	public float maxIntensity = 3f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;

	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;
	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;

    public Text TextTimeMultiplierStatus;
    private float SkySpeed = 1;
	public Transform StarConstellation;
    float exposure = 0.45f;

    private ParticleSystem Stars;

    // The number of real-world seconds in one full game day.
    // Set this to 86400 for a 24-hour realtime day.
    public float secondsInFullDay = 120f;

    // The value we use to calculate the current time of day.
    // Goes from 0 (midnight) through 0.25 (sunrise), 0.5 (midday), 0.75 (sunset) to 1 (midnight).
    // We define ourself what value the sunrise sunrise should be etc., but I thought these 
    // values fit well. And now much of the script are hardcoded to these values.
    [Range(0,1)]
    public float currentTimeOfDay = 0;
	
	public float RiseAndSetAtDegree = 170f;

	Light Sun;
	Skybox Sky;
	Material SkyMat;

	void Start () 
	{
		Sun = GetComponent<Light>();
		SkyMat = RenderSettings.skybox;
        TextTimeMultiplierStatus.text = "Timemultiplier: " + Mathf.Floor(SkySpeed) + " (press R/F)";
        exposure = RenderSettings.skybox.GetFloat("_Exposure");
        Stars = GameObject.Find("Stars").GetComponent<ParticleSystem>();
    }

	void Update () 
	{
		// This makes currentTimeOfDay go from 0 to 1 in the number of seconds we've specified.
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * SkySpeed;

        // If currentTimeOfDay == 1 (midnight), set it to 0 again so we start a new day.
        if (currentTimeOfDay >= 1) 
		{
            currentTimeOfDay = 0;
        }
		
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (Sun.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		Sun.intensity = i;

		tRange = 1 - minAmbientPoint;
		dot = Mathf.Clamp01 ((Vector3.Dot (Sun.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
		i = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = i;

		Sun.color = nightDayColor.Evaluate(dot);
		RenderSettings.ambientLight = Sun.color;

		RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

		i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
		SkyMat.SetFloat("_AtmosphereThickness", i);

        // if (dot > 0) 
        // transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
        // else
        // transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);
        //Debug.Log("Dot:" + dot);
        exposure = RenderSettings.skybox.GetFloat("_Exposure");

        if (dot > 0.5)
        {
            //ChangeStarOpacity("FadeOut");
            if (Stars.isStopped)
            {
                Stars.Stop();
            }
            if (RenderSettings.skybox.GetFloat("_Exposure") < 0.45f)
            {
                StartCoroutine("SkyboxExposureUp");
            }
            Sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f * dayRotateSpeed.x) - 90, RiseAndSetAtDegree, 0);
        }
        else
        {
            //ChangeStarOpacity("FadeIn");
            if(Stars.isStopped)
            { 
            Stars.Play();
            }
            if (RenderSettings.skybox.GetFloat("_Exposure") > 0.2f)
            {
                StartCoroutine("SkyboxExposureDown");
            }
            Sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f * nightRotateSpeed.x) - 90, RiseAndSetAtDegree, 0);
        }

        // Handling of time multiplier
        if (Input.GetKeyDown (KeyCode.R)) SkySpeed *= 2f;
		if (Input.GetKeyDown (KeyCode.F)) SkySpeed *= 0.5f;
        TextTimeMultiplierStatus.text = "Timemultiplier: " + Mathf.Floor(SkySpeed) + " (press R/F)";

        // Transform the star-objects (stars, sun and moon)
        //        Stars.playbackSpeed = SkySpeed;
       // Stars.main.simulationSpeed = 
        var main = Stars.main;
        main.simulationSpeed = SkySpeed;
        //Stars.simulationSpeed = SkySpeed;
        StarConstellation.transform.rotation = transform.rotation;
	}

    // not in use currently
    void ChangeStarOpacity(string InOrOut)
    {
        float alpha = 0f;
        if(InOrOut == "FadeIn")
        {
            alpha = 1f;
        }
        else
        {
            alpha = 0f;
        }
        ParticleSystem.Particle[] Particles = new ParticleSystem.Particle[Stars.particleCount];
        Stars.GetParticles(Particles);
        // Color32 c = Particles[1].GetCurrentColor(Stars); // doesnt work somehow .. need to check again, for now 124, 210, 204 is hardcoded

        var main = Stars.main;
        main.startColor = new Color(124, 210, 204, alpha);
    }

    IEnumerator SkyboxExposureUp()
    {
        yield return new WaitForSeconds(0.1f);
        exposure += 0.0001f;
        RenderSettings.skybox.SetFloat("_Exposure", exposure);
    }

    IEnumerator SkyboxExposureDown()
    {
        for (int interval = 0; interval < 10; interval++)
        {
            yield return new WaitForSeconds(0.1f);
            exposure -= 0.0001f;
            RenderSettings.skybox.SetFloat("_Exposure", exposure);
        }
    }
}
