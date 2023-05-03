using DB_Example.Models.DbModel;
using TechTalk.SpecFlow;

namespace DB_Example.Transformations
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
                name = row["Name"],
                login = row["Login"],
                email = row["Email"]
            };
        }
    }
}
