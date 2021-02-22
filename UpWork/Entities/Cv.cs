using System;
using System.Collections.Generic;
using System.Text;
using UpWork.Abstracts;
using UpWork.Interfaces;

namespace UpWork.Entities
{
    public class Cv:Id, ICv
    {
        public string Speciality { get; set; }
        public string School { get; set; }
        public int UniScore { get; set; }
        public IList<Skill> Skills { get; set; }
        public IList<WorkPlaces> WorkPlaces { get; set; }
        public Timeline Timeline { get; set; }
        public IList<Language> Languages { get; set; }
        public bool HonorsDiploma { get; set; }
        public IList<Social> Socials { get; set; }

        public override string ToString()
        {
            return $@"{base.ToString()}
{GetCv()}";
        }

        public Cv()
        {
            Skills = new List<Skill>();
            WorkPlaces = new List<WorkPlaces>();
            Languages = new List<Language>();
            Socials = new List<Social>();
        }

        public string GetCv()
        {
            var sb = new StringBuilder();
            sb.Append($"Speciality: {Speciality}\n");
            sb.Append($"School: {School}\n");
            sb.Append($"UniScore: {UniScore}\n");

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

            sb.Append(Timeline);

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