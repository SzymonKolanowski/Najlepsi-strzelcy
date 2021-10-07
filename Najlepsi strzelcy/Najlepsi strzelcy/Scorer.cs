using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Najlepsi_strzelcy
{
	public class Scorer
	{
		public int ID { get; set; }
		public string NameAndSurname { get; set; }
		public int YearOfBirth { get; set; }
		public string Nation { get; set; }


		public Dictionary<CompetitionType, int> Goals { get; }

		public Scorer()
		{
			Goals = new Dictionary<CompetitionType, int>
			{
				[CompetitionType.ChampionsLeague] = 0,
				[CompetitionType.League] = 0,
				[CompetitionType.National] = 0,
			};
		}

		public int TotalGoals
		{
			get
			{
				return Goals.Aggregate(0, (totals, competition) => totals + competition.Value);
			}
		}

	}

	public enum CompetitionType
	{
		National,
		League,
		ChampionsLeague
	}
}
