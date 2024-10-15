using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto_s
{
	public class MatchDto
	{
		public int UserIdPlayer { get; set; }
		public int? UserIdPlayer2 { get; set; }
		public int UserIdOpponent { get; set; }
		public int? UserIdOpponent2 { get; set; }
		public bool IsDoubleMatch { get; set; }
	}
}
