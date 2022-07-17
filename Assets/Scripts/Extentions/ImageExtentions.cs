/// <summary>
/// This property always returns a value &lt; 1.
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ImageExtentions
{
	public static Sprite SpriteFromBytes(this byte[] imageBytes)
	{
		Texture2D texture2D = imageBytes.TextureFromBytes();
		return Sprite.Create(texture2D,new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
	}
	
	public static Texture2D TextureFromBytes(this byte[] imageBytes)
	{
		Texture2D image = new Texture2D(2,2);
		image.LoadImage(imageBytes);
		image.Apply(false,false);
		return image;
	}
	
	public static Sprite SpriteFromTexture(this Texture2D texture)
	{
		Texture2D texture2D = texture;
		return Sprite.Create(texture2D,new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
	}

	public static byte[] BytesFromSprite(this Sprite sprite)
	{
		return sprite.texture.EncodeToPNG();
	}
	
	public static byte[] BytesFromTexture(this Texture2D texture2D)
	{
		return texture2D.EncodeToPNG();
	}
	
	public static Texture2D LoadTextureFromPath(this string path)
	{
		
		byte[] bytes = File.ReadAllBytes(path);
		return TextureFromBytes(bytes);
	}
	
	public static Sprite LoadSpriteFromPath(this string path)
	{
		byte[] bytes = File.ReadAllBytes(path);
		return SpriteFromBytes(bytes);
	}

	public static byte[] LoadBytesFromPath(this string path)
	{
		byte[] bytes = File.ReadAllBytes(path);
		return bytes;
	}
	
	public static void SaveByteArrayInPath(this byte[] bytes,string path)
	{
		File.WriteAllBytes(path, bytes);
	}
	
	public static void SaveTextureInPath(this Texture2D texture2D,string path)
	{
		byte[] bytes = texture2D.EncodeToPNG();
		File.WriteAllBytes(path, bytes);
	}
	
	public static void SaveSpriteInPath(this Sprite sprite,string path)
	{
		byte[] bytes = sprite.texture.EncodeToPNG();
		File.WriteAllBytes(path, bytes);
	}

	public static Sprite GetSpriteFromUrl(this string path)
	{
		byte[] imageBytes = File.ReadAllBytes(path);
		return imageBytes.SpriteFromBytes();
	}
}
