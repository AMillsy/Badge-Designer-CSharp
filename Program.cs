namespace CatWorx.BadgeMaker
{
  
  class Program
  {
    async static Task Main(string[] args)
    {
      List<Employee> totalEmployees = new List<Employee>();

      Console.WriteLine("Do you want to manually add new Employees: (y/n)");

      string input = Console.ReadLine() ?? "";

      if(input == "y"){
        List<Employee> newEmployees = PeopleFetcher.GetEmployees();
        totalEmployees.AddRange(newEmployees);
      }

      List<Employee> apiEmployees = await PeopleFetcher.GetFromApi();
        totalEmployees.AddRange(apiEmployees);

        Util.PrintEmployees(totalEmployees);
        Util.MakeCSV(totalEmployees);
        await Util.MakeBadges(totalEmployees);
    }
  }
}
    