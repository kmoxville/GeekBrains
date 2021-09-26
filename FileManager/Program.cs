using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace FileManager
{
    class Program
    {
        class Settings : ConfigurationSection
        {
            [ConfigurationProperty("Path", IsRequired = true)]
            public string Path 
            { 
                get => (string)this["Path"]; 
                set
                {
                    this["Path"] = value;
                }
            }

            [ConfigurationProperty("History", IsRequired = true)]
            public string History 
            {
                get => (string)this["History"];
                set
                {
                    this["History"] = value;
                } 
            }
        }

        static void Main(string[] args)
        {
            Settings settings = LoadSettings();

            History history = new History(settings.History.Split('\n'));

            FileManagerModel fm = new FileManagerModel(settings.Path);
            FileManagerView fv = new FileManagerView(fm, history);

            fv.Run();

            settings.Path = fm.CurrentDirectory.FullName;
            settings.History = string.Join('\n', history.Commands);

            SaveSettings(settings);
        }

        private static void SaveSettings(Settings settings)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

            var configSection = cfg.GetSection("Settings") as Settings;
            if (configSection == null)
                cfg.Sections.Add("Settings", settings);

            configSection = cfg.GetSection("Settings") as Settings;
            configSection.Path = settings.Path;
            configSection.History = settings.History;

            cfg.Save();
        }

        private static Settings LoadSettings()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            Settings settings = cfg.GetSection("Settings") as Settings;
                    
            return settings ?? new Settings() { Path = Environment.CurrentDirectory, History = "" };
        }
    }
}
