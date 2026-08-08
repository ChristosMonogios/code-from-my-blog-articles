using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * C#6 feature: Use the word static for a static class and then use its methods without the class name.
 */
using static NewFeaturesInCSharpSix.ProgramHelper;

namespace NewFeaturesInCSharpSix
{
	class Program
	{
		static void Main(string[] args)
		{
			var myDog = new Dog("Jack", GetARandomAge());
			myDog.Mouth = new Mouth();
			myDog.Mouth.TeehtsInMouth[0] = new Tooth();

			/*
			 * C#6 feature: Avoid null reference exceptions with the ? operator.
			 */
			if (myDog?.Mouth?.TeehtsInMouth[0]?.ScientificName == "default tooth name")
			{
				Console.WriteLine("default tooth found!");
			}

			Console.WriteLine(myDog.CalculateAgeOfDogAsAgeOfHuman());

			Console.WriteLine(myDog.GetInformationAboutDog());

			Console.WriteLine("Came till here!");
			Console.ReadLine();
		}
	}

	class Dog
	{
		private Mouth mouth;
		private int age;
		private string name;

		public Dog(string name, int age)
		{
			this.name = name;
			this.age = age;
		}

		internal Mouth Mouth
		{
			get
			{
				return mouth;
			}

			set
			{
				mouth = value;
			}
		}

		public int Age
		{
			get
			{
				return age;
			}

			set
			{
				age = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
			}
		}

		/*
		 * C#6 feature: Expression bodied methods for "one line" methods
		 */
		public int CalculateAgeOfDogAsAgeOfHuman()
			=> this.age * 7;

		/*
		 * C#6 feature: $ operator instead of String.Format. Code looks cleaner and more compact
		 */
		public string GetInformationAboutDog()
			=> $"Dog's name: {this.name}, dog's age: {this.age}";
	}

	class Mouth
	{
		private Tooth[] teehtsInMouth = new Tooth[1];

		internal Tooth[] TeehtsInMouth
		{
			get
			{
				return teehtsInMouth;
			}

			set
			{
				teehtsInMouth = value;
			}
		}
	}

	class Tooth
	{
		private string scientificName;

		/*
		 * C#6 feature: initialize a property when the property is defined
		 */
		public string ScientificName
		{
			get; set;
		} = "default tooth name";
	}
	

}
