using MovieManager.Models;
using static MovieManager.Data.DataConstants.User;
using System.Text.RegularExpressions;

namespace MovieManager.Services.UserServices
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterViewModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }
            
            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }


        public ICollection<string> ValidateRepository(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }

        /*
        public ICollection<string> ValidateRepository(CreateListFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < RepositoryMinName || model.Name.Length > RepositoryMaxName)
            {
                errors.Add($"Repository '{model.Name}' is not valid. It must be between {RepositoryMinName} and {RepositoryMinName} characters long.");
            }

            if (model.RepositoryType != RepositoryPublicType && model.RepositoryType != RepositoryPrivateType)
            {
                errors.Add($"Repository type can be either '{RepositoryPublicType}' or '{RepositoryPrivateType}'.");
            }

            return errors;
        }
        */
    }
}
