using System;
using System.Collections.Generic;
using System.Linq;
using UpWork.Entities;
using UpWork.Exceptions;
using UpWork.Interfaces;
using UpWork.Sides;

namespace UpWork.Extensions
{
    public static class WorkerExtensions
    {
        public static void ShowAllCv(this Worker worker, bool onlyPublic = false)
        {
            if (worker.Cvs.Count == 0)
                throw new CvException("There is no cv!");

            
            if (onlyPublic)
            {
                var flag = false;

                foreach (var ICv in worker.Cvs)
                {
                    if (ICv is Cv cv && cv.IsPublic)
                    {
                        Console.WriteLine("--------------------------------------");

                        Console.WriteLine(cv);
                        flag = true;
                    }
                }

                if (!flag)
                    throw new CvException("There is no Cv!");
            }
            else
            {
                foreach (var cv in worker.Cvs)
                {
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine(cv);
                }
            }
        }

        public static void DeleteCv(this Worker worker, Guid guid)
        {
            var cv = worker.Cvs.SingleOrDefault(c => ((Cv) c).Guid == guid);


            if (cv == null)
                throw new CvException($"There is no cv associated this guid -> {guid}");
            
            worker.Cvs.Remove(cv);
        }

        public static ICv GetCv(this Worker worker, Guid guid)
        {
            var cv = worker.Cvs.SingleOrDefault(c => ((Cv) c).Guid == guid);

            if (cv == null)
                throw new CvException($"There is no cv associated this guid -> {guid}");

            return cv;
        }

        public static void ShowShortNotfInfo(this Worker worker)
        {
            if (worker.Notifications.Count == 0)
                throw new NotificationException("There is no notification!");

            foreach (var notification in worker.Notifications)
            {
                Console.WriteLine($"Guid: {notification.Guid}");
                Console.WriteLine($"Title: {notification.Title}");
            }
        }
    }
}