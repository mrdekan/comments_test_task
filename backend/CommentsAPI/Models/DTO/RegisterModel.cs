namespace CommentsAPI.Models.DTO
{
    public class RegisterModel : LoginModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
