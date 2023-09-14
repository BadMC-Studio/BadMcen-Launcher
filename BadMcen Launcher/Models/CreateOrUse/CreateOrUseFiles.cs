using BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using static BadMcen_Launcher.Models.Definition;

namespace BadMcen_Launcher.Models.CreateOrUse
{
    class CreateOrUseFiles
    {
        //Get Path
        static PathCode pathCode = new PathCode();
        //Create List
        static List<string> folderPath = new List<string>();
        //Use files
        //Create Json
        public class SetVersionPathJson
        {
            //Get Json Path
            public string MCPath = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings\MCPath.json");
            public void CreateJson()
            {
                if (!File.Exists(MCPath)) File.Create(MCPath);
            }
            public async void DeleteJson(int RemovePath)
            {
                //Read json
                string json = File.ReadAllText(MCPath);
                //Deserialization
                List<string> PathJson = JsonSerializer.Deserialize<List<string>>(json);
                //Delete Item
                PathJson.RemoveAt(RemovePath);
                //Serialize
                string writeJson = JsonSerializer.Serialize(PathJson);
                //Write Json
                await File.WriteAllTextAsync(MCPath, writeJson);
            }
            
            public async void WriteJson(string AddPath)
            {
                
                //Add to string
                folderPath.Add(AddPath);
                //String to json
                string json = JsonSerializer.Serialize(folderPath);
                //Write to json
                await File.WriteAllTextAsync(Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings\MCPath.json"), json);
            }
            public void ReadJson()
            {
                //Creating instances of classes
                SetVersionPathJson setVersionPathJson = new SetVersionPathJson();
                //Read json
                string json = File.ReadAllText(setVersionPathJson.MCPath);
                if (!string.IsNullOrWhiteSpace(json))
                {

                    List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(json);

                    foreach (string item in JsonToList)
                    {
                        SetVersionPathPage.Instance.SetVersionPathListView.Items.Add(item);
                        folderPath.Add(item);
                    }
                }
            }

        }
        
    }
}
