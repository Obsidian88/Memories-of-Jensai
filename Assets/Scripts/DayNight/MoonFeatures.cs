// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MoonFeatures : MonoBehaviour {
///
///   Designed and Programmed by
///      Juan Ignacio Tel  (juanignaciotel.tamaroqblog@gmail.com)
///       tamaroq.blogspot.com
///
///   Copyright (C) 2013
///   Free to use and distribute
///



public float phase;
public float moonDeclination;
public float sizeInRelationToMoon;
public Material material;
public Color lightTint = Color.white;
public float lightIntensity;

void  Start (){
	this.GetComponent<Renderer>().material = material;
	Light moonlight = this.GetComponentInChildren<Light>();
	moonlight.intensity = lightIntensity;
	moonlight.color = lightTint;
}

void  Update (){

}
}