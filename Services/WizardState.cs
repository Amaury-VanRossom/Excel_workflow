using excel_workflow.Models;

namespace excel_workflow.Services
{
    public class WizardState
    {
        private WizardModel _wizardModel;

        public WizardState()
        {
            _wizardModel = new WizardModel();
        }

        public WizardModel WizardModel { get => _wizardModel; set => _wizardModel = value; }
    }
}
