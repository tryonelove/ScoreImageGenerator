using ScoreImageGenerator.Generator.API.Responses;

namespace ScoreImageGenerator.Generator.Objects
{
    public class User
    {
        public byte[] Avatar { get; set; }

        // Username
        public string Username { get; set; }

        // Worldwide rank
        public int Rank { get; set; }

        // Performance points
        public double PP { get; set; }

        public User(GetUserResponse response)
        {
            Username = response.Username;
            Rank = int.Parse(response.PpRank);
            PP = double.Parse(response.PpRaw);
        }
    }
}