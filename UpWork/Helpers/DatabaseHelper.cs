using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UpWork.Abstracts;
using UpWork.Entities;
using UpWork.Exceptions;

namespace UpWork.Helpers
{
    public static class DatabaseHelper
    {
        public static void ShowWorkers(IList<Worker> workers)
        {
            if (workers.Count == 0)
                throw new DataException("There is no worker!");

            foreach (var worker in workers)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine(worker);
            }
        }

        public static User GetUser(Guid guid, IList<User> users)
        {
            var user = users.SingleOrDefault(u => u.Guid == guid);

            if (user == null)
                throw new DatabaseException($"There is no user associated this guid -> {guid}");

            return user;
        }

    }
}