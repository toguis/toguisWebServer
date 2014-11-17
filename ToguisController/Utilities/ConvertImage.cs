using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToguisController.Utilities
{
    public class ConvertImage
    {

        public static String ConvertImageToBase64(String pPath)
        {
            String loBaseData = "";
            using(FileStream loFile = new FileStream(pPath, FileMode.Open)){
                byte[] loData = new byte[loFile.Length];
                loFile.Read(loData, 0, loData.Length);
                loBaseData = Convert.ToBase64String(loData);
            }
            return loBaseData;
        }
    }
}
