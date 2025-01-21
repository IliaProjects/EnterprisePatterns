using SimpleFactory;


Name name1 = new NameFactory().getName("Jordan, Michael");
Console.WriteLine("name1 firstname: " + name1.getFirstName());
Console.WriteLine("name1 lastname: " + name1.getLastName());

Console.WriteLine();

Name name2 = new NameFactory().getName("Michael Jordan");
Console.WriteLine("name2 firstname: " + name2.getFirstName());
Console.WriteLine("name2 lastname: " + name2.getLastName());



