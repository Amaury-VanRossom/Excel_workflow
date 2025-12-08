namespace excel_workflow.Models.Csv
{
    public class ExamRoomRow
    {
        public string Name;
        public int Capacity;
        public double MaxUsage;
        public int RealCapacity;

        public ExamRoomRow(string name, int capacity, double maxUsage, int realCapacity)
        {
            Name = name;
            Capacity = capacity;
            MaxUsage = maxUsage;
            RealCapacity = realCapacity;
        }

        public ExamRoomRow() { }
    }
}
