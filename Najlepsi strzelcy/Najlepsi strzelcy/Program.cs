using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace Najlepsi_strzelcy
{
	class Program
	{
		private static ScorersDatabase database = new ScorersDatabase();
		static void Main(string[] args)
		{
			string command = string.Empty;
			do
			{
				Console.WriteLine("If you want add scorer write 'AddScorer'");
				Console.WriteLine("If you want see actual scorer list write 'ScorersList'");
				Console.WriteLine("If you want remove scorer write 'RemoveScorer'");
				Console.WriteLine("If you want see some scorer write 'ShowScorer'");
				Console.WriteLine("If you want see the best scorers in total competitions write 'SortScorersDescByTotalGoals'");
				Console.WriteLine("If you want see the best scorers in Champions Leaque competition write 'SortScorersDescByChlGoals'");
				Console.WriteLine("If you want see the best scorers in Leaques competition write 'SortScorersDescByLeaquesGoals'");
				Console.WriteLine("If you want see the best scorers in National competition write 'SortScorersDescByNationalsGoals'");
				Console.WriteLine("If you want leave program write 'Exit'");
				command = Console.ReadLine();

				switch (command)
				{
					case "ScorersList":
						ScorersList();
						break;
					case "AddScorer":
						AddScorer();
						break;
					case "RemoveScorer":
						RemoveScorer();
						break;
					case "ShowScorer":
						ShowScorer();
						break;
					case "SortScorersDescByTotalGoals":
						SortScorersDescByTotalGoals();
						break;
					case "SortScorersDescByChlGoals":
						SortScorersDescByChlGoals();
						break;
					case "SortScorersDescByLeaquesGoals":
						SortScorersDescByLeaquesGoalsals();
						break;
					case "SortScorersDescByNationalsGoals":
						SortScorersDescByNationalsGoalsals();
						break;
				}
			} while (command != "Exit");
			Console.WriteLine("Exiting program");
			database.Save();
		}

		private static void ScorersList()
		{
			var scorer = database.ScorersList();
			WriteJson(scorer);
		}

		private static void WriteJson(object obj)
		{
			var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
			Console.WriteLine(json);
		}

		private static int GetIntParameter()
		{
			var idInput = Console.ReadLine();
			var id = int.TryParse(idInput, out var parsedID)
				 ? parsedID
				 : 0;

			return id;
		}

		private static void AddScorer()
		{
			Console.WriteLine("Name and Surname");
			var nameAndSurname = Console.ReadLine();

			Console.WriteLine("YearofBirth");
			var yearOfBirth = GetIntParameter();

			Console.WriteLine("Nation");
			var nation = Console.ReadLine();

			Console.WriteLine("Goals in Champions Leaque");
			var championsleaque = GetIntParameter();


			Console.WriteLine("Goals in Leaques");
			var leaques = GetIntParameter();

			Console.WriteLine("Goals in National Representation");
			var nationals = GetIntParameter();

			var scorer = new Scorer
			{
				NameAndSurname = nameAndSurname,
				YearOfBirth = yearOfBirth,
				Nation = nation
			};

			scorer.Goals[CompetitionType.ChampionsLeague] = championsleaque;
			scorer.Goals[CompetitionType.League] = leaques;
			scorer.Goals[CompetitionType.National] = nationals;

			database.AddScorer(scorer);

		}

		private static void RemoveScorer()
		{
			Console.WriteLine("Which scorer which you want remove");
			var idscorer = GetIntParameter();
			database.RemoveScorer(idscorer);
		}

		private static void ShowScorer()
		{
			Console.WriteLine("Choose scorer which you want to see");
			var idscorer = GetIntParameter();
			Scorer scorer = database.GetScorerById(idscorer);


			var scorerViewModel = new
			{
				scorer.NameAndSurname,
				scorer.Nation,
				scorer.YearOfBirth,
				scorer.Goals,
				scorer.TotalGoals
			};
			WriteJson(scorerViewModel);
		}

		private static void SortScorersDescByTotalGoals()
		{
			var scorer = database.SortScorersDescByTotalGoals();
			WriteJson(scorer);
		}

		private static void SortScorersDescByChlGoals()
		{
			var scorers = database.SortScorersDescByChlGoals();
			WriteJson(scorers);
		}

		private static void SortScorersDescByLeaquesGoalsals()
		{
			var scorers = database.SortScorersDescByLeaquesGoals();
			WriteJson(scorers);
		}

		private static void SortScorersDescByNationalsGoalsals()
		{
			var scorers = database.SortScorersDescByNationalsGoals();
			WriteJson(scorers);
			// zmiana
			// zmiana2
		}
	}
}
