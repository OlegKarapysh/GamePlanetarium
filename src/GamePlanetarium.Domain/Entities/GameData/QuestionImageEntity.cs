namespace GamePlanetarium.Domain.Entities.GameData;

public class QuestionImageEntity
{
    public int Id { get; set; }
    public string ImageName { get; set; }
    public int HashCode { get; set; }
    public byte[] ImageSource { get; set; }
    
    public QuestionEntity Question { get; set; }
}