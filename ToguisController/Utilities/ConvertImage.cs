/***********************************************************************************************
 * Project: Tourist Guide System Toguis Web Services
 * University: UNIAJC
 * Authors: Julieth Candia and Carlos Morante
 * Year: 2014 - 2015
 * Version: 1.0 
 * License: GPL V2
 ***********************************************************************************************/
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
