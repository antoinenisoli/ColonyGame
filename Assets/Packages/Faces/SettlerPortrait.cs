using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct Replacable
{
	public string Name;
	public Image image;
	public Sprite[] sprites;
}

public class SettlerPortrait : MonoBehaviour 
{
	[SerializeField] Replacable[] portraitParts;

	void Start() 
	{
		RandomizeCharacter();
	}
	
	[ContextMenu(nameof(RandomizeCharacter))]
	public void RandomizeCharacter()
	{
        foreach (var item in portraitParts)
			item.image.sprite = item.sprites[Random.Range(0, item.sprites.Length)];
	}

}
