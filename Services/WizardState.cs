using excel_workflow.Models;

namespace excel_workflow.Services
{
    public class WizardState
    {
        private WizardModel _wizardModel;
        private int _step;
        private bool[] _stepCompleted;

        public WizardState()
        {
            _wizardModel = new WizardModel();
            _step = 0;
            _stepCompleted = new bool[12];
        }

        public WizardModel WizardModel { get => _wizardModel; set => _wizardModel = value; }
        public int Step { get => _step; set => _step = value; }
        public bool[] StepCompleted { get => _stepCompleted; set => _stepCompleted = value; }

        public void ToggleStepDone(int step)
        {
            StepCompleted[step-1] = !StepCompleted[step-1];
        }

        public int CanAccessStep(int step)
        {
            int nextAccessibleStep = StepCompleted.TakeWhile(b => b).Count() + 1;
            return step - nextAccessibleStep;
        }
    }
}
