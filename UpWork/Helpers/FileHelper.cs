using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UpWork.ConsoleInterface;
using UpWork.Entities;
using UpWork.Logger;

namespace UpWork.Helpers
{
    public static class FileHelper
    {
        public static List<string> Categories { get; set; }
        public static List<string> Salaries { get; set; }
        public static List<string> Experiences { get; set; }
        public static List<string> Regions { get; set; }
        public static List<string> Educations { get; set; }

        static FileHelper()
        {
            
            var directory = @"Data\Categories\";
            if (Directory.Exists(directory))
            {
                Categories = new List<string>();
                ReadCategories(directory + "Categories.txt", Categories);
                Educations = new List<string>();
                ReadCategories(directory + "Educations.txt", Educations);
                Experiences = new List<string>();
                ReadCategories(directory + "Experiences.txt", Experiences);
                Regions = new List<string>();
                ReadCategories(directory + "Regions.txt", Regions);
                Salaries = new List<string>();
                ReadCategories(directory + "Salaries.txt", Salaries);
            }
            else
            {
                LoggerPublisher.OnLogError($"This directory does not exist -> {directory}");
            }

        }
        public static void ReadCategories(string filePath, List<string> list)
        {
            
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs, Encoding.ASCII))
                    {
                        while (!sr.EndOfStream)
                        {
                            list.Add(sr.ReadLine());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LoggerPublisher.OnLogError(e.Message);
            }
        }

        public static void WriteToJson(Database.Database db)
        {
            var serializer = new JsonSerializer();

            try
            {
                var workers = db.GetWorkers();
                using(var fs = new FileStream(@"Data\Workers.json", FileMode.Create))
                {
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        using (var jw = new JsonTextWriter(sw))
                        {
                            jw.Formatting = Formatting.Indented;

                            serializer.Serialize(jw, workers);
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }

            try
            {
                var employers = db.GetEmployers();
                using (var fs = new FileStream(@"Data\Employers.json", FileMode.Create))
                {
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        using (var jw = new JsonTextWriter(sw))
                        {
                            jw.Formatting = Formatting.Indented;

                            serializer.Serialize(jw, employers);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public static void ReadFromJson(ref Database.Database db)
        {
            //Database.Database db = null;

            List<Worker> workers = null;
            List<Employer> employers = null;
            var serializer = new JsonSerializer();

            try
            {
                using (var sr = new StreamReader(@"Data\Workers.json", Encoding.UTF8))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        workers = serializer.Deserialize<List<Worker>>(jr);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ConsoleScreen.Clear();
            }

            workers?.ForEach(db.Users.Add);

            try
            {
                using (var sr = new StreamReader(@"Data\Employers.json", Encoding.UTF8))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        employers = serializer.Deserialize<List<Employer>>(jr);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ConsoleScreen.Clear();
            }

            employers?.ForEach(db.Users.Add);
        }
    }
}