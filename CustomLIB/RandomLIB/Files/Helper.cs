using System;
using System.IO;

namespace CustomLIB.RandomLIB.Files
{
    public class Helper
    {
        public byte[] FileToByteArray(string fileNameWithPath)
        {
            byte[] fileContent = null;

            System.IO.FileStream fs = new System.IO.FileStream(fileNameWithPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);

            long byteLength = new System.IO.FileInfo(fileNameWithPath).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent;
        }


        public void ByteToFile(string fileName, byte[] fileByte)
        {
            File.WriteAllBytes(fileName, fileByte);
        }

    }
}