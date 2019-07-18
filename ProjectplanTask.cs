using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using CounterpartProjects.Common;
using CounterpartProjects.PlanningTool;

namespace CounterpartProjects.Projects.ProjectPlanningTool
{
    public class ProjectplanTask : IDetailedPlanTask
    {
        public ProjectplanTask()
        {
            this.Order = 0;
            this.TaskId = "0";
            this.Indentation = 0;
            this.PlannedHours = 0;
        }
        public long Id { get; set; }
        public virtual string Title { get; set; }
        [DataType(DataType.Date)]
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? FinishDate { get; set; }
        [DefaultValue(0)]
        public virtual decimal PlannedHours { get; set; }
        public virtual DateTime? ActualStartDate { get; set; }
        public virtual DateTime? ActualFinishDate { get; set; }
        [DefaultValue(0)]
        public virtual decimal ActualHours { get; set; }
        [DefaultValue(0)]
        public virtual decimal Cost { get; set; }
        [DefaultValue(0)]
        public virtual decimal PercentComplete { get; set; }
        [DefaultValue(false)]
        public virtual bool IsCompleted { get; set; }
        [DefaultValue(false)]
        public virtual bool IsExpanded { get; set; }
        [DefaultValue(false)]
        public virtual bool IsMilestone { get; set; }
        [DefaultValue(false)]
        public virtual bool IsHeaderTask { get; set; }
        public virtual int Indentation { get; set; }
        public virtual int Order { get; set; }
        public virtual long? ParentId { get; set; }
        [Required]
        public virtual string TaskId { get; set; }
        public virtual long TaskNumber { get; set; }

        public override string ToString() {
            return $"Id: {this.Id}\tParentId: {this.ParentId}\t Percent: {this.PercentComplete}\tTittle: {this.Title}\t Indentation: {this.Indentation}\t Order: {this.Order}\t TaskId: {this.TaskId} \tPlannedHours: {this.PlannedHours} \t StartDate:{((DateTime)this.StartDate).ToString("dd/MMM/yyyy")}\tFinishDate: {((DateTime)this.FinishDate).ToString("dd/MMM/yyyy")}";
        }
    }
}
