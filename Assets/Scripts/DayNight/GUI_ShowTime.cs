// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class GUI_ShowTime : MonoBehaviour {
///
///   Designed and Programmed by
///      Juan Ignacio Tel  (juanignaciotel.tamaroqblog@gmail.com)
///       tamaroq.blogspot.com
///
///   Copyright (C) 2013
///   Free to use and distribute
///



private GameTime gameTime;

void  Start (){
	gameTime = GameTime.Instance();
}

void  Update (){ }

void  OnGUI (){
	string hour = gameTime.GameHour.ToString();
	while (hour.Length < 2) {
		hour = "0" + hour;
	}
	if (hour=="00") hour="24";
	string minute = gameTime.GameMinute.ToString();
	while (minute.Length < 2) {
		minute = "0" + minute;
	}
	GUI.Label ( new Rect(20,15,200,50), hour + ":" + minute);
}
}