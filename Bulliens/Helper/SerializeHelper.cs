using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Boiler.Helper
{
    public class SerializeHelper
    {
        public static void BinarySerialize(string fileName, ArrayList arrayList)
        {
            if (arrayList != null && arrayList.Count > 0)
            {
                try
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        for (int i = 0; i < arrayList.Count; i++)
                        {
                            formatter.Serialize(fileStream, arrayList[i]);
                        }
                        fileStream.Flush();
                        fileStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        //public static void BinaryDeserialize(string fileName, ArrayList arrayList)
        //{

        //        try
        //        {
        //            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        //            {
        //                BinaryFormatter formatter = new BinaryFormatter();
        //                for (int i = 0; i < arrayList.Count; i++)
        //                {
        //                    arrayList[i]=formatter.Deserialize(fileStream);
        //                }
        //                fileStream.Close();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }

        //}
    }
}
