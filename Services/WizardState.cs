using excel_workflow.Models;

namespace excel_workflow.Services
{
    public class WizardState
    {
        private WizardModel _wizardModel;
        private int _step;
        private bool _isHydrated;
        public WizardState()
        {
            _wizardModel = new WizardModel();
            _step = 1;
            _isHydrated = false;
        }

        public WizardModel WizardModel { get => _wizardModel; set => _wizardModel = value; }
        public int Step { get => _step; set => _step = value; }
        public bool IsHydrated { get => _isHydrated; set => _isHydrated = value; }

        public void ToggleStepDone()
        {
            Step++;
        }

        public int CanAccessStep(int step)
        {
            Console.WriteLine($"Diff is {step} - {Step}");
            return step - Step;
        }
    }
}
