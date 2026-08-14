using System.Net;

namespace team_management_system.Helper
{
    public class APIResponse
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public dynamic Data { get; set; }
        public List<string> Message { get; set; } = new List<string>();
    }
}
