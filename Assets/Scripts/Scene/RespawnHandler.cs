// Used to manage the respawning of the character
using UnityEngine;
using System.Collections;

public class RespawnHandler : MonoBehaviour {

public Transform RespawnDestination; // Invisible gameObject here on top of the RespawnSprite
//private CameraHandler Camerahandler;

public AudioClip SoundOfRespawning;
private AudioSource Source { get { return GetComponent<AudioSource>(); } }

	void Start()
	{
		//Camerahandler = GameObject.Find("Camera").GetComponent<CameraHandler>();
		Source.clip = SoundOfRespawning;
		Source.volume = 1f;
		Source.playOnAwake = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "Character") 
		{
			other.transform.position = RespawnDestination.position; // new Vector3(300,100,0);

			Source.PlayOneShot(SoundOfRespawning);
			//Camerahandler.Reset();
		}
	}

}