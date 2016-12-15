using UnityEngine;
using System.Collections;

public static class GP8ResourceUtility {


	public static Object GetBackgroundMusicPrefab(){
		return Resources.Load<Object>(GP8Strings.RESOURCE_PATH_FOR_MUSIC_PREFABS);
	}
}
