using EntityFrameworkExample.Models.DbModels;
using EntityFrameworkExample.Utils;
using TechTalk.SpecFlow;

namespace EntityFrameworkExample.Transformations
{
    [Binding]
    public class TypeTransformations
    {
        private readonly ScenarioContext _scenarioContext;

        public TypeTransformations(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [StepArgumentTransformation]
        public bool TransformationTextToBool(string text)
        {
            return text switch
            {
                "is" => true,
                "are" => true,
                "is not" => false,
                "are not" => false,
                _ => throw new SpecFlowException($"Transformation text <{text}> to bool is not implemented!")
            };
        }

        [StepArgumentTransformation]
        public Author TransformationTableToAuthor(Table table)
        {
            var row = table.Rows[0];
            return new Author()
            {
                Name = row["Name"],
                Login = row["Login"],
                Email = row["Email"]
            };
        }
    }
}
