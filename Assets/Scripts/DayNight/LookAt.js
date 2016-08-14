#pragma strict

var lightTarget : GameObject;

function Start () {

}

function Update () {
	transform.LookAt(lightTarget.transform);
}