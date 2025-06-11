namespace REVALB.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string CurrentRole { get; set; }

        public List<string> AvailableRoles { get; set; }

        public string SelectedRole { get; set; }
    }
}
