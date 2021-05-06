using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    static string path = Application.dataPath + "/Highscore.dat";

    public static void CreateHighscoreFile()
    {
        if(!File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, 1);
            Debug.Log("Create: " + 1);
        }
    }
    public static void SaveHighscore(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        if(File.Exists(path))
        {
            stream = new FileStream(path, FileMode.OpenOrCreate);
            int auxToReturn = (int)formatter.Deserialize(stream);
            stream.Close();
            if (auxToReturn<score)
            {
                stream = new FileStream(path, FileMode.OpenOrCreate);
                formatter.Serialize(stream, score);
                stream.Close();
                Debug.Log("Save: " + score);
            }
        }
    }
    public static int LoadHighscoreFile()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int auxToReturn = (int)formatter.Deserialize(stream);
            stream.Close();
            Debug.Log("Load: " + auxToReturn);
            return auxToReturn;
        }
        else
            return 0;
    }
    //Este codigo deberia de funcionar pero no supe implementar guardar un array dentro del archivo
    //static void asd(int score)
    //{
    //    highscore = new int[MAX_HIGHSCORES];
    //    for (int i = 0; i < MAX_HIGHSCORES; i++)
    //    {
    //        int aux;
    //        if (score > highscore[i])
    //        {
    //            aux = highscore[i];
    //            highscore[i] = score;
    //            if (i > 0)
    //            {
    //                highscore[i--] = aux;
    //            }
    //        }
    //    }
    //}
}
