
using Paradigmi.Abstraction;
using Progetto.Paradigmi.Test.Example;

var examples = new List<IProject>();
/*examples.Add(new InizialializzazioneClassiExample());
examples.Add(new GestioneEventiExample());*/
examples.Add(new RepositoryExample());
//examples.Add(new JsonSerializerExample());

foreach (var example in examples)
{
    //InizialializzazioneClassiExample test = (InizialializzazioneClassiExample)example;
    example.RunProject();
}


Console.ReadLine();