using excel_workflow.Services;
using Microsoft.AspNetCore.Components;

namespace excel_workflow.Shared
{
    public class WizardStepBase : ComponentBase
    {
        [Inject] public required WizardState Wiz { get; set; }
        [Inject] public required NavigationManager Nav { get; set; }
        [Parameter] public int Step { get; set; }
        protected void EnsureValidStep()
        {
            int diff = Wiz.CanAccessStep(Step);
            if (diff != 0)
            {
                int completedInOrder = Wiz.StepCompleted.TakeWhile(b => b).Count();
                int nextAccessibleStep = completedInOrder + 1;
                Nav.NavigateTo($"/wizard/step/{nextAccessibleStep}");
            }
            else
            {
                Wiz.Step = Step;
            }
        }

        protected void GoToNextStep()
        {
            Wiz.ToggleStepDone(Step);
            Nav.NavigateTo($"/wizare/step/{Step+1}");
        }
    }
}
