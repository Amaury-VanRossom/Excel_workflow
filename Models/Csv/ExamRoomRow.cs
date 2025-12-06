namespace excel_workflow.Models.Csv
{
    public class ExamRoomRow(string name, int capacity, float maxUsage, int realCapacity)
    {
        public string Name = name;
        public int Capacity = capacity;
        public float MaxUsage = maxUsage;
        public int RealCapacity = realCapacity;
    }
}
