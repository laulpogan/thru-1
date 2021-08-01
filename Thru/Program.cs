using System;

namespace Thru
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new ThruGame())
				game.Run();
		}
	}
}
