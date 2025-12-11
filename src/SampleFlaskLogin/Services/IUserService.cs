namespace SampleFlaskLogin.Services
{
    /// <summary>
    /// Provides user authentication validation services.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Validates whether a username/password combination is correct.
        /// </summary>
        /// <param name="username">User's login username.</param>
        /// <param name="password">User's login password.</param>
        /// <returns>True if credentials are valid; otherwise false.</returns>
        bool ValidateUser(string username, string password);
    }
}
