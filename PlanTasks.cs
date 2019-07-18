using System.Collections.Generic;

public class PlanTasks {
    public List<IBasicPlanTask> planTasks;
    public PlanTasks(List<IBasicPlanTask> _planTasks) {
        this.planTasks = _planTasks;
    }
}