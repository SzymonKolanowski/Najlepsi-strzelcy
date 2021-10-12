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
				Console.WriteLine("If you want see scorers which have min. 50 goals in Champions Leaque write 'CHL50Goals'");
				Console.WriteLine("If you want see scorers which have min. 50 goals in National representation write 'National50Goals'");
				Console.WriteLine("If you want see scorers which have min. 500 goals in Leaques write 'Leaque500Goals'");
				Console.WriteLine("If you want add next goals to some scorer write 'AddNextGoals'");
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
					case "Leaque500Goals":
						Leaque500Goals();
						break;
					case "CHL50Goals":
						CHL50Goals();
						break;
					case "National50Goals":
						National50Goals();
						break;
					case "AddNextGoals":
						AddNextGoals();
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

			//tu chcę zrobić by się wyświetlało tylko imię i nazwisko gracza i suma goli
			var scorers = database.SortScorersDescByTotalGoals();
			WriteJson(scorers);
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
		}

		private static void Leaque500Goals()
		{
			foreach (var scorer in database.ScorersList())
			{
				if (scorer.Goals[CompetitionType.League] > 500)
				{
					Console.WriteLine("Name and Surname");
					Console.WriteLine(scorer.NameAndSurname);
					Console.WriteLine("Leaque Goals");
					Console.WriteLine(scorer.Goals[CompetitionType.League]);
					Console.WriteLine();
				}
			}
		}

		private static void CHL50Goals()
		{
			foreach (var scorer in database.ScorersList())
			{
				if (scorer.Goals[CompetitionType.ChampionsLeague] > 50)
				{
					Console.WriteLine("Name and Surname");
					Console.WriteLine(scorer.NameAndSurname);
					Console.WriteLine("Champions Leaque Goals");
					Console.WriteLine(scorer.Goals[CompetitionType.ChampionsLeague]);
					Console.WriteLine();
				}
			}
		}

		private static void National50Goals()
		{
			foreach (var scorer in database.ScorersList())
			{
				if (scorer.Goals[CompetitionType.National] > 50)
				{
					Console.WriteLine("Name and Surname");
					Console.WriteLine(scorer.NameAndSurname);
					Console.WriteLine("National Goals");
					Console.WriteLine(scorer.Goals[CompetitionType.National]);
					Console.WriteLine();
				}
			}
		}

		private static void AddNextGoals()
		{
			Console.WriteLine("Choose id of scorer");
			var scorerid = GetIntParameter();

			var scorer = database.GetScorerById(scorerid);
			//scorer.Goals[CompetitionType.ChampionsLeague] = new Dictionary<CompetitionType , int>();

			Console.WriteLine("Next Goals in Champions Leaque");
			var championsleaque = GetIntParameter();

			Console.WriteLine("Next goals in Leaques");
			var leaques = GetIntParameter();

			Console.WriteLine("Next goals in National Representation");
			var nationals = GetIntParameter();
			
			database.AddNextGoals(scorer,championsleaque,leaques,nationals);			

		}

	}
}
