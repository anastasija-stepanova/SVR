using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ARMathGame
{
    public static class SaveSystem
    {
        public static void CreateSave(out LevelsData data)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/levels.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            data = new LevelsData();

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static void DeleteSave(out LevelsData data)
        {
            string path = Application.persistentDataPath + "/levels.save";

            File.Delete(path);

            CreateSave(out data);
        }

        public static void SaveData(LevelsData data)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/levels.save";
            FileStream _stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(_stream, data);
            _stream.Close();
        }

        public static LevelsData LoadData()
        {
            string path = Application.persistentDataPath + "/levels.save";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream _stream = new FileStream(path, FileMode.Open);

                LevelsData data = formatter.Deserialize(_stream) as LevelsData;

                _stream.Close();

                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
