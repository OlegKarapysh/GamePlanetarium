namespace GamePlanetarium.Domain.Question;

public interface IQuestion
{
    string Text { get; set; }
    Answer.Answer[] Answers { get; }
    QuestionImage QuestionImage { get; }
    bool IsAnswered { get; }

    bool CheckIfCorrect(params Answer.Answer[] answers);
    bool TryAnswer(params Answer.Answer[] answers);
}