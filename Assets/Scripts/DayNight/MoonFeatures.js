///
///   Designed and Programmed by
///      Juan Ignacio Tel  (juanignaciotel.tamaroqblog@gmail.com)
///       tamaroq.blogspot.com
///
///   Copyright (C) 2013
///   Free to use and distribute
///

#pragma strict

public var phase : float;
public var moonDeclination : float;
public var sizeInRelationToMoon : float;
public var material : Material;
public var lightTint : Color = Color.white;
public var lightIntensity : float;

function Start () {
	this.GetComponent.<Renderer>().material = material;
	var moonlight : Light = this.GetComponentInChildren(Light);
	moonlight.intensity = lightIntensity;
	moonlight.color = lightTint;
}

function Update () {

}