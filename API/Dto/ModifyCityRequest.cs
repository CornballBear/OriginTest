namespace API.Dto
{
    public class ModifyCityRequest
    {
        public ModifyCityRequest()
        {
        }
        public string NewName { get; set; }
        public string OldName { get; set; }
        public string Temperature { get; set; }
    }
}