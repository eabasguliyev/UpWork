using System.Collections.Generic;
using UpWork.Entities;

namespace UpWork.Interfaces
{
    public interface ICv
    {
        string Speciality { get; set; }
        string School { get; set; }
        IList<Skill> Skills { get; set; }
        IList<WorkPlace> WorkPlaces { get; set; }
        IList<Language> Languages { get; set; }
    }
}