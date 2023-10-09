using BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.AppNotifications.Builder;
using Microsoft.Windows.AppNotifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using static BadMcen_Launcher.Models.Definition;
using Windows.System;
using Microsoft.Toolkit.Uwp.Notifications;

namespace BadMcen_Launcher.Models.CreateOrUse
{
    class CreateOrUseFiles
    {
        //Get Path
        static PathCode pathCode = new PathCode();
        //Create List
        static List<string> MinecraftPath = new List<string>();
        //Get json path
        public static string MCPath = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings\MCPath.json");
        public static string LaunchConfigPath = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\LaunchConfig.json");
        //Create json
        public void CreateJson()
        {
            if (Directory.Exists(Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings")))
            {
                if (!File.Exists(MCPath)) File.Create(MCPath);
                if (!File.Exists(LaunchConfigPath)) File.Create(LaunchConfigPath);
            }
           
            
        }
        //Use files
        public class SetVersionPathJson
        {
            
            public async void DeleteJsonElement(string RemovePath)
            {
                MinecraftPath.Remove(RemovePath);
                //Serialize
                string writeJson = JsonSerializer.Serialize(MinecraftPath);
                //Write Json
                await File.WriteAllTextAsync(MCPath, writeJson);
            }
            
            public async void WriteJson(string AddPath)
            {
                
                //Add to string
                MinecraftPath.Add(AddPath);
                //String to json
                string json = JsonSerializer.Serialize(MinecraftPath);
                //Write to json
                await File.WriteAllTextAsync(MCPath, json);
            }
            public void ReadJson()
            {
                //Read json
                string json = File.ReadAllText(MCPath);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(json);
                    MinecraftPath.Clear();
                    foreach (string item in JsonToList)
                    {
                        SetVersionPathPage.Instance.SetVersionPathListView.Items.Add(item);
                        MinecraftPath.Add(item);
                    }
                }
            }

        }

        public class LaunchInfo
        {
            public static async void WriteJson(string AddDictionary, object AddWorth)
            {
                var JsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                //Add to string
                var config = new Dictionary<string, object> { {AddDictionary, AddWorth} };
                //String to json
                string json = JsonSerializer.Serialize(config, JsonOptions);
                //Write to json
                await File.WriteAllTextAsync(LaunchConfigPath, json);
                
            }
            //Read json
            public static object ReadJson(string DictionaryKey)
            {
                //Read json and deserialize it.
                
                    try
                    {
                        Dictionary<string, object> dict = JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(LaunchConfigPath));
                        if (!string.IsNullOrWhiteSpace(DictionaryKey))
                        {
                            //Get the value of a specific key
                            object ReturnValue = dict[DictionaryKey];
                            //Return to Dictionary
                            return ReturnValue;
                        }
                    }
                    catch
                    {
                    new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Andrew sent you a picture")
                    .AddText("Check this out, The Enchantments in Washington!")
                    .Show();
                }
                return null;
            }
            
        }
        
    }
}
