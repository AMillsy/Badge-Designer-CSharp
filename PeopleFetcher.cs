using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            while (true)
            {
                Console.WriteLine("Please enter a name: (leave empty to exit): ");
                string firstName = Console.ReadLine() ?? "";
                if (firstName == "")
                {
                    break;
                }

                Console.Write("Enter last name: ");
                string lastName = Console.ReadLine() ?? "";
                Console.Write("Enter ID: ");
                int id = Int32.Parse(Console.ReadLine() ?? "");
                Console.Write("Enter Photo URL: ");
                string photoUrl = Console.ReadLine() ?? "";
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                employees.Add(currentEmployee);
            }

            return employees;
        }

        public static async Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>();

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(
                    "https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture"
                );

                JObject json = JObject.Parse(response);
                JToken results = json.SelectToken("results") ?? "";
                Console.WriteLine(results);
                foreach (var x in results)
                {
                    var name = x.SelectToken("name");
                    var id = x.SelectToken("id");
                    var picture = x.SelectToken("picture");
                    Console.WriteLine(id);
                    Console.WriteLine(name);
                    Console.WriteLine(picture);
                    Console.WriteLine("----------------");

                    Employee employee = new Employee(
                        x.SelectToken("name.first")!.ToString(),
                        x.SelectToken("name.last")!.ToString(),
                        int.Parse(x.SelectToken("id.value")!.ToString().Replace("-", "")),
                        x.SelectToken("picture.large")!.ToString()
                    );
                    employees.Add(employee);
                }
            }

            

            return employees;
        }
    }
}
