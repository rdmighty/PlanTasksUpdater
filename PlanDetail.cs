using System;
using System.Collections.Generic;
using System.Text;

namespace CounterpartProjects.PlanningTool
{
    public class PlanDetail
    {
        public PlanDetail() {
            PercentComplete = 0;
            PlannedHours = 0;
            StartDate = null;
            EndDate = null;
        }

        public decimal PercentComplete { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PlannedHours { get; set; }

        public override string ToString() {
            return $"PlannedHours: {this.PlannedHours}\tPercent: {this.PercentComplete}\tStart: {((DateTime)this.StartDate).ToString("dd/MMM/yyyy")}\tEnd: {((DateTime)this.EndDate).ToString("dd/MMM/yyyy")}";
        }
    }
}
