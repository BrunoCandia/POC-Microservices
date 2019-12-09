using Users.API.Infrastructure.Documents;

namespace Users.API.Model
{
    public class Questions : BaseDocument<string>
    {
        public string QuestionText { get; set; }
        
        public bool IsMultipleChoice { get; set; }
        
        //public List<Answer> Answers { get; set; }
        
        public string Tags { get; set; }
    }
}
