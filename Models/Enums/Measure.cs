namespace excel_workflow.Models.Enums
{
    [Flags]
    public enum Measure
    {
        None = 0b0,
        SeperateRoom = 0b1,
        Computer = 0b10,
        DigitalCursus = 0b100,
        ExAlternateEvaluation = 0b1000,
        ExOralReview = 0b1_0000,
        ExSpecificSeat = 0b10_0000,
        TimeOral = 0b100_0000,
        TimeWritten = 0b1000_0000,
        LawfulAbscence = 0b1_0000_0000,
        LawfulAbscenceEvaluation = 0b10_0000_0000,
        Deafening = 0b100_0000_0000,
        LessonSpecificSeat = 0b1000_0000_0000,
        LeaveClass = 0b1_0000_0000_0000,
        SpellingLanguage = 0b10_0000_0000_0000,
        SpellingSentence = 0b100_0000_0000_0000,
        MoveDeadline = 0b1000_0000_0000_0000,
        MoveExam = 0b1_0000_0000_0000_0000,
        Sentence = 0b10_0000_0000_0000_0000
    }
}
