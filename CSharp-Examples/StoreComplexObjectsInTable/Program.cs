using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace StoreComplexObjectsInTable
{
	class Program
	{
		static void Main(string[] args)
		{
			// Make a connection to the Table Storage account
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
				"YOUR-ACCOUNT-NAME-AND-ACCOUNT-KEY-STRING");

			// Create a Table client
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

			// Get a reference to the table we want to add new entities in
			CloudTable table = tableClient.GetTableReference("THE-NAME-OF-THE-TABLE");

			// create this table if it does not already exist
			table.CreateIfNotExists();

			// Start creating a new Person object. Scroll down to find more information about the properties of the class
			List<string> hobbies = new List<string>() { "swimming", "programming" };

			var child1 = new Child()
			{
				Firstname = "John",
				Age = 10
			};

			var child2 = new Child()
			{
				Firstname = "Mary",
				Age = 15
			};

			List<Child> childs = new List<Child>() { child1, child2 };

			// Serialize the complex types into strings and store them in the Person TableEntity
			var person = new Person("Christos")
			{
				Lastname = "Monogios",
				Hobbies = JsonConvert.SerializeObject(hobbies),
				Children = JsonConvert.SerializeObject(childs)
			};

			// Add the entity to the table
			TableOperation operation = TableOperation.InsertOrMerge(person);
			table.Execute(operation);

			// Create a new query to retrieve the inserted entity back
			TableQuery<Person> query = new TableQuery<Person>();

			// execute the query against the Table Storage
			var result = table.ExecuteQuery<Person>(query);

			// We got the answer, now we have to deserialize the strings we want to convert into complex objects
			// Notice that we use the generic method in order to define the type we want to convert the string to.
			var firstPerson = result.First();
			var firstPersonHobbies = JsonConvert.DeserializeObject<List<string>>(firstPerson.Hobbies);
			var firstPersonChildrens = JsonConvert.DeserializeObject<List<Child>>(firstPerson.Children);

			// Show the deserialized properties
			foreach (var hobby in firstPersonHobbies)
			{
				Console.WriteLine(hobby);
			}

			foreach (var child in firstPersonChildrens)
			{
				Console.WriteLine(child.Firstname);
			}

			Console.ReadLine();
		}
	}

	// remember to inherit your POCOs from the TableEntity class
	class Person : TableEntity
	{
		public Person()
		{
		}

		public Person(string firstname)
		{
			this.PartitionKey = firstname;
			this.RowKey = DateTime.UtcNow.Ticks.ToString();
		}

		public string Lastname
		{
			get; set;
		}

		public string Hobbies
		{
			get; set;
		}

		public string Children
		{
			get; set;
		}
	}

	class Child
	{
		public string Firstname
		{
			get; set;
		}

		public int Age
		{
			get; set;
		}
	}
}