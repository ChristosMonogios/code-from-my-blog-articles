### Five new and useful features of C#6

Take a look at the working code of this project, you will find the following 5 new candies in C#6:

1. *static keyword in using*: Use it for a static class when using the class in another file.
Then, when we call the members of the static class, we can call them without writing the name of the class
Example:
```
using static NewFeaturesInCSharpSix.ProgramHelper;
```

2. *null conditional operator*: Removes the need for checking for null values and thus we avoid the tedious: if(... != null && ... != null .... etc.)
Example:
```
if (myDog?.Mouth?.TeehtsInMouth[0]?.ScientificName == "default tooth name") ...
```

3. *Property initialization*:
Define and initialize a property at once:
```
public string ScientificName
		{
			get; set;
		} = "default tooth name";
```

4. Expression bodied methods:
Use this type of method definition when dealing with "one line" methods.
```
public int CalculateAgeOfDogAsAgeOfHuman()
	=> this.age * 7;
```

5. New operator for string interpolation:
More simple and with fewer required code than String.Format
```
$"Dog's name: {this.name}, dog's age: {this.age}";
```