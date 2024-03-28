// See https://aka.ms/new-console-template for more information

using Paradigmi.Abstraction;
using Progetto.Paradigmi.Test.Example;

var examples = new List<IProject>();
examples.Add(new RepositoryExample());


foreach (var example in examples)
{
    example.RunProject();
}