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
        public float PP { get; set; }

        public User(GetUserResponse response)
        {
            Username = response.Username;
            Rank = int.Parse(response.PpRank);
            PP = float.Parse(response.PpRaw);
        }
    }
}