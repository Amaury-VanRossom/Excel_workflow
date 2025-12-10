namespace excel_workflow.Models.Enums
{
    [Flags]
    public enum ExamRoomNotes
    {
        None = 0b0,
        IOEM = 0b1,
        TIAOVC = 0b10,
        SB = 0b100,
        Others = 0b1000
    }
}