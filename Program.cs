using System;
using System.Collections.Generic;
using CounterpartProjects.Projects.ProjectPlanningTool;

namespace PlanTask
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IBasicPlanTask> tasks = new List<IBasicPlanTask>();

            var task1 = new ProjectplanTask();
            task1.Id = 1;
            task1.Title = "Task 1";
            task1.Indentation = 0;
            task1.PlannedHours = 16;
            task1.PercentComplete = 99;
            task1.StartDate = new DateTime(2019, 7, 12);
            task1.FinishDate = new DateTime(2019, 7, 15);

            var task2 = new ProjectplanTask();
            task2.Id = 2;
            task2.Title = "Task 2";
            task2.PlannedHours = 26;
            task2.PercentComplete = 98;
            task2.Indentation = 1;
            task2.StartDate = new DateTime(2019, 7, 8);
            task2.FinishDate = new DateTime(2019, 7, 13);

            var task3 = new ProjectplanTask();
            task3.Id = 3;
            task3.Title = "Task 3";
            //task3.IsMilestone = true;
            task3.PlannedHours = 13;
            task3.PercentComplete = 98;
            task3.Indentation = 1;
            task3.StartDate = new DateTime(2019, 7, 11);
            task3.FinishDate = new DateTime(2019, 7, 18);

            var task4 = new ProjectplanTask();
            task4.Id = 4;
            task4.Title = "Milestone 1";
            task4.IsMilestone = true;
            task4.Indentation = 0;

            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task4);

            var updater = new PlanTasksUpdater(tasks);
            var planDetail = updater.SortAndRecalculateProperties();

            foreach(var item in tasks) {
                Console.WriteLine(item);
            }

            Console.WriteLine(planDetail);
        }
    }
}
