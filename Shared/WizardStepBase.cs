using Blazored.SessionStorage;
using excel_workflow.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace excel_workflow.Shared
{
    public class WizardStepBase : ComponentBase
    {
        [Inject] public required WizardState Wiz { get; set; }
        [Inject] public required NavigationManager Nav { get; set; }
        [Inject] public required ISessionStorageService Session { get; set; }
        [CascadingParameter] public required int Step { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Console.WriteLine($"Step: {Wiz.Step}");
        }

        protected override async Task OnInitializedAsync()
        {
            if (!Wiz.IsHydrated)
            {
                await FetchStateFromSessionStorage();
                Wiz.IsHydrated = true;
            }
            EnsureValidStep();
        }
        protected void EnsureValidStep()
        {
            Console.WriteLine($"Parameter: {Step}");
            int diff = Wiz.CanAccessStep(Step);
            if (diff != 0)
            {
                Nav.NavigateTo($"/wizard/step/{Step-diff}");
            }
            else
            {
                Wiz.Step = Step;
            }
        }

        protected async Task ResetWizard()
        {
            await Session.RemoveItemAsync("wizard");
            Wiz = new WizardState();
            Nav.NavigateTo("/", forceLoad: true);
        }

        private async Task FetchStateFromSessionStorage()
        {
            var saved = await Session.GetItemAsync<WizardState>("wizard");

            if (saved != null && saved.Step != Wiz.Step)
            {
                Wiz.Step = saved.Step;
                Wiz.WizardModel.Olod = saved.WizardModel.Olod;
                Wiz.WizardModel.ExamType = saved.WizardModel.ExamType;
                Wiz.WizardModel.DistanceLearningClassroomType = saved.WizardModel.DistanceLearningClassroomType;
                Wiz.WizardModel.MeasuresTaken = saved.WizardModel.MeasuresTaken;
                Wiz.WizardModel.Students = saved.WizardModel.Students;
                Wiz.WizardModel.Rooms = saved.WizardModel.Rooms;
                Wiz.WizardModel.AssignedStudents = saved.WizardModel.AssignedStudents;
            }
        }

        protected async Task SaveStateToSessionStorage()
        {
            await Session.SetItemAsync("wizard", Wiz);
        }

        protected async Task GoToNextStep()
        {
            Wiz.ToggleStepDone();
            await SaveStateToSessionStorage();
            Nav.NavigateTo($"/wizard/step/{Step+1}");
        }
    }
}
