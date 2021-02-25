using System;
using System.Collections.Generic;
using System.Text;
using UpWork.Abstracts;
using UpWork.Interfaces;

namespace UpWork.Entities
{
    public class Cv:Id, ICv
    {
        private bool _isPublic = true;

        public bool IsPublic => _isPublic;
        public string Category { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Region { get; set; }
        public string Salary { get; set; }
        public IList<Skill> Skills { get; set; }
        public IList<WorkPlace> WorkPlaces { get; set; }
        public IList<Language> Languages { get; set; }
        public bool HonorsDiploma { get; set; }
        public IList<Social> Socials { get; set; }

        public IList<Guid> RequestsFromEmployers { get; }
        public override string ToString()
        {
            return $@"{base.ToString()}
{GetCvData()}";
        }

        public Cv()
        {
            Skills = new List<Skill>();
            WorkPlaces = new List<WorkPlace>();
            Languages = new List<Language>();
            Socials = new List<Social>();
            RequestsFromEmployers = new List<Guid>();
        }

        public string GetCvData()
        {
            var sb = new StringBuilder();
            sb.Append($"Category: {Category}\n");
            sb.Append($"Education: {Education}\n");
            sb.Append($"Experience: {Experience}\n");
            sb.Append($"Region: {Region}\n");
            sb.Append($"Salary: {Salary}\n");

            if (Skills.Count != 0)
            {
                sb.Append("Skills:\n");

                foreach (var skill in Skills)
                {
                    sb.Append(skill);
                    sb.Append("\n");
                }
            }

            if (WorkPlaces.Count != 0)
            {
                sb.Append("Workplaces:\n");

                foreach (var workPlace in WorkPlaces)
                {
                    sb.Append(workPlace);
                    sb.Append("\n");
                }
            }
            
            if (Languages.Count != 0)
            {
                sb.Append("Languages:\n");

                foreach (var language in Languages)
                {
                    sb.Append(language);
                    sb.Append("\n");
                }
            }

            sb.Append($"Honors diploma: {(HonorsDiploma ? "Yes" : "No")}\n");

            if (Socials.Count != 0)
            {
                sb.Append("Social:\n");

                foreach (var social in Socials)
                {
                    sb.Append(social);
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }
    }
}