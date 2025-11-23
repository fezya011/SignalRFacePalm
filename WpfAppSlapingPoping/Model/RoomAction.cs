namespace ServerSignalRFacePalm.Server
{
    public class RoomAction
    {
        public string Actor {  get; set; }
        public string GroupId { get; set; }
        public int ActionType { get; set; }
        public string Target { get; set; }
    }
}