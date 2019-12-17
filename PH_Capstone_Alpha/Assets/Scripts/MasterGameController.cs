using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MasterGameController : MonoBehaviour {

	static MasterGameController instance;
	GameSave save_file;
	int max_level = 29;

	// Use this for initialization
	void Awake () {
		
		if (instance == null)
		{
			int file_count = 0;

			instance = this;

			DontDestroyOnLoad(gameObject);

			if (File.Exists(Application.persistentDataPath + "/save.gs"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/save.gs", FileMode.Open);
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

	public static void SaveCurrent()
	{
		instance.save_file = GameSave.loaded_save;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/save.gs");
		Debug.Log(Application.persistentDataPath + "/save.gs");
		bf.Serialize(file, instance.save_file);
		file.Close();

		Debug.Log("File Saved");
	}

	public void Update()
	{
		if (GameSave.loaded_save.GetLevelProgress() < max_level)
		{
			if (Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.L))
			{
				Debug.Log("Unlock activated");
				GameSave.loaded_save.CompleteLevel(max_level);
				SaveCurrent();
			}
		}
	}
}
