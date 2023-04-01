using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lab01Sydorova.DataManage
{
    internal class SerializationProc
    {
        internal static void Serialize<TObject>(TObject obj, string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                if (file.Exists) { file.Delete(); }
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(stream, obj);
                }
            }
            catch (Exception e) { throw new Exception($"Failed serialization at {filePath}", e); }
        }

        internal static TObject Deserialize<TObject>(string filePath) where TObject : class
        {
            try
            {
                if (!FileManageCl.CreateFolderAndCheckFileExistence(filePath))
                    throw new FileNotFoundException("File doesn't exist.");
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    return (TObject)formatter.Deserialize(stream);
                }
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException($"Failed deserialization at {filePath}", e);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed deserialization at {filePath}", e);
            }
        }
    }
}
