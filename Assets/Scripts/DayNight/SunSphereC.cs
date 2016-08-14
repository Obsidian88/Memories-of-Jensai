using UnityEngine;
using System.Collections;

public class SunSphereC : MonoBehaviour
{
    ///
    ///   Designed and Programmed by
    ///      Juan Ignacio Tel  (juanignaciotel.tamaroqblog@gmail.com)
    ///       tamaroq.blogspot.com
    ///
    ///   Copyright (C) 2013
    ///   Free to use and distribute
    ///



    public float angleNorthDegrees = 0.0f; // 0 to 360 degrees
    public float declinationDegrees = 60.0f; // 0 to 90 degrees
    public float winterVsSummer = 0.0f; // -1 to +1
    public float moonPhase = 135.0f;
    public float maximumSunIntensity = 1.0f;
    public float maximumMoonIntensity = 0.5f;
    private float sunIntensity = 0.0f;
    private float moonIntensity = 0.0f;
    public int hourTheSunIsAtTheTop = 14; //usually 13 or 14

    private GameObject sun;
    private Light sunLight;
    private GameObject moon;
    private GameObject moonBody;
    private Light moonLight;
    private GameObject sunTarget;

    private GameTime gameTime;

    private int lastSphereRenderedHour;
    private int lastSphereRenderedMinute;
    private int lastSphereRenderedSecond;

    void Start()
    {
        gameTime = GameTime.Instance();

        sun = GameObject.Find("Sun");
        sunLight = sun.GetComponent<Light>();
        moon = GameObject.Find("MoonLight");
        moonLight = moon.GetComponent<Light>();
        moonBody = GameObject.Find("MoonBody");
        sunTarget = GameObject.Find("SunTarget");
        moonBody.transform.RotateAround(sunTarget.transform.position, Vector3.right, moonPhase);
        moonBody.transform.RotateAround(sunTarget.transform.position, Vector3.forward, 6);
        maximumMoonIntensity = maximumMoonIntensity * (1 - Mathf.Abs((180 - moonPhase) / 180));
        transform.rotation = Quaternion.identity;
        transform.position = sunTarget.transform.position + Vector3.up * winterVsSummer * transform.localScale.magnitude * 0.5f;
        transform.Rotate(Vector3.up * angleNorthDegrees);
        transform.Rotate(Vector3.forward * declinationDegrees);

        lastSphereRenderedHour = hourTheSunIsAtTheTop;
        lastSphereRenderedMinute = 0;
        lastSphereRenderedSecond = 0;
    }

    void Update()
    {
        sunIntensity = (Vector3.Project(sun.transform.position.normalized, Vector3.up) * maximumSunIntensity).y;
        moonIntensity = (Vector3.Project(moonBody.transform.position.normalized, Vector3.up) * maximumMoonIntensity).y;
        sunLight.intensity = sunIntensity;
        moonLight.intensity = moonIntensity;

        MoveSun();
    }

    void MoveSun()
    {
        int hour = gameTime.GameHour;
        int minute = gameTime.GameMinute;
        int second = gameTime.GameSecond;
        RotationSunSphere(hour, minute, second, lastSphereRenderedHour, lastSphereRenderedMinute, lastSphereRenderedSecond);
        lastSphereRenderedHour = hour;
        lastSphereRenderedMinute = minute;
        lastSphereRenderedSecond = second;
    }

    void RotationSunSphere(int hour, int minute, int second, int previousHour, int previousMinute, int previousSecond)
    {
        float timeAdvanced = ((hour - previousHour) * 0.04166667f + (minute - previousMinute) * 0.0006944f + (second - previousSecond) * 0.0000115f);
        if (timeAdvanced < 0) { timeAdvanced += 1; }
        RotateSunSphere(timeAdvanced);
    }

    void RotateSunSphere(float t)
    {
        transform.Rotate(Vector3.right * t * 360.0f);
    }


}