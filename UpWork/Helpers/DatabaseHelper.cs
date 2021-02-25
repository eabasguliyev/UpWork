using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public static Worker GetWorker(Guid guid, IList<Worker> workers)
        {
            var worker = workers.SingleOrDefault(w => w.Guid == guid);

            if (worker == null)
                throw new DatabaseException($"There is no worker associated this guid -> {guid}");

            return worker;
        }
    }
}