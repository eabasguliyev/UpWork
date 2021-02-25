using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UpWork.Logger;

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
                ReadFromFile(directory + "Categories.txt", Categories);
                Educations = new List<string>();
                ReadFromFile(directory + "Educations.txt", Educations);
                Experiences = new List<string>();
                ReadFromFile(directory + "Experiences.txt", Experiences);
                Regions = new List<string>();
                ReadFromFile(directory + "Regions.txt", Regions);
                Salaries = new List<string>();
                ReadFromFile(directory + "Salaries.txt", Salaries);
            }
            else
            {
                logger.Error($"This directory does not exist -> {directory}");
            }

        }
        public static void ReadFromFile(string filePath, List<string> list)
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
    }
}