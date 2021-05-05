using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem 
{
    static string path = Application.dataPath + "/Highscore.dat";
    public static void SaveHighscore(int score)
    {
        Debug.Log("HOLA");
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        if (!File.Exists(path))
        {
            stream = new FileStream(path,FileMode.Create);
        }
        else
        {
            stream = new FileStream(path, FileMode.Open);
        }
        if(LoadHighscoreFile()<score)
        {
            formatter.Serialize(stream, score);
        }
        stream.Close();
    }
    public static int LoadHighscoreFile()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int auxToReturn = (int)formatter.Deserialize(stream);
            stream.Close();
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
