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
using BadMcen_Launcher.Models.ToastNotifications;
using System.Text.Encodings.Web;

namespace BadMcen_Launcher.Models.CreateOrUse
{
    class CreateOrUseFiles
    {
        //Get Path
        static PathCode pathCode = new PathCode();

        //Get json path
        public static string MCPath = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings\MCPath.json");
        public static string JavaPath = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings\JavaPath.json");
        public static string LaunchConfigPath = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\LaunchConfig.json");
        //Create json
        public void CreateJson()
        {
            if (Directory.Exists(Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings")))
            {
                if (!File.Exists(MCPath)) File.Create(MCPath);
                if (!File.Exists(JavaPath)) File.Create(JavaPath);
                if (!File.Exists(LaunchConfigPath)) File.Create(LaunchConfigPath);
            }
           
            
        }
        //Use files
        //MCPath.json
        public class SetVersionPathJson
        {        

            //Create List
            
            public async static Task DeleteJsonElement(string RemovePath)
            {
                try
                {
                    string jsonString = await File.ReadAllTextAsync(MCPath);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(jsonString);
                        JsonToList.Remove(RemovePath);
                        //Serialize
                        string writeJson = JsonSerializer.Serialize(JsonToList);
                        //Write Json
                        await File.WriteAllTextAsync(MCPath, writeJson);
                    }
                }
                catch (Exception ex)
                {
                    //ErrorToast
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(MCPath, string.Empty);
                }
                
            }
            
            public async static Task WriteJson(string AddPath)
            {
                try
                {
                    string jsonString = await File.ReadAllTextAsync(MCPath);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(jsonString);
                        //Add to string
                        JsonToList.Add(AddPath);
                        //String to json
                        string json = JsonSerializer.Serialize(JsonToList);
                        //Write to json
                        await File.WriteAllTextAsync(MCPath, json);
                    }
                    else
                    {
                        List<string> JsonToList = new List<string>();
                        //Add to string
                        JsonToList.Add(AddPath);
                        //String to json
                        string json = JsonSerializer.Serialize(JsonToList);
                        //Write to json
                        await File.WriteAllTextAsync(MCPath, json);
                    }
                }
                catch (Exception ex)
                {
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(MCPath, string.Empty);
                }
                
                
            }
            public static List<String> ReadJson()
            {
                //Read json
                try
                {
                    string json = File.ReadAllText(MCPath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(json);
                        return JsonToList;
                    }
                }
                catch (Exception ex)
                {
                    //ErrorToast
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(MCPath, string.Empty);
                }
                return null;
            }

        }
        //LaunchConfig.json
        public class LaunchInfo
        {
            
            public static async void WriteJson(string AddDictionary, object AddWorth)
            {
                try
                {
                    string jsonString = await File.ReadAllTextAsync(LaunchConfigPath);
                    var JsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };
                    if (string.IsNullOrEmpty(jsonString))
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        dict.Add(AddDictionary, AddWorth);
                        string json = JsonSerializer.Serialize(dict, JsonOptions);
                        //Write to json
                        await File.WriteAllTextAsync(LaunchConfigPath, json);
                    }
                    else
                    {
                        Dictionary<string, object> dict = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
                        //Add to string
                        var config = new Dictionary<string, object> { { AddDictionary, AddWorth } };
                        foreach (var item in dict)
                        {
                            if (!config.ContainsKey(item.Key))
                            {
                                config.Add(item.Key, item.Value);
                            }
                            //String to json
                            string json = JsonSerializer.Serialize(config, JsonOptions);
                            //Write to json
                            await File.WriteAllTextAsync(LaunchConfigPath, json);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ErrorToast
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(LaunchConfigPath, string.Empty);
                }
                
                
                
            }
            //Read json
            public static object ReadJson(string DictionaryKey)
            {
                //Read json and deserialize it.
                try
                {
                    string ReadFile = File.ReadAllText(LaunchConfigPath);
                    if (!string.IsNullOrEmpty(ReadFile))
                    {
                        Dictionary<string, object> dict = JsonSerializer.Deserialize<Dictionary<string, object>>(ReadFile);
                        if (!string.IsNullOrWhiteSpace(DictionaryKey))
                        {
                            //Get the value of a specific key

                            if (dict.TryGetValue(DictionaryKey, out var value))
                            {
                                object ReturnValue = value;
                                //Return to Dictionary
                                return ReturnValue;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ErrorToast
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(LaunchConfigPath, string.Empty);
                }
                return null;
            }
            
        }

        public class SetJavaPathJson
        {
            public static async Task WriteJson(string AddPath)
            {
                try
                {
                    var JsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };
                    string jsonString = await File.ReadAllTextAsync(JavaPath);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(jsonString);
                        //Add to string
                        JsonToList.Add(AddPath);
                        JsonToList = JsonToList.Distinct().ToList();

                        //String to json
                        string json = JsonSerializer.Serialize(JsonToList.Distinct(), JsonOptions);
                        //Write to json
                        await File.WriteAllTextAsync(JavaPath, json);
                    }
                    else
                    {

                        List<string> JsonToList = new List<string>();
                        //Add to string
                        JsonToList.Add(AddPath);
                        //String to json
                        string json = JsonSerializer.Serialize(JsonToList.Distinct(), JsonOptions);
                        //Write to json
                        await File.WriteAllTextAsync(JavaPath, json);
                    }
                }
                catch (Exception ex)
                {
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(MCPath, string.Empty);
                }
            }
            public static List<String> ReadJson()
            {
                //Read json
                try
                {
                    string json = File.ReadAllText(JavaPath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(json);
                        return JsonToList;
                    }
                }
                catch (Exception ex)
                {
                    //ErrorToast
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(JavaPath, string.Empty);
                }
                return null;
            }
            public async static Task DeleteJsonElement(string RemovePath)
            {
                try
                {
                    string jsonString = await File.ReadAllTextAsync(JavaPath);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        var JsonOptions = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        };
                        List<string> JsonToList = JsonSerializer.Deserialize<List<string>>(jsonString);
                        JsonToList.Remove(RemovePath);
                        //Serialize
                        string writeJson = JsonSerializer.Serialize(JsonToList, JsonOptions);
                        //Write Json
                        await File.WriteAllTextAsync(JavaPath, writeJson);
                    }
                }
                catch (Exception ex)
                {
                    //ErrorToast
                    AppToastNotification.ErrorToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message + "\n" + LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonErrorSubtitle"));
                    File.WriteAllText(JavaPath, string.Empty);
                }

            }
        }
    }
}
