namespace SCA_Server.RequestModels
{
	public class MatchRequest
	{
		public int UserIdPlayer { get; set; }
		public int? UserIdPlayer2 { get; set; }
		public int UserIdOpponent { get; set; }
		public int? UserIdOpponent2 { get; set; }
		public bool IsDoubleMatch { get; set; }
	}
}
