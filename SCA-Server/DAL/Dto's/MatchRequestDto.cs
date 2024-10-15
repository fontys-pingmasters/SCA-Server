using Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto_s
{
	public class MatchRequestDto
	{
		public int MatchId { get; set; }
		public RequestStatus Status { get; set; } = RequestStatus.Pending;
	}
}
