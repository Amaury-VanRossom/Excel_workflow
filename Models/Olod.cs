using excel_workflow.Models.Enums;

namespace excel_workflow.Models
{
    public class Olod
    {
        private string _name;
        private bool _exemption;
        private Traject _traject;
        private string _subgroup;

        public Olod(string name, bool exemption, Traject traject, string subgroup)
        {
            _name = name;
            _exemption = exemption;
            _traject = traject;
            _subgroup = subgroup;
        }

        public string Name { get => _name; set => _name = value; }
        public bool Exemption { get => _exemption; set => _exemption = value; }
        public Traject Traject { get => _traject; set => _traject = value; }
        public string Subgroup { get => _subgroup; set => _subgroup = value; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
