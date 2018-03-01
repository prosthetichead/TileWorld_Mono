﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.Xna.Framework.Utilities;

namespace TileWorld_Mono
{
    public  static class FileSystem
    {
    //    public static async Task ReadFile(string filePath)
    //    {
    //        try
    //        {
    //            StorageFolder InstallationFolder = Windows.Storage.
                
    //            StorageFile file = await InstallationFolder.GetFileAsync(fname);
    //            var a = file.DisplayName;
    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }
 
    //    }

        public static async Task WriteTextLocalStorage(string filePath, string text)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile File = await storageFolder.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            Game.debugConsole.WriteLine("Disc IO Started for file " + filePath);
            await Windows.Storage.FileIO.WriteTextAsync(File, text);
            Game.debugConsole.WriteLine("Disc IO Complete for file " + filePath);

        }

        public static async Task<String> ReadTextLocalStorage(string filePath)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile File = await storageFolder.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            string text = await Windows.Storage.FileIO.ReadTextAsync(File);

            return text;
        }
    }
}
