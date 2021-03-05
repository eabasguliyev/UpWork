using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UpWork.Entities;
using UpWork.Exceptions;
using UpWork.Logger;
using UpWork.Sides.Employee;

namespace UpWork.Data
{
    public static class Data
    {
        public static List<string> Categories { get; set; }
        public static List<string> Salaries { get; set; }
        public static List<string> Experiences { get; set; }
        public static List<string> Regions { get; set; }
        public static List<string> Educations { get; set; }

        static Data()
        {
            var logger = new ConsoleLogger();
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
                logger.Error($"This directory does not exist -> {directory}");
            }

        }
        public static void ReadCategories(string filePath, List<string> list)
        {
            var logger = new ConsoleLogger();
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
                logger.Error(e.Message);
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

            using (var sr = new StreamReader(@"Data\Workers.json", Encoding.UTF8))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    workers = serializer.Deserialize<List<Worker>>(jr);
                }
            }

            workers?.ForEach(db.Users.Add);

            using (var sr = new StreamReader(@"Data\Employers.json", Encoding.UTF8))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    employers = serializer.Deserialize<List<Employer>>(jr);
                }
            }

            employers?.ForEach(db.Users.Add);
        }
    }
}