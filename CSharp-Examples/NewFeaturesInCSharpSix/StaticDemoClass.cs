using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewFeaturesInCSharpSix
{
	static class ProgramHelper
	{
		public static int GetARandomAge()
		{
			var rand = new Random();
			return rand.Next(1,15);
		}
	}
}
