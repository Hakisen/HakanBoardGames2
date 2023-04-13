namespace HakanBoardGames.Shared.Models
{

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string released { get; set; }
        public string tagline { get; set; }
        public Players players { get; set; }
        public int age { get; set; }
        public string[] creators { get; set; }
        public string[] categories { get; set; }
    }

    public class Players
    {
        public int min { get; set; }
        public int max { get; set; }
    }

}
