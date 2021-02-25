using System;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;
using UpWork.Interfaces;
using UpWork.Sides;

namespace UpWork.Extensions
{
    public static class CvExtensions
    {
        public static void DeleteWorkplace(this Cv cv, Guid guid)
        {
            var workplace = cv.WorkPlaces.SingleOrDefault(w => w.Guid == guid);

            if (workplace == null)
                throw new CvException($"There is no workplace associated this id -> {guid}");

            cv.WorkPlaces.Remove(workplace);
        }

        public static void DeleteLanguage(this Cv cv, Guid guid)
        {
            var language = cv.Languages.SingleOrDefault(l => l.Guid == guid);

            if (language == null)
                throw new CvException($"There is no language associated this id -> {guid}");

            cv.Languages.Remove(language);
        }

        public static void DeleteSkill(this Cv cv, Guid guid)
        {
            var skill = cv.Skills.SingleOrDefault(s => s.Guid == guid);

            if (skill == null)
                throw new CvException($"There is no skill associated this id -> {guid}");

            cv.Skills.Remove(skill);
        }

        public static void DeleteSocial(this Cv cv, Guid guid)
        {
            var social = cv.Socials.SingleOrDefault(s => s.Guid == guid);

            if (social == null)
                throw new CvException($"There is no social associated this id -> {guid}");

            cv.Socials.Remove(social);
        }

        public static bool CheckEmployerRequest(this Cv cv, Guid employerId)
        {
            if (cv.RequestsFromEmployers.Count == 0)
                return false;

            var req = cv.RequestsFromEmployers.SingleOrDefault(i => i == employerId);

            return req != Guid.Empty;
        }

        public static void SendRequest(this Cv cv, Guid employerId)
        {
            cv.RequestsFromEmployers.Add(employerId);
        }

        public static void CancelRequest(this Cv cv, Guid employerId)
        {
            cv.RequestsFromEmployers.Remove(employerId);
        }
    }
}