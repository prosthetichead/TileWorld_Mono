using System;
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
        public static async Task ReadJSONContent(string filePath)
        {
            try
            {
                
                string fname = @"Content\" + filePath + ".json";
                StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                
                StorageFile file = await InstallationFolder.GetFileAsync(fname);
                var a = file.DisplayName;
                //string text = FileIO.ReadTextAsync(file).GetResults();

                //return InstallationFolder.DisplayName;
            }
            catch (Exception)
            {

                throw;
            }
 
        }

        public static async Task WriteTextLocalStorage(string filePath, string text)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile File = await storageFolder.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(File, text);
        }
    }
}
