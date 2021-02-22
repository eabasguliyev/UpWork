using System.Collections.Generic;
using UpWork.Interfaces;

namespace UpWork.Entities
{
    public class Cv:ICv
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

        public Cv()
        {
            Skills = new List<Skill>();
            WorkPlaces = new List<WorkPlaces>();
            Languages = new List<Language>();
            Socials = new List<Social>();
        }
    }
}