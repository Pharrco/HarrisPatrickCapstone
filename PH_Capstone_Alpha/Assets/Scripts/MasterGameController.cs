using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MasterGameController : MonoBehaviour {

	static MasterGameController instance;
	GameSave save_file;

	// Use this for initialization
	void Awake () {
		
		if (instance == null)
		{
			int file_count = 0;

			instance = this;

			DontDestroyOnLoad(gameObject);

			if (File.Exists(Application.persistentDataPath + "/savedGame.game"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/savedGame.game", FileMode.Open);
				save_file = (GameSave)bf.Deserialize(file);
				GameSave.loaded_save = save_file;
				file.Close();

				Debug.Log("Saved Game Exists");
				Debug.Log("Player starts with $" + save_file.GetPlayerCash());
				file_count++;
			}
			else
			{
				save_file = new GameSave();
				GameSave.loaded_save = save_file;

				SaveCurrent();
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void SaveCurrent()
	{
		instance.save_file = GameSave.loaded_save;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGame.game");
		Debug.Log(Application.persistentDataPath + "/savedGame.game");
		bf.Serialize(file, instance.save_file);
		file.Close();

		Debug.Log("File Saved");
	}
}
