using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reflection;

namespace Lesson8
{

    /// <summary>
    /// Контролирует, что возраст не превышает разумного предела
    /// </summary>
    public class ValidBirthdayDate : ValidationAttribute
    {
        public int MaxYear { get; set; } = 150;

        public override bool IsValid(object value)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParse(Convert.ToString(value), out dateTime);

            if (dateTime < DateTime.Now.AddYears(-MaxYear) || dateTime > DateTime.Now)
                isValid = false;

            return isValid;
        }
    }

    class ApplicationSettings : ConfigurationSection
    {
        [ConfigurationProperty("Username", DefaultValue = "Ivan", IsRequired = true)]
        public string Username 
        {
            get => (string)this["Username"]; 
            set
            {
                this["Username"] = value;
            }
        }

        [ConfigurationProperty("Birthday", IsRequired = true)]
        [ValidBirthdayDate(MaxYear = 100)]
        public DateTime Birthday
        {
            get => (DateTime)this["Birthday"];
            set
            {
                this["Birthday"] = value;
            }
        }

        [ConfigurationProperty("Job", DefaultValue = "Manager", IsRequired = true)]
        public string Job
        {
            get => (string)this["Job"];
            set
            {
                this["Job"] = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string greeting = ConfigurationManager.AppSettings["Greeting"];
            Console.WriteLine(greeting);

            ApplicationSettings settings = LoadSettings();
            Console.WriteLine($"Previously saved settings:\nName: {settings.Username}\nBirthday: {settings.Birthday}\nJob: {settings.Job}");

            Console.WriteLine("Enter name");
            settings.Username = Console.ReadLine();

            Console.WriteLine($"Enter birthday, format: {DateTime.Now}");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthday))
                settings.Birthday = birthday;
            else
                Console.WriteLine("Invalid date");

            Console.WriteLine("Enter job");
            settings.Job = Console.ReadLine();

            var results = new List<ValidationResult>();
            var context = new ValidationContext(settings);
            if (!Validator.TryValidateObject(settings, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            SaveSettings(settings);
        }

        private static void SaveSettings(ApplicationSettings settings)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);

            if (cfg.Sections["ApplicationSettings"] == null)
                cfg.Sections.Add("ApplicationSettings", settings);

            var section = cfg.Sections["ApplicationSettings"] as ApplicationSettings;
            section.Username    = settings.Username;
            section.Birthday    = settings.Birthday;
            section.Job         = settings.Job;

            cfg.Save();
        }

        private static ApplicationSettings LoadSettings()
        {
            ApplicationSettings settings = new ApplicationSettings();

            Configuration cfg   = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            if (cfg.Sections["ApplicationSettings"] == null)
                return settings;

            settings = cfg.Sections["ApplicationSettings"] as ApplicationSettings;

            return settings;
        }
    }
}
