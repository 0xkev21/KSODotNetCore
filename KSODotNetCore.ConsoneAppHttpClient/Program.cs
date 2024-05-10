using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//string jsonStr = await File.ReadAllTextAsync("data.json");
//var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

//foreach(var question in model.questions)
//{
//    Console.WriteLine(question.questionNo);
//}

string jsonStr = await File.ReadAllTextAsync("Snakes.json");
var snakesModel = JsonConvert.DeserializeObject<SnakesModel>(jsonStr);

foreach (var snake in snakesModel.snakes)
{
    Console.WriteLine(snake.Id);
}

// Json to C#
// C# to Json // need Package

Console.ReadLine();


public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}



public class SnakesModel
{
    public Snake[] snakes { get; set; }
}

public class Snake
{
    public int Id { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}

