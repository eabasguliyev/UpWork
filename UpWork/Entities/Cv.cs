using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UpWork.Abstracts;
using UpWork.Exceptions;
using UpWork.Interfaces;

namespace UpWork.Entities
{
    public class Cv:Id
    {
        public bool IsPublic { get; set; } = true;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Category { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Region { get; set; }
        public int Salary { get; set; }
        public IList<Skill> Skills { get; set; }
        public IList<WorkPlace> WorkPlaces { get; set; }
        public IList<Language> Languages { get; set; }
        public bool HonorsDiploma { get; set; }
        public IList<Social> Socials { get; set; }
        public int Views { get; set; }
        public Dictionary<Guid, Guid> RequestFromEmployers { get; }

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
            RequestFromEmployers = new Dictionary<Guid, Guid>();
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

            sb.Append($"\nView(s): {Views}\n");
            return sb.ToString();
        }

        public void ShowCvWithRequestCount()
        {
            Console.WriteLine(this);
            Console.WriteLine($"Request count: {RequestFromEmployers.Count}");
        }

        public void DeleteWorkplace(Guid guid)
        {
            var workplace = WorkPlaces.SingleOrDefault(w => w.Guid == guid);

            if (workplace == null)
                throw new CvException($"There is no workplace associated this id -> {guid}");

            WorkPlaces.Remove(workplace);
        }

        public void DeleteLanguage(Guid guid)
        {
            var language = Languages.SingleOrDefault(l => l.Guid == guid);

            if (language == null)
                throw new CvException($"There is no language associated this id -> {guid}");

            Languages.Remove(language);
        }

        public void DeleteSkill(Guid guid)
        {
            var skill = Skills.SingleOrDefault(s => s.Guid == guid);

            if (skill == null)
                throw new CvException($"There is no skill associated this id -> {guid}");

            Skills.Remove(skill);
        }

        public void DeleteSocial(Guid guid)
        {
            var social = Socials.SingleOrDefault(s => s.Guid == guid);

            if (social == null)
                throw new CvException($"There is no social associated this id -> {guid}");

            Socials.Remove(social);
        }

        public bool CheckEmployerRequest(Guid employerId)
        {
            if (RequestFromEmployers.Count == 0)
                return false;

            var req = RequestFromEmployers.SingleOrDefault(i => i.Key == employerId);

            return req.Key != Guid.Empty;
        }

        public void SendRequest(Guid employerId, Guid vacancyId)
        {
            RequestFromEmployers.Add(employerId, vacancyId);
        }

        public void RemoveRequest(Guid employerId)
        {
            RequestFromEmployers.Remove(employerId);
        }

        public static Cv operator ++(Cv cv)
        {
            cv.Views++;
            return cv;
        }
    }
}